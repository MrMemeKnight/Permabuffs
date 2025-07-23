using System;
using System.Collections.Generic;
using System.Timers;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class Permabuffs : TerrariaPlugin
    {
        public override string Name => "Permabuffs";
        public override string Author => "Myoni + Modified";
        public override string Description => "Grants permanent buffs if 30 of an item are in your piggy bank.";
        public override Version Version => new Version(1, 0);

        private Dictionary<int, HashSet<int>> playerBuffs = new();
        private Timer scanTimer;
        private Timer buffTimer;

        public Permabuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            Commands.ChatCommands.Add(new Command("permabuffs.use", EnablePermabuffs, "pbenable"));
            Commands.ChatCommands.Add(new Command("permabuffs.use", DisablePermabuffs, "pbdisable"));

            ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
            ServerApi.Hooks.ServerLeave.Register(this, OnLeave);

            scanTimer = new Timer(60000); // every 60s
            scanTimer.Elapsed += ScanForBuffs;
            scanTimer.Start();

            buffTimer = new Timer(30000); // every 30s
            buffTimer.Elapsed += ApplyBuffs;
            buffTimer.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.ServerJoin.Deregister(this, OnJoin);
                ServerApi.Hooks.ServerLeave.Deregister(this, OnLeave);
                scanTimer?.Dispose();
                buffTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void OnJoin(JoinEventArgs args)
        {
            playerBuffs[args.Who] = new HashSet<int>();
        }

        private void OnLeave(LeaveEventArgs args)
        {
            playerBuffs.Remove(args.Who);
        }

        private void EnablePermabuffs(CommandArgs args)
        {
            args.Player.SetData("permabuffs_enabled", true);
            args.Player.SendSuccessMessage("Permabuffs enabled.");
        }

        private void DisablePermabuffs(CommandArgs args)
        {
            args.Player.RemoveData("permabuffs_enabled");
            args.Player.SendInfoMessage("Permabuffs disabled.");
        }

        private void ScanForBuffs(object sender, ElapsedEventArgs e)
        {
            foreach (TSPlayer player in TShock.Players)
            {
                if (player == null || !player.Active || !player.HasData("permabuffs_enabled"))
                    continue;

                var set = playerBuffs.GetValueOrDefault(player.Index);
                if (set == null)
                {
                    set = new HashSet<int>();
                    playerBuffs[player.Index] = set;
                }
                else
                {
                    set.Clear();
                }

                foreach (var item in player.TPlayer.bank.item)
                {
                    if (item == null || item.stack < 30 || string.IsNullOrEmpty(item.Name))
                        continue;

                    if (Potions.BuffMap.TryGetValue(item.Name, out int buffID))
                    {
                        set.Add(buffID);
                    }
                }
            }
        }

        private void ApplyBuffs(object sender, ElapsedEventArgs e)
        {
            foreach (TSPlayer player in TShock.Players)
            {
                if (player == null || !player.Active || !player.HasData("permabuffs_enabled"))
                    continue;

                if (!playerBuffs.TryGetValue(player.Index, out var buffs))
                    continue;

                foreach (int buffID in buffs)
                {
                    player.SetBuff(buffID, 1800); // 30 seconds duration (in ticks)
                }
            }
        }
    }
}