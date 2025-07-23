using System;
using System.Collections.Generic;
using System.Timers;
using Terraria;
using Terraria.ID;
using TShockAPI;

namespace Permabuffs
{
    public class PermabuffManager
    {
        private readonly Dictionary<int, HashSet<int>> playerActiveBuffs = new();
        private readonly Timer piggyBankScanTimer;
        private readonly Timer buffApplyTimer;

        public PermabuffManager()
        {
            piggyBankScanTimer = new Timer(60000); // 1 minute
            piggyBankScanTimer.Elapsed += (sender, args) => ScanAllPlayersPiggyBanks();
            piggyBankScanTimer.Start();

            buffApplyTimer = new Timer(30000); // 30 seconds
            buffApplyTimer.Elapsed += (sender, args) => ApplyAllPlayerBuffs();
            buffApplyTimer.Start();
        }

        private void ScanAllPlayersPiggyBanks()
        {
            foreach (TSPlayer player in TShock.Players)
            {
                if (player?.Active != true || !player.ConnectionAlive || player.TPlayer == null)
                    continue;

                if (!PermabuffsPlugin.EnabledPlayers.Contains(player.Account?.ID ?? -1))
                    continue;

                HashSet<int> buffsToApply = new();

                var piggyBank = player.TPlayer.bank.item;
                foreach (var item in piggyBank)
                {
                    if (item == null || item.stack < 30)
                        continue;

                    if (Potions.BuffMap.TryGetValue(item.Name, out int buffID))
                    {
                        buffsToApply.Add(buffID);
                    }
                }

                playerActiveBuffs[player.Index] = buffsToApply;
                player.SendInfoMessage($"[DEBUG] Scanned piggy bank. Buffs queued: {string.Join(", ", buffsToApply)}");
            }
        }

        private void ApplyAllPlayerBuffs()
        {
            foreach (TSPlayer player in TShock.Players)
            {
                if (player?.Active != true || !player.ConnectionAlive || player.TPlayer == null)
                    continue;

                if (!PermabuffsPlugin.EnabledPlayers.Contains(player.Account?.ID ?? -1))
                    continue;

                if (!playerActiveBuffs.TryGetValue(player.Index, out var buffs))
                    continue;

                foreach (var buffID in buffs)
                {
                    player.TPlayer.AddBuff(buffID, 60 * 60); // 60 seconds
                }

                player.SendInfoMessage($"[DEBUG] Applied buffs: {string.Join(", ", buffs)}");
            }
        }
    }

    public class PermabuffsPlugin : TerrariaPlugin
    {
        public static HashSet<int> EnabledPlayers = new();

        public override string Name => "Permabuffs";
        public override Version Version => new(1, 0);
        public override string Author => "Myoni/SyntaxVoid";
        public override string Description => "Applies buffs from Piggy Bank items.";

        private PermabuffManager manager;

        public PermabuffsPlugin(Main game) : base(game) { }

        public override void Initialize()
        {
            manager = new PermabuffManager();

            Commands.ChatCommands.Add(new Command("permabuff.toggle", ToggleCommand, "pbenable", "pbdisable"));
        }

        private void ToggleCommand(CommandArgs args)
        {
            int accountId = args.Player.Account?.ID ?? -1;
            if (accountId == -1)
            {
                args.Player.SendErrorMessage("You must be logged in to use this command.");
                return;
            }

            if (args.Message.Contains("pbenable"))
            {
                EnabledPlayers.Add(accountId);
                args.Player.SendSuccessMessage("Permabuffs enabled.");
            }
            else if (args.Message.Contains("pbdisable"))
            {
                EnabledPlayers.Remove(accountId);
                args.Player.SendSuccessMessage("Permabuffs disabled.");
            }
        }
    }
}