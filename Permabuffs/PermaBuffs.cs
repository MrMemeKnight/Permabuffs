using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Terraria;
using TShockAPI;

namespace Permabuffs
{
    [APIVersion(2, 1)]
    public class Permabuffs : TerrariaPlugin
    {
        public override string Name => "Permabuffs";
        public override string Author => "SyntaxVoid, modified by Gian";
        public override string Description => "Provides permanent buffs based on Piggy Bank inventory.";
        public override Version Version => new Version(1, 2, 0, 0);

        private Timer checkTimer;
        private Timer buffTimer;

        private readonly Dictionary<int, List<int>> playerBuffs = new();

        public Permabuffs(Main game) : base(game)
        {
        }

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

                checkTimer?.Dispose();
                buffTimer?.Dispose();
            }

            base.Dispose(disposing);
        }

        private void OnInitialize(EventArgs args)
        {
            checkTimer = new Timer(60000); // 60 seconds
            checkTimer.Elapsed += (sender, e) => CheckPiggyBanks();
            checkTimer.Start();

            buffTimer = new Timer(60000); // 60 seconds
            buffTimer.Elapsed += (sender, e) => ApplyBuffs();
            buffTimer.Start();

            Commands.ChatCommands.Add(new Command("permabuffs.toggle", TogglePermabuffs, "pbenable", "pbdisable"));
        }

        private void OnJoin(JoinEventArgs args)
        {
            lock (playerBuffs)
            {
                if (!playerBuffs.ContainsKey(args.Who))
                    playerBuffs[args.Who] = new List<int>();
            }
        }

        private void OnLeave(LeaveEventArgs args)
        {
            lock (playerBuffs)
            {
                if (playerBuffs.ContainsKey(args.Who))
                    playerBuffs.Remove(args.Who);
            }
        }

        private void TogglePermabuffs(CommandArgs args)
        {
            var id = args.Player.Index;

            if (!playerBuffs.ContainsKey(id))
            {
                playerBuffs[id] = new List<int>();
                args.Player.SendSuccessMessage("Permabuffs enabled.");
            }
            else
            {
                playerBuffs.Remove(id);
                args.Player.SendSuccessMessage("Permabuffs disabled.");
            }
        }

        private void CheckPiggyBanks()
        {
            foreach (TSPlayer player in TShock.Players.Where(p => p?.Active == true))
            {
                int id = player.Index;

                if (!playerBuffs.ContainsKey(id))
                    continue;

                List<int> buffs = new();
                var piggyBank = Main.player[id].bank.item;

                TShock.Log.ConsoleInfo($"[DEBUG] Checking Piggy Bank for player {player.Name}");

                foreach (var item in piggyBank)
                {
                    if (item == null || item.stack < 30)
                        continue;

                    if (Potions.buffMap.TryGetValue(item.Name, out int buff))
                    {
                        TShock.Log.ConsoleInfo($"[DEBUG] Detected item '{item.Name}' granting buff {buff} for {player.Name}");
                        buffs.Add(buff);
                    }
                }

                lock (playerBuffs)
                {
                    playerBuffs[id] = buffs;
                }
            }
        }

        private void ApplyBuffs()
        {
            foreach (TSPlayer player in TShock.Players.Where(p => p?.Active == true))
            {
                int id = player.Index;

                if (!playerBuffs.ContainsKey(id))
                    continue;

                foreach (int buff in playerBuffs[id])
                {
                    TShock.Log.ConsoleInfo($"[DEBUG] Applying buff {buff} to player {player.Name}");

                    try
                    {
                        player.SetBuff(buff, 3600); // 60 seconds
                    }
                    catch (Exception ex)
                    {
                        TShock.Log.ConsoleError($"[ERROR] Failed to apply buff {buff} to {player.Name}: {ex.Message}");
                    }
                }
            }
        }
    }
}