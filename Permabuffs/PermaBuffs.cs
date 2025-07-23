using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using OTAPI;
using Microsoft.Xna.Framework;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class PermaBuffs : TerrariaPlugin
    {
        public override string Name => "PermaBuffs";
        public override string Author => "Myoni (SyntaxVoid)";
        public override string Description => "Piggy Bank Potion Buffs";
        public override Version Version => new Version(1, 0, 0, 0);

        private System.Timers.Timer checkPiggyBankTimer;
        private System.Timers.Timer applyBuffTimer;

        private Dictionary<int, List<int>> playerBuffs = new Dictionary<int, List<int>>();
        private HashSet<int> enabledPlayers = new HashSet<int>();

        public PermaBuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
            ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
            ServerApi.Hooks.ServerLeave.Register(this, OnLeave);

            applyBuffTimer = new System.Timers.Timer(60000);
            applyBuffTimer.Elapsed += (sender, e) => ApplyBuffs();
            applyBuffTimer.Start();

            checkPiggyBankTimer = new System.Timers.Timer(60000);
            checkPiggyBankTimer.Elapsed += (sender, e) => CheckPiggyBanks();
            checkPiggyBankTimer.Start();

            Commands.ChatCommands.Add(new Command("permabuffs.use", ToggleBuffs, "pbenable", "pbdisable"));
        }

        private void OnInitialize(EventArgs args)
        {
            Potions.Initialize();
        }

        private void OnJoin(JoinEventArgs args)
        {
            int id = args.Who;
            enabledPlayers.Add(id);
            playerBuffs[id] = new List<int>();
        }

        private void OnLeave(LeaveEventArgs args)
        {
            int id = args.Who;
            enabledPlayers.Remove(id);
            playerBuffs.Remove(id);
        }

        private void ToggleBuffs(CommandArgs args)
        {
            int id = args.Player.Index;
            if (args.Message.Contains("disable"))
            {
                enabledPlayers.Remove(id);
                args.Player.SendSuccessMessage("Permabuffs disabled.");
            }
            else
            {
                enabledPlayers.Add(id);
                args.Player.SendSuccessMessage("Permabuffs enabled.");
            }
        }

        private void CheckPiggyBanks()
        {
            foreach (TSPlayer player in TShock.Players.Where(p => p != null && p.Active))
            {
                int id = player.Index;
                if (!enabledPlayers.Contains(id))
                    continue;

                List<int> buffs = new List<int>();

                foreach (Item item in Main.player[id].bank.item)
                {
                    if (item == null || item.stack < 30 || !Potions.BuffMap.TryGetValue(item.Name, out int buffId))
                        continue;

                    if (!buffs.Contains(buffId))
                    {
                        buffs.Add(buffId);
                        TShock.Log.ConsoleInfo($"[DEBUG] Player {player.Name} has {item.stack}x {item.Name} => Buff {buffId}");
                    }
                }

                playerBuffs[id] = buffs;
            }
        }

        private void ApplyBuffs()
        {
            foreach (TSPlayer player in TShock.Players.Where(p => p != null && p.Active))
            {
                int id = player.Index;
                if (!enabledPlayers.Contains(id))
                    continue;

                if (!playerBuffs.TryGetValue(id, out List<int> buffs))
                    continue;

                foreach (int buff in buffs)
                {
                    if (!Main.player[id].HasBuff(buff))
                    {
                        Main.player[id].AddBuff(buff, 3600, true);
                        TShock.Log.ConsoleInfo($"[DEBUG] Applied Buff {buff} to {player.Name}");
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
                ServerApi.Hooks.ServerJoin.Deregister(this, OnJoin);
                ServerApi.Hooks.ServerLeave.Deregister(this, OnLeave);

                applyBuffTimer?.Dispose();
                checkPiggyBankTimer?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}