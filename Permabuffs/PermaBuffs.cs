using Microsoft.Xna.Framework;
using OTAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class PermaBuffs : TerrariaPlugin
    {
        public override string Author => "SyntaxVoid";
        public override string Description => "Permanent potion buffs!";
        public override string Name => "PermaBuffs";
        public override Version Version => new Version(1, 1, 1);

        private CancellationTokenSource? tokenSource;
        private Task? refreshTask;

        public PermaBuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GamePostInitialize.Register(this, OnPostInitialize);
            ServerApi.Hooks.ServerLeave.Register(this, OnLeave);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GamePostInitialize.Deregister(this, OnPostInitialize);
                ServerApi.Hooks.ServerLeave.Deregister(this, OnLeave);
                tokenSource?.Cancel();
            }
            base.Dispose(disposing);
        }

        private void OnLeave(LeaveEventArgs args)
        {
            PermabuffManager.Remove(args.Who);
        }

        private void OnPostInitialize(EventArgs args)
        {
            Potions.Initialize();
            DB.Load();
            tokenSource = new CancellationTokenSource();
            refreshTask = RunPeriodicBuffsAsync(tokenSource.Token);
        }

        private async Task RunPeriodicBuffsAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    foreach (TSPlayer player in TShock.Players)
                    {
                        if (player == null || !player.Active)
                            continue;

                        BuffManager.GiveBuffs(player);
                    }

                    await Task.Delay(1500, token);
                }
            }
            catch (TaskCanceledException)
            {
                // Graceful cancellation
            }
            catch (Exception ex)
            {
                TShock.Log.ConsoleError($"[PermaBuffs] Error in buff loop: {ex}");
            }
        }
    }
}