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
    public class PermaBuffs : TerrariaPlugin
    {
        public override string Name => "PermaBuffs";
        public override string Author => "SyntaxVoid (Original), Updated for ARM64 by You";
        public override string Description => "Automatically grants buffs if 30 of the corresponding potion are in the piggy bank.";
        public override Version Version => new Version(1, 0, 0, 0);

        private readonly Dictionary<int, System.Timers.Timer> _playerTimers = new();
        private readonly Dictionary<int, bool> _playerEnabled = new();

        public PermaBuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
            ServerApi.Hooks.ServerLeave.Register(this, OnLeave);
            Commands.ChatCommands.Add(new Command("permabuffs.toggle", ToggleBuffs, "pbenable", "pbdisable"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.ServerJoin.Deregister(this, OnJoin);
                ServerApi.Hooks.ServerLeave.Deregister(this, OnLeave);
            }
            base.Dispose(disposing);
        }

        private void OnJoin(JoinEventArgs args)
        {
            int playerIndex = args.Who;
            _playerEnabled[playerIndex] = false;

            var timer = new System.Timers.Timer(2000);
            timer.Elapsed += (sender, e) => ApplyBuffs(playerIndex);
            timer.Start();

            _playerTimers[playerIndex] = timer;
        }

        private void OnLeave(LeaveEventArgs args)
        {
            int playerIndex = args.Who;

            if (_playerTimers.ContainsKey(playerIndex))
            {
                _playerTimers[playerIndex].Stop();
                _playerTimers[playerIndex].Dispose();
                _playerTimers.Remove(playerIndex);
            }

            if (_playerEnabled.ContainsKey(playerIndex))
                _playerEnabled.Remove(playerIndex);
        }

        private void ToggleBuffs(CommandArgs args)
        {
            if (!_playerEnabled.ContainsKey(args.Player.Index))
                _playerEnabled[args.Player.Index] = false;

            bool enable = args.Message.Contains("pbenable");
            _playerEnabled[args.Player.Index] = enable;

            args.Player.SendSuccessMessage($"PermaBuffs {(enable ? "enabled" : "disabled")}.");
        }

        private void ApplyBuffs(int playerIndex)
        {
            if (!_playerEnabled.ContainsKey(playerIndex) || !_playerEnabled[playerIndex])
                return;

            var player = TShock.Players[playerIndex];
            if (player?.TPlayer == null || !player.Active)
                return;

            var piggyBank = player.TPlayer.bank.item;
            foreach (var item in piggyBank)
            {
                if (item == null || item.stack < 30 || string.IsNullOrEmpty(item.Name))
                    continue;

                if (Potions.BuffMap.TryGetValue(item.Name, out int buffId))
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