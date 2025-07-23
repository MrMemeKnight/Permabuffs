using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class Permabuffs : TerrariaPlugin
    {
        private static Dictionary<int, List<int>> PlayerBuffs = new();
        private static Dictionary<int, bool> Enabled = new();
        private static Dictionary<int, System.Timers.Timer> PlayerTimers = new(); // Disambiguated here
        private static System.Timers.Timer GlobalTimer; // Disambiguated here

        public override string Author => "Myoni";
        public override string Description => "Enables players to permanently gain potion buffs by storing 30 of the potion in their Piggy Bank.";
        public override string Name => "Permabuffs";
        public override Version Version => new(1, 0, 0, 0);

        public Permabuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
            ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
            ServerApi.Hooks.ServerLeave.Register(this, OnLeave);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
                ServerApi.Hooks.ServerJoin.Deregister(this, OnJoin);
                ServerApi.Hooks.ServerLeave.Deregister(this, OnLeave);
            }
            base.Dispose(disposing);
        }

        private void OnInitialize(EventArgs args)
        {
            Commands.ChatCommands.Add(new Command("permabuffs.toggle", ToggleBuffs, "pbenable", "pbdisable"));
            GlobalTimer = new System.Timers.Timer(5000);
            GlobalTimer.Elapsed += CheckBuffs;
            GlobalTimer.AutoReset = true;
            GlobalTimer.Start();
        }

        private void ToggleBuffs(CommandArgs args)
        {
            int playerIndex = args.Player.Index;
            if (!Enabled.ContainsKey(playerIndex))
                Enabled[playerIndex] = false;

            Enabled[playerIndex] = !Enabled[playerIndex];
            string status = Enabled[playerIndex] ? "enabled" : "disabled";
            args.Player.SendSuccessMessage($"Permabuffs {status}.");
        }

        private void OnJoin(JoinEventArgs args)
        {
            int playerIndex = args.Who;
            PlayerBuffs[playerIndex] = new List<int>();
            Enabled[playerIndex] = true;
        }

        private void OnLeave(LeaveEventArgs args)
        {
            int playerIndex = args.Who;
            PlayerBuffs.Remove(playerIndex);
            Enabled.Remove(playerIndex);
        }

        private void CheckBuffs(object sender, ElapsedEventArgs e)
        {
            foreach (TSPlayer player in TShock.Players.Where(p => p != null && p.Active))
            {
                if (!Enabled.TryGetValue(player.Index, out bool isEnabled) || !isEnabled)
                    continue;

                List<int> buffsToApply = Potions.GetBuffsFromPiggyBank();
                PlayerBuffs[player.Index] = buffsToApply;

                foreach (int buffId in buffsToApply)
                {
                    if (!player.TPlayer.buffType.Contains(buffId))
                    {
                        player.SetBuff(buffId, 60 * 30); // Apply buff for 30 seconds
                    }
                }
            }
        }
    }
}using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class Permabuffs : TerrariaPlugin
    {
        private static Dictionary<int, List<int>> PlayerBuffs = new();
        private static Dictionary<int, bool> Enabled = new();
        private static Dictionary<int, System.Timers.Timer> PlayerTimers = new(); // Disambiguated here
        private static System.Timers.Timer GlobalTimer; // Disambiguated here

        public override string Author => "Myoni";
        public override string Description => "Enables players to permanently gain potion buffs by storing 30 of the potion in their Piggy Bank.";
        public override string Name => "Permabuffs";
        public override Version Version => new(1, 0, 0, 0);

        public Permabuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
            ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
            ServerApi.Hooks.ServerLeave.Register(this, OnLeave);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
                ServerApi.Hooks.ServerJoin.Deregister(this, OnJoin);
                ServerApi.Hooks.ServerLeave.Deregister(this, OnLeave);
            }
            base.Dispose(disposing);
        }

        private void OnInitialize(EventArgs args)
        {
            Commands.ChatCommands.Add(new Command("permabuffs.toggle", ToggleBuffs, "pbenable", "pbdisable"));
            GlobalTimer = new System.Timers.Timer(5000);
            GlobalTimer.Elapsed += CheckBuffs;
            GlobalTimer.AutoReset = true;
            GlobalTimer.Start();
        }

        private void ToggleBuffs(CommandArgs args)
        {
            int playerIndex = args.Player.Index;
            if (!Enabled.ContainsKey(playerIndex))
                Enabled[playerIndex] = false;

            Enabled[playerIndex] = !Enabled[playerIndex];
            string status = Enabled[playerIndex] ? "enabled" : "disabled";
            args.Player.SendSuccessMessage($"Permabuffs {status}.");
        }

        private void OnJoin(JoinEventArgs args)
        {
            int playerIndex = args.Who;
            PlayerBuffs[playerIndex] = new List<int>();
            Enabled[playerIndex] = true;
        }

        private void OnLeave(LeaveEventArgs args)
        {
            int playerIndex = args.Who;
            PlayerBuffs.Remove(playerIndex);
            Enabled.Remove(playerIndex);
        }

        private void CheckBuffs(object sender, ElapsedEventArgs e)
        {
            foreach (TSPlayer player in TShock.Players.Where(p => p != null && p.Active))
            {
                if (!Enabled.TryGetValue(player.Index, out bool isEnabled) || !isEnabled)
                    continue;

                List<int> buffsToApply = Potions.GetBuffsFromPiggyBank(player.TPlayer);
                PlayerBuffs[player.Index] = buffsToApply;

                foreach (int buffId in buffsToApply)
                {
                    if (!player.TPlayer.buffType.Contains(buffId))
                    {
                        player.SetBuff(buffId, 60 * 10); // Apply buff for 10 seconds
                    }
                }
            }
        }
    }
}