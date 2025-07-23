using OTAPI;
using System;
using System.Collections.Generic;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class Permabuffs : TerrariaPlugin
    {
        public override string Name => "Permabuffs";
        public override Version Version => new Version(1, 0);
        public override string Author => "Myoni (SyntaxVoid)";
        public override string Description => "Automatically applies permanent buffs from piggy bank potions";

        private HashSet<int> enabledPlayers = new HashSet<int>();

        public Permabuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
            Commands.ChatCommands.Add(new Command("permabuffs.toggle", ToggleBuffs, "pbenable"));
            Commands.ChatCommands.Add(new Command("permabuffs.toggle", ToggleBuffs, "pbdisable"));
            Potions.PopulateBuffMap();
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
            if (args.Message.Equals("/pbenable", StringComparison.OrdinalIgnoreCase))
            {
                enabledPlayers.Add(args.Player.Index);
                args.Player.SendSuccessMessage("Permabuffs enabled.");
            }
            else if (args.Message.Equals("/pbdisable", StringComparison.OrdinalIgnoreCase))
            {
                enabledPlayers.Remove(args.Player.Index);
                args.Player.SendSuccessMessage("Permabuffs disabled.");
            }
        }

        private void OnUpdate(EventArgs args)
        {
            foreach (TSPlayer tsPlayer in TShock.Players)
            {
                if (tsPlayer == null || !tsPlayer.Active || !enabledPlayers.Contains(tsPlayer.Index))
                    continue;

                var player = Main.player[tsPlayer.Index];
                var piggyBank = player.bank.item;

                foreach (var item in piggyBank)
                {
                    if (item == null || item.stack < 30 || !Potions.buffMap.ContainsKey(item.Name))
                        continue;

                    int buffId = Potions.buffMap[item.Name];
                    if (!Array.Exists(plr.buffType, id => id == buffID))
                        player.AddBuff(buffId, 60 * 10);
                }
            }
        }
    }
}