using System;
using System.Collections.Generic;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using OTAPI;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class Permabuffs : TerrariaPlugin
    {
        public override string Author => "Original: SyntaxVoid | Restored by ChatGPT";
        public override string Name => "Permabuffs";
        public override Version Version => new Version(1, 1);
        public override string Description => "Grants buffs when 30+ potions or food are in Piggy Bank.";

        public Permabuffs(Main game) : base(game) { }

        public static Dictionary<int, bool> EnabledPlayers = new();

        public override void Initialize()
        {
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
            Commands.ChatCommands.Add(new Command("permabuffs.toggle", ToggleBuffs, "pbenable", "pbdisable"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameUpdate.Deregister(this, OnUpdate);
            }
            base.Dispose(disposing);
        }

        private void ToggleBuffs(CommandArgs args)
        {
            var player = args.Player;

            if (args.Message.StartsWith("/pbenable"))
            {
                EnabledPlayers[player.Index] = true;
                player.SendSuccessMessage("Permabuffs enabled.");
            }
            else if (args.Message.StartsWith("/pbdisable"))
            {
                EnabledPlayers[player.Index] = false;
                player.SendSuccessMessage("Permabuffs disabled.");
            }
        }

        private void OnUpdate(EventArgs args)
        {
            foreach (var tsPlayer in TShock.Players)
            {
                if (tsPlayer == null || !tsPlayer.Active || tsPlayer.TPlayer == null)
                    continue;

                if (!EnabledPlayers.TryGetValue(tsPlayer.Index, out var isEnabled) || !isEnabled)
                    continue;

                var player = tsPlayer.TPlayer;
                var piggyBank = player.bank?.item;

                if (piggyBank == null)
                    continue;

                foreach (var pair in Potions.BuffIDs)
                {
                    int itemType = pair.Key;
                    int buffID = pair.Value;
                    int stack = 0;

                    foreach (var item in piggyBank)
                    {
                        if (item != null && item.type == itemType && item.stack > 0)
                        {
                            stack += item.stack;
                        }
                    }

                    if (stack >= 30 && !player.HasBuff(buffID))
                    {
                        player.AddBuff(buffID, 60);
                    }
                }
            }
        }
    }
}