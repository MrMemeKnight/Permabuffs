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
        public override string Author => "Myoni (SyntaxVoid)";
        public override string Description => "Automatically applies buffs from potions in piggy bank.";
        public override string Name => "Permabuffs";
        public override Version Version => new Version(1, 0);

        private Timer timer;
        private DB db;

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
                if (timer != null)
                {
                    timer.Stop();
                    timer.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void OnInitialize(EventArgs args)
        {
            db = new DB();

            Commands.ChatCommands.Add(new Command("permabuffs.toggle", TogglePermabuffs, "pbenable", "pbdisable"));

            Potions.PopulateBuffMap();

            timer = new Timer(1000); // 1 second
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void TogglePermabuffs(CommandArgs args)
        {
            if (args.Parameters.Count != 0)
            {
                args.Player.SendErrorMessage("Usage: /pbenable or /pbdisable");
                return;
            }

            var userId = args.Player.Account.ID;
            bool enable = args.Message.ToLower().Contains("enable");

            db.SetEnabled(userId, enable);

            args.Player.SendSuccessMessage(enable ? "Permabuffs enabled." : "Permabuffs disabled.");
        }

        private void OnJoin(JoinEventArgs args)
        {
            int userId = TShock.Players[args.Who]?.Account?.ID ?? -1;
            if (userId != -1 && !db.PlayerExists(userId))
            {
                db.AddPlayer(userId);
            }
        }

        private void OnLeave(LeaveEventArgs args)
        {
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            foreach (TSPlayer player in TShock.Players)
            {
                if (player == null || !player.Active || !player.RealPlayer || player.Account == null)
                    continue;

                int userId = player.Account.ID;
                if (!db.IsEnabled(userId))
                    continue;

                var piggyBank = player.TPlayer.bank.item;

                foreach (var item in piggyBank)
                {
                    if (item == null || item.stack < 30)
                        continue;

                    if (Potions.buffMap.TryGetValue(item.Name.ToLower(), out int buffID))
                    {
                        bool hasBuff = false;
                        foreach (var b in player.TPlayer.buffType)
                        {
                            if (b == buffID)
                            {
                                hasBuff = true;
                                break;
                            }
                        }

                        if (!hasBuff)
                        {
                            player.TPlayer.AddBuff(buffID, 60 * 60 * 5); // 5 minutes
                        }
                    }
                }
            }
        }
    }
}