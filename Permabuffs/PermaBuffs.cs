using System;
using System.Collections.Generic;
using System.Timers;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class PermaBuffs : TerrariaPlugin
    {
        public override string Author => "Myoni";
        public override string Description => "Permabuffs using your piggy bank!";
        public override string Name => "PermaBuffs";
        public override Version Version => new Version(1, 1, 0, 0);

        public static List<int> EnabledPlayers = new List<int>();
        private static System.Timers.Timer updateTimer;
        private static System.Timers.Timer updateSaveTimer;

        public PermaBuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            Commands.ChatCommands.Add(new Command("permbuff.use", BuffCommand, "pbenable", "pbdisable"));
            ServerApi.Hooks.GameInitialize.Register(this, OnGameInit);
            ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
            ServerApi.Hooks.ServerLeave.Register(this, OnLeave);

            updateTimer = new System.Timers.Timer(5000);
            updateTimer.Elapsed += UpdateBuffs;
            updateTimer.Start();

            updateSaveTimer = new System.Timers.Timer(60000);
            updateSaveTimer.Elapsed += SaveEnabledPlayers;
            updateSaveTimer.Start();
        }

        private void OnGameInit(EventArgs args)
        {
            DB.LoadEnabledPlayers();
        }

        private void OnJoin(JoinEventArgs args)
        {
            if (DB.IsPlayerEnabled(args.Who))
                EnabledPlayers.Add(args.Who);
        }

        private void OnLeave(LeaveEventArgs args)
        {
            EnabledPlayers.Remove(args.Who);
        }

        private void BuffCommand(CommandArgs args)
        {
            if (args.Parameters.Count == 0)
            {
                args.Player.SendInfoMessage("Usage: /pbenable or /pbdisable");
                return;
            }

            var cmd = args.Parameters[0].ToLower();

            if (cmd == "pbenable")
            {
                if (!EnabledPlayers.Contains(args.Player.Index))
                {
                    EnabledPlayers.Add(args.Player.Index);
                    DB.SetPlayerEnabled(args.Player.Index, true);
                    args.Player.SendSuccessMessage("PermaBuffs enabled.");
                }
                else
                {
                    args.Player.SendInfoMessage("PermaBuffs is already enabled.");
                }
            }
            else if (cmd == "pbdisable")
            {
                if (EnabledPlayers.Contains(args.Player.Index))
                {
                    EnabledPlayers.Remove(args.Player.Index);
                    DB.SetPlayerEnabled(args.Player.Index, false);
                    args.Player.SendSuccessMessage("PermaBuffs disabled.");
                }
                else
                {
                    args.Player.SendInfoMessage("PermaBuffs is already disabled.");
                }
            }
            else
            {
                args.Player.SendErrorMessage("Unknown command. Use /pbenable or /pbdisable.");
            }
        }

        private void UpdateBuffs(object sender, ElapsedEventArgs e)
        {
            foreach (var playerIndex in EnabledPlayers)
            {
                TSPlayer tsPlayer = TShock.Players[playerIndex];
                if (tsPlayer == null || !tsPlayer.Active || tsPlayer.TPlayer == null || !tsPlayer.TPlayer.active)
                    continue;

                Player tPlayer = tsPlayer.TPlayer;
                var buffsToApply = Potions.GetBuffsFromPiggyBank(tPlayer);

                foreach (var buffId in buffsToApply)
                {
                    bool alreadyHasBuff = false;
                    for (int i = 0; i < Player.MaxBuffs; i++)
                    {
                        if (tPlayer.buffType[i] == buffId)
                        {
                            alreadyHasBuff = true;
                            break;
                        }
                    }

                    if (!alreadyHasBuff)
                        tPlayer.AddBuff(buffId, 60 * 10); // 10 seconds
                }
            }
        }

        private void SaveEnabledPlayers(object sender, ElapsedEventArgs e)
        {
            DB.SaveEnabledPlayers(EnabledPlayers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnGameInit);
                ServerApi.Hooks.ServerJoin.Deregister(this, OnJoin);
                ServerApi.Hooks.ServerLeave.Deregister(this, OnLeave);

                if (updateTimer != null)
                {
                    updateTimer.Stop();
                    updateTimer.Dispose();
                }

                if (updateSaveTimer != null)
                {
                    updateSaveTimer.Stop();
                    updateSaveTimer.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}