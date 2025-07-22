using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers; // Keep this for clarity
using Terraria;
using Terraria.ID;
using TShockAPI;
using TerrariaApi.Server;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class Permabuffs : TerrariaPlugin
    {
        public override string Name => "PermaBuffs";
        public override string Author => "SyntaxVoid, modified by ChatGPT";
        public override string Description => "Allows players to receive buffs automatically from potions stored in Piggy Bank.";
        public override Version Version => new Version(1, 0);

        private static System.Timers.Timer _timer;

        public Permabuffs(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
            ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
            ServerApi.Hooks.ServerLeave.Register(this, OnLeave);
            ServerApi.Hooks.GamePostUpdate.Register(this, OnGameUpdate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
                ServerApi.Hooks.ServerJoin.Deregister(this, OnJoin);
                ServerApi.Hooks.ServerLeave.Deregister(this, OnLeave);
                ServerApi.Hooks.GamePostUpdate.Deregister(this, OnGameUpdate);
                _timer?.Dispose();
            }

            base.Dispose(disposing);
        }

        private void OnInitialize(EventArgs args)
        {
            Commands.ChatCommands.Add(new Command("permabuff.toggle", TogglePermabuffs, "pbenable", "pbdisable")
            {
                HelpText = "Enable or disable automatic permanent potion effects."
            });

            _timer = new System.Timers.Timer(10000);
            _timer.Elapsed += CheckBuffs;
            _timer.Start();

            Potions.PopulateBuffMap();
        }

        private void OnJoin(JoinEventArgs args)
        {
            DB.ToggleStatus[args.Who] = DB.GetStatus(args.Who);
        }

        private void OnLeave(LeaveEventArgs args)
        {
            if (DB.ToggleStatus.ContainsKey(args.Who))
            {
                DB.ToggleStatus.Remove(args.Who);
            }
        }

        private void TogglePermabuffs(CommandArgs args)
        {
            bool enable = args.Message == "pbenable";

            DB.SetStatus(args.Player.Index, enable);
            DB.ToggleStatus[args.Player.Index] = enable;

            args.Player.SendSuccessMessage($"PermaBuffs {(enable ? "enabled" : "disabled")}.");
        }

        private void OnGameUpdate(EventArgs args)
        {
            // Optional alternative to timer-based updates
        }

        private void CheckBuffs(object sender, ElapsedEventArgs e)
        {
            foreach (TSPlayer player in TShock.Players.Where(p => p?.Active ?? false))
            {
                if (!DB.ToggleStatus.TryGetValue(player.Index, out bool enabled) || !enabled)
                    continue;

                var inventory = player.TPlayer.bank.item;
                foreach (var item in inventory)
                {
                    if (item == null || item.stack < 30 || string.IsNullOrWhiteSpace(item.Name))
                        continue;

                    if (Potions.BuffMap.TryGetValue(item.Name.ToLower(), out int buffId))
                    {
                        if (!player.TPlayer.HasBuff(buffId))
                        {
                            player.TPlayer.AddBuff(buffId, 3600, true);
                        }
                    }
                }
            }
        }
    }
}