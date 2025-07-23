using System;
using System.Collections.Generic;
using System.Timers;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class Permabuffs : TerrariaPlugin
    {
        private Dictionary<int, bool> enabledPlayers = new();
        private Timer piggyScanTimer;
        private Timer buffApplyTimer;

        public override Version Version => new Version(1, 0);
        public override string Name => "Permabuffs";
        public override string Author => "Myoni/SyntaxVoid + fixes by ChatGPT";
        public override string Description => "Applies buffs based on potions, food, and drink stored in a player's piggy bank.";

        public Permabuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            Commands.ChatCommands.Add(new Command("permabuffs.toggle", TogglePermabuffs, "pbenable", "pbdisable"));

            piggyScanTimer = new Timer(60000); // 60 seconds
            piggyScanTimer.Elapsed += OnPiggyScan;
            piggyScanTimer.AutoReset = true;
            piggyScanTimer.Start();

            buffApplyTimer = new Timer(30000); // 30 seconds
            buffApplyTimer.Elapsed += OnBuffApply;
            buffApplyTimer.AutoReset = true;
            buffApplyTimer.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                piggyScanTimer?.Dispose();
                buffApplyTimer?.Dispose();
            }
        }

        private void TogglePermabuffs(CommandArgs args)
        {
            if (args.CommandName == "pbenable")
            {
                enabledPlayers[args.Player.Index] = true;
                args.Player.SendSuccessMessage("Permabuffs enabled.");
            }
            else if (args.CommandName == "pbdisable")
            {
                enabledPlayers[args.Player.Index] = false;
                args.Player.SendSuccessMessage("Permabuffs disabled.");
            }
        }

        private void OnPiggyScan(object sender, ElapsedEventArgs e)
        {
            foreach (TSPlayer tsPlayer in TShock.Players)
            {
                if (tsPlayer?.Active != true || !tsPlayer.RealPlayer)
                    continue;

                if (!enabledPlayers.TryGetValue(tsPlayer.Index, out bool enabled) || !enabled)
                    continue;

                var player = Main.player[tsPlayer.Index];
                var piggyBankItems = player.bank.item;

                tsPlayer.SendInfoMessage($"[DEBUG] Scanning piggy bank for {tsPlayer.Name}");

                foreach (var item in piggyBankItems)
                {
                    if (item == null || item.stack < 30)
                        continue;

                    if (Potions.buffMap.TryGetValue(item.Name.ToLowerInvariant(), out int buffID))
                    {
                        tsPlayer.SendInfoMessage($"[DEBUG] Found {item.stack}x {item.Name} → BuffID {buffID}");
                    }
                }
            }
        }

        private void OnBuffApply(object sender, ElapsedEventArgs e)
        {
            foreach (TSPlayer tsPlayer in TShock.Players)
            {
                if (tsPlayer?.Active != true || !tsPlayer.RealPlayer)
                    continue;

                if (!enabledPlayers.TryGetValue(tsPlayer.Index, out bool enabled) || !enabled)
                    continue;

                var player = Main.player[tsPlayer.Index];
                var piggyBankItems = player.bank.item;

                foreach (var item in piggyBankItems)
                {
                    if (item == null || item.stack < 30)
                        continue;

                    if (Potions.buffMap.TryGetValue(item.Name.ToLowerInvariant(), out int buffID))
                    {
                        player.AddBuff(buffID, 3600, true);
                        tsPlayer.SendInfoMessage($"[DEBUG] Applied buff {buffID} ({item.Name})");
                    }
                }
            }
        }
    }
}