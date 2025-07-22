using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class PermaBuffs : TerrariaPlugin
    {
        public override string Name => "PermaBuffs";
        public override string Author => "SyntaxVoid (Myoni)";
        public override string Description => "Provides players with buffs based on potions in their piggy bank.";
        public override Version Version => new Version(1, 0);

        private readonly Dictionary<int, List<int>> ActiveBuffs = new();
        private Timer timer;

        public PermaBuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GameUpdate.Register(this, OnGameUpdate);
            Commands.ChatCommands.Add(new Command("permbuff.toggle", ToggleBuffs, "pbenable", "pbdisable"));
            timer = new Timer(5000);
            timer.Elapsed += (sender, args) => ApplyBuffsToAllPlayers();
            timer.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameUpdate.Deregister(this, OnGameUpdate);
                timer?.Stop();
                timer?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void OnGameUpdate(EventArgs args)
        {
            // Left empty on purpose, timer handles updates
        }

        private void ToggleBuffs(CommandArgs args)
        {
            if (args.Message.Equals("/pbenable", StringComparison.OrdinalIgnoreCase))
            {
                ActiveBuffs[args.Player.Index] = new List<int>();
                args.Player.SendSuccessMessage("PermaBuffs enabled.");
            }
            else if (args.Message.Equals("/pbdisable", StringComparison.OrdinalIgnoreCase))
            {
                ActiveBuffs.Remove(args.Player.Index);
                args.Player.SendSuccessMessage("PermaBuffs disabled.");
            }
        }

        private void ApplyBuffsToAllPlayers()
        {
            foreach (TSPlayer tsPlayer in TShock.Players)
            {
                if (tsPlayer == null || !tsPlayer.Active || !tsPlayer.RealPlayer)
                    continue;

                Player player = tsPlayer.TPlayer;
                if (!ActiveBuffs.ContainsKey(player.whoAmI))
                    continue;

                List<int> buffsToApply = GetBuffsFromPiggyBank(player);
                foreach (int buffID in buffsToApply)
                {
                    if (!player.buffType.Contains(buffID))
                        player.AddBuff(buffID, 60 * 10); // 10 seconds
                }

                ActiveBuffs[player.whoAmI] = buffsToApply;
            }
        }

        private List<int> GetBuffsFromPiggyBank(Player player)
        {
            List<int> buffIDs = new();
            for (int i = 0; i < 40; i++)
            {
                var item = player.bank.item[i];
                if (item != null && !string.IsNullOrEmpty(item.Name) && item.stack >= 30)
                {
                    if (Potions.BuffMap.TryGetValue(item.Name.ToLower(), out int buffID))
                    {
                        buffIDs.Add(buffID);
                    }
                }
            }
            return buffIDs.Distinct().ToList();
        }
    }
}