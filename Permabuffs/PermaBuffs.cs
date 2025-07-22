using System;
using System.Timers;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class PermaBuffs : TerrariaPlugin
    {
        private Timer _timer;
        private DB _db;

        public override string Name => "Permabuffs";
        public override Version Version => new Version(1, 0);
        public override string Author => "SyntaxVoid (Myoni)";
        public override string Description => "Grants permanent buffs from piggy bank.";

        public PermaBuffs(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            _db = new DB(TShock.DB); // ✅ Fixed: pass existing IDbConnection
            Commands.ChatCommands.Add(new Command("permabuffs.toggle", TogglePermabuffs, "pbenable", "pbdisable"));
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);

            _timer = new Timer(5000); // check every 5 seconds
            _timer.Elapsed += CheckBuffs;
            _timer.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameUpdate.Deregister(this, OnUpdate);
                _timer.Dispose();
            }
            base.Dispose(disposing);
        }

        private void OnUpdate(EventArgs args)
        {
            // Could be used for future updates
        }

        private void TogglePermabuffs(CommandArgs args)
        {
            bool enable = args.Message.ToLower().Contains("enable");
            _db.SetEnabled(args.Player.UserID, enable);
            args.Player.SendSuccessMessage($"Permabuffs {(enable ? "enabled" : "disabled")}.");
        }

        private void CheckBuffs(object sender, ElapsedEventArgs e)
        {
            foreach (TSPlayer player in TShock.Players)
            {
                if (player == null || !player.Active || !player.RealPlayer)
                    continue;

                if (!_db.IsEnabled(player.UserID))
                    continue;

                foreach (var entry in Potions.BuffMap)
                {
                    int itemId = entry.Key;
                    int buffId = entry.Value;

                    int count = 0;

                    for (int i = 0; i < player.TPlayer.bank.item.Length; i++)
                    {
                        var item = player.TPlayer.bank.item[i];
                        if (item != null && item.type == itemId)
                        {
                            count += item.stack;
                            if (count >= 30)
                            {
                                player.SetBuff(buffId, 60 * 60);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}