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
        private System.Timers.Timer _timer;
        private readonly Dictionary<string, bool> _enabledPlayers = new Dictionary<string, bool>();
        private DB _db;

        public override string Name => "Permabuffs";
        public override string Author => "Myoni (SyntaxVoid)";
        public override string Description => "Adds permanent buff functionality via piggy bank.";
        public override Version Version => new Version(1, 0);

        public Permabuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
            Commands.ChatCommands.Add(new Command("permabuffs.toggle", ToggleBuffs, "pbenable", "pbdisable"));
            _db = new DB();

            Potions.PopulateBuffMap();

            _timer = new System.Timers.Timer(3000);
            _timer.Elapsed += CheckBuffs;
            _timer.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.ServerJoin.Deregister(this, OnJoin);
                _timer.Dispose();
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void OnJoin(JoinEventArgs args)
        {
            var playerName = TShock.Players[args.Who]?.Name;
            if (!string.IsNullOrEmpty(playerName) && !_enabledPlayers.ContainsKey(playerName))
            {
                _enabledPlayers[playerName] = true;
            }
        }

        private void ToggleBuffs(CommandArgs args)
        {
            if (args.Message.StartsWith("/pbenable"))
            {
                _enabledPlayers[args.Player.Name] = true;
                args.Player.SendSuccessMessage("Permanent buffs enabled.");
            }
            else if (args.Message.StartsWith("/pbdisable"))
            {
                _enabledPlayers[args.Player.Name] = false;
                args.Player.SendSuccessMessage("Permanent buffs disabled.");
            }
        }

        private void CheckBuffs(object sender, ElapsedEventArgs e)
        {
            foreach (var tsPlayer in TShock.Players)
            {
                if (tsPlayer == null || !tsPlayer.Active || !tsPlayer.TPlayer.active)
                    continue;

                if (!_enabledPlayers.TryGetValue(tsPlayer.Name, out bool enabled) || !enabled)
                    continue;

                var player = tsPlayer.TPlayer;
                var piggyBank = player.bank.item;

                foreach (var item in piggyBank)
                {
                    if (item == null || item.stack < 30)
                        continue;

                    if (Potions.BuffMap.TryGetValue(item.Name, out var buffId))
                    {
                        if (!player.buffType.Contains(buffId))
                        {
                            player.AddBuff(buffId, 60 * 10); // 10 seconds
                        }
                    }
                }
            }
        }
    }
}