
using System;
using System.Collections.Generic;
using System.Timers; // Keep for clarity
using TShockAPI;
using Terraria;
using TerrariaApi.Server;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class PermabuffsPlugin : TerrariaPlugin
    {
        public override string Author => "SyntaxVoid";
        public override string Description => "Automatically grants buffs based on potions in the piggy bank.";
        public override string Name => "Permabuffs";
        public override Version Version => new Version(1, 0, 0, 0);

        private Dictionary<int, System.Timers.Timer> playerTimers = new();

        public PermabuffsPlugin(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
            ServerApi.Hooks.ServerLeave.Register(this, OnLeave);
        }

        private void OnInitialize(EventArgs args)
        {
            DB.Connect();
            Commands.ChatCommands.Add(new Command("permabuffs.toggle", TogglePermabuffs, "pbenable", "pbdisable"));

            foreach (var player in TShock.Players)
            {
                if (player != null && player.Active)
                    StartTimer(player);
            }
        }

        private void TogglePermabuffs(CommandArgs args)
        {
            if (args.Player == null || !args.Player.Active)
                return;

            var userId = args.Player.Account.ID;
            var enabled = playerTimers.ContainsKey(args.Player.Index);

            if (args.Message.StartsWith("/pbenable"))
            {
                if (!enabled)
                {
                    StartTimer(args.Player);
                    args.Player.SendSuccessMessage("Permabuffs enabled.");
                }
                else
                {
                    args.Player.SendInfoMessage("Permabuffs are already enabled.");
                }
            }
            else if (args.Message.StartsWith("/pbdisable"))
            {
                if (enabled)
                {
                    StopTimer(args.Player.Index);
                    args.Player.SendSuccessMessage("Permabuffs disabled.");
                }
                else
                {
                    args.Player.SendInfoMessage("Permabuffs are already disabled.");
                }
            }
        }

        private void StartTimer(TSPlayer player)
        {
            if (playerTimers.ContainsKey(player.Index))
                return;

            var timer = new System.Timers.Timer(1000); // Disambiguated Timer
            timer.Elapsed += (sender, args) => ApplyPermabuffs(player);
            timer.AutoReset = true;
            timer.Start();

            playerTimers[player.Index] = timer;
        }

        private void StopTimer(int index)
        {
            if (playerTimers.TryGetValue(index, out var timer))
            {
                timer.Stop();
                timer.Dispose();
                playerTimers.Remove(index);
            }
        }

        private void OnLeave(LeaveEventArgs args)
        {
            StopTimer(args.Who);
        }

        private void ApplyPermabuffs(TSPlayer player)
        {
            if (player?.TPlayer == null || !player.Active)
                return;

            var inventory = player.TPlayer.bank.item;
            var foundBuffs = new List<int>();

            foreach (var item in inventory)
            {
                if (item == null || item.stack < 30 || item.netID <= 0)
                    continue;

                if (Potions.BuffMap.TryGetValue(item.Name, out int buffId))
                {
                    foundBuffs.Add(buffId);
                }
            }

            foreach (var buff in foundBuffs)
            {
                if (!player.TPlayer.HasBuff(buff))
                {
                    player.TPlayer.AddBuff(buff, 60 * 2); // Apply for 2 seconds, will refresh each tick
                }
            }
        }
    }
}