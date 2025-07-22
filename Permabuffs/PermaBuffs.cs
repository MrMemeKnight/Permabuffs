using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using OTAPI;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class PermaBuffs : TerrariaPlugin
    {
        public override string Name => "PermaBuffs";
        public override string Author => "SyntaxVoid, updated for ARM64 by Gian";
        public override string Description => "Auto-applies buffs based on items in Piggy Bank.";
        public override Version Version => new Version(1, 0, 0, 0);

        private readonly Dictionary<int, bool> ActivePlayers = new();
        private readonly Timer timer = new(3000);

        public PermaBuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            Potions.PopulateBuffMap();
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
            ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
            ServerApi.Hooks.ServerLeave.Register(this, OnLeave);
            timer.Elapsed += OnTimedEvent;
            timer.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
                ServerApi.Hooks.ServerJoin.Deregister(this, OnJoin);
                ServerApi.Hooks.ServerLeave.Deregister(this, OnLeave);
                timer.Stop();
                timer.Dispose();
            }
            base.Dispose(disposing);
        }

        private void OnInitialize(EventArgs args)
        {
            Commands.ChatCommands.Add(new Command("permbuff.toggle", ToggleBuffs, "pbenable", "pbdisable"));
        }

        private void OnJoin(JoinEventArgs args)
        {
            ActivePlayers[args.Who] = false;
        }

        private void OnLeave(LeaveEventArgs args)
        {
            ActivePlayers.Remove(args.Who);
        }

        private void ToggleBuffs(CommandArgs args)
        {
            if (!ActivePlayers.ContainsKey(args.Player.Index))
                ActivePlayers[args.Player.Index] = false;

            string command = args.Message.Trim().ToLowerInvariant();

            if (command.EndsWith("pbenable"))
            {
                ActivePlayers[args.Player.Index] = true;
                args.Player.SendSuccessMessage("Permabuffs enabled.");
            }
            else if (command.EndsWith("pbdisable"))
            {
                ActivePlayers[args.Player.Index] = false;
                args.Player.SendInfoMessage("Permabuffs disabled.");
            }
            else
            {
                args.Player.SendErrorMessage("Unknown command. Use /pbenable or /pbdisable.");
            }
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            foreach (var pair in ActivePlayers)
            {
                int index = pair.Key;
                bool isActive = pair.Value;

                if (!isActive || index < 0 || index >= TShock.Players.Length)
                    continue;

                TSPlayer tsPlayer = TShock.Players[index];
                if (tsPlayer == null || !tsPlayer.Active || tsPlayer.TPlayer.dead)
                    continue;

                Player player = tsPlayer.TPlayer;
                var piggyBank = player.bank.item;

                foreach (var item in piggyBank)
                {
                    if (item == null || item.stack < 30 || string.IsNullOrEmpty(item.Name))
                        continue;

                    if (Potions.BuffIDs.TryGetValue(item.Name.ToLowerInvariant(), out int buffId))
                    {
                        bool hasBuff = player.buffType.Any(b => b == buffId);

                        if (!hasBuff)
                        {
                            player.AddBuff(buffId, 60 * 10); // 10 seconds
                        }
                    }
                }
            }
        }
    }
}