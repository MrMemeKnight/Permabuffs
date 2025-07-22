using OTAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class Permabuffs : TerrariaPlugin
    {
        public override string Author => "Myoni (SyntaxVoid)";
        public override string Name => "Permabuffs";
        public override Version Version => new Version(1, 0);
        public override string Description => "Grants players buffs based on items in their piggy bank.";

        private readonly Dictionary<int, bool> EnabledPlayers = new();

        public Permabuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
            ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
            ServerApi.Hooks.ServerLeave.Register(this, OnLeave);

            Commands.ChatCommands.Add(new Command("permabuffs.toggle", TogglePermabuffs, "pbenable", "pbdisable"));

            Potions.Initialize();
        }

        private void OnJoin(JoinEventArgs args)
        {
            EnabledPlayers[args.Who] = true;
        }

        private void OnLeave(LeaveEventArgs args)
        {
            EnabledPlayers.Remove(args.Who);
        }

        private void TogglePermabuffs(CommandArgs args)
        {
            if (!EnabledPlayers.ContainsKey(args.Player.Index))
                EnabledPlayers[args.Player.Index] = true;

            if (args.Message.ToLower().Contains("disable"))
            {
                EnabledPlayers[args.Player.Index] = false;
                args.Player.SendSuccessMessage("Permabuffs disabled.");
            }
            else
            {
                EnabledPlayers[args.Player.Index] = true;
                args.Player.SendSuccessMessage("Permabuffs enabled.");
            }
        }

        private void OnUpdate(EventArgs args)
        {
            foreach (TSPlayer player in TShock.Players)
            {
                if (player == null || !player.Active || !player.ConnectionAlive || !EnabledPlayers.TryGetValue(player.Index, out bool enabled) || !enabled)
                    continue;

                var piggyBank = Main.player[player.Index].bank.item;
                foreach (var item in piggyBank)
                {
                    if (item == null || item.stack < 30)
                        continue;

                    if (Potions.Map.TryGetValue(item.type, out int buffID))
                    {
                        if (!player.TPlayer.HasBuff(buffID))
                            player.SetBuff(buffID, 60 * 10, quiet: true);
                    }
                }
            }
        }
    }
}