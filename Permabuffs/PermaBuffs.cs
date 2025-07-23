using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class PermaBuffs : TerrariaPlugin
    {
        public override string Author => "SyntaxVoid";
        public override string Description => "Allows for potions placed in piggy bank to become permanent buffs.";
        public override string Name => "PermaBuffs";
        public override Version Version => new Version(1, 1, 2, 1);

        private Timer checkTimer;
        private Timer dbTimer;

        public PermaBuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
            ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
            ServerApi.Hooks.ServerLeave.Register(this, OnLeave);
            ServerApi.Hooks.GamePostInitialize.Register(this, OnPostInitialize);
            ServerApi.Hooks.NetGreetPlayer.Register(this, OnGreet);

            checkTimer = new System.Timers.Timer(2000);
            checkTimer.Elapsed += OnCheckBuffs;
            checkTimer.Start();

            dbTimer = new System.Timers.Timer(300000);
            dbTimer.Elapsed += OnSaveAll;
            dbTimer.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
                ServerApi.Hooks.ServerJoin.Deregister(this, OnJoin);
                ServerApi.Hooks.ServerLeave.Deregister(this, OnLeave);
                ServerApi.Hooks.GamePostInitialize.Deregister(this, OnPostInitialize);
                ServerApi.Hooks.NetGreetPlayer.Deregister(this, OnGreet);

                checkTimer?.Dispose();
                dbTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void OnInitialize(EventArgs args)
        {
            Commands.ChatCommands.Add(new Command("permabuff.use", PermabuffCommand, "pbenable", "pbdisable")
            {
                HelpText = "Enables or disables permabuffs for your character."
            });
        }

        private void OnPostInitialize(EventArgs args)
        {
            DB.LoadAll();
        }

        private void OnJoin(JoinEventArgs args)
        {
            DB.EnsurePlayer(args.Who);
        }

        private void OnLeave(LeaveEventArgs args)
        {
            DB.Save(args.Who);
        }

        private void OnGreet(GreetPlayerEventArgs args)
        {
            DB.Load(args.Who);
        }

        private void OnCheckBuffs(object sender, ElapsedEventArgs args)
        {
            foreach (TSPlayer player in TShock.Players)
            {
                if (player == null || !player.Active || !DB.Enabled(player.Index))
                    continue;

                var piggyBank = Main.player[player.Index].bank.item;
                var buffsToApply = new List<int>();

                foreach (var item in piggyBank)
                {
                    if (item == null || item.stack < 30 || string.IsNullOrWhiteSpace(item.Name))
                        continue;

                    if (Potions.buffMap.TryGetValue(item.Name.ToLowerInvariant(), out int buffId))
                    {
                        if (!Main.player[player.Index].HasBuff(buffId))
                        {
                            buffsToApply.Add(buffId);
                        }
                    }
                }

                foreach (var buff in buffsToApply)
                {
                    Main.player[player.Index].AddBuff(buff, 240);
                }
            }
        }

        private void OnSaveAll(object sender, ElapsedEventArgs args)
        {
            DB.SaveAll();
        }

        private void PermabuffCommand(CommandArgs args)
        {
            if (args.Parameters.Count != 0)
            {
                args.Player.SendErrorMessage("Usage: /pbenable or /pbdisable");
                return;
            }

            if (args.Message.Contains("pbenable"))
            {
                DB.SetEnabled(args.Player.Index, true);
                args.Player.SendSuccessMessage("Permabuffs enabled.");
            }
            else if (args.Message.Contains("pbdisable"))
            {
                DB.SetEnabled(args.Player.Index, false);
                args.Player.SendSuccessMessage("Permabuffs disabled.");
            }
        }
    }
}