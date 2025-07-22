using System;
using System.Collections.Generic;
using System.Timers;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using OTAPI;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class PermaBuffs : TerrariaPlugin
    {
        public override string Name => "PermaBuffs";
        public override string Author => "SyntaxVoid, modified by ChatGPT";
        public override string Description => "Grants permabuffs from Piggy Bank";
        public override Version Version => new Version(1, 0, 0);

        private DB db;
        private Dictionary<int, System.Timers.Timer> buffTimers = new();
        private HashSet<int> enabledPlayers = new();

        public PermaBuffs(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnGameInitialize);
            ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
            ServerApi.Hooks.ServerLeave.Register(this, OnLeave);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnGameInitialize);
                ServerApi.Hooks.ServerJoin.Deregister(this, OnJoin);
                ServerApi.Hooks.ServerLeave.Deregister(this, OnLeave);
            }
            base.Dispose(disposing);
        }

        private void OnGameInitialize(EventArgs args)
        {
            db = new DB();
            Potions.PopulateBuffMap();
            Commands.ChatCommands.Add(new Command("permabuffs.use", ToggleBuffs, "pbenable", "pbdisable"));
        }

        private void OnJoin(JoinEventArgs args)
        {
            int playerIndex = args.Who;
            Player player = Main.player[playerIndex];
            if (db.IsEnabled(player.name))
            {
                enabledPlayers.Add(playerIndex);
                StartBuffTimer(playerIndex);
            }
        }

        private void OnLeave(LeaveEventArgs args)
        {
            int playerIndex = args.Who;
            enabledPlayers.Remove(playerIndex);
            if (buffTimers.ContainsKey(playerIndex))
            {
                buffTimers[playerIndex].Stop();
                buffTimers[playerIndex].Dispose();
                buffTimers.Remove(playerIndex);
            }
        }

        private void StartBuffTimer(int index)
        {
            System.Timers.Timer timer = new(5000);
            timer.Elapsed += (sender, e) => ApplyBuffs(index);
            timer.Start();
            buffTimers[index] = timer;
        }

        private void ApplyBuffs(int index)
        {
            if (!enabledPlayers.Contains(index) || !Main.player[index].active)
                return;

            Player player = Main.player[index];

            foreach (Item item in player.bank.item)
            {
                if (item.stack >= 30 && Potions.BuffMap.TryGetValue(item.name.ToLower(), out int buffID))
                {
                    if (!player.buffType.Contains(buffID))
                    {
                        player.AddBuff(buffID, 60 * 10);
                    }
                }
            }
        }

        private void ToggleBuffs(CommandArgs args)
        {
            int index = args.Player.Index;
            string cmd = args.Message.Split(' ')[0].ToLowerInvariant();

            if (cmd == "/pbenable")
            {
                if (!enabledPlayers.Contains(index))
                {
                    db.SetEnabled(args.Player.Name, true);
                    enabledPlayers.Add(index);
                    StartBuffTimer(index);
                    args.Player.SendSuccessMessage("PermaBuffs enabled.");
                }
                else
                {
                    args.Player.SendInfoMessage("PermaBuffs are already enabled.");
                }
            }
            else if (cmd == "/pbdisable")
            {
                if (enabledPlayers.Contains(index))
                {
                    db.SetEnabled(args.Player.Name, false);
                    enabledPlayers.Remove(index);
                    if (buffTimers.ContainsKey(index))
                    {
                        buffTimers[index].Stop();
                        buffTimers[index].Dispose();
                        buffTimers.Remove(index);
                    }
                    args.Player.SendSuccessMessage("PermaBuffs disabled.");
                }
                else
                {
                    args.Player.SendInfoMessage("PermaBuffs are already disabled.");
                }
            }
        }
    }
}