using System;
using System.Threading;
using System.Threading.Tasks;
using TShockAPI;
using Terraria;

namespace Permabuffs
{
    public class PermaBuffs : TerrariaPlugin
    {
        public override Version Version => new Version(1, 1);
        public override string Name => "Permabuffs";
        public override string Author => "Original by SyntaxVoid | Ported by MrMemeKnight";
        public override string Description => "Provides permanent potion buffs when 30 are in piggy bank";

        public static PermaBuffs Instance;

        private CancellationTokenSource cancelToken;

        public PermaBuffs(Main game) : base(game)
        {
            Instance = this;
        }

        public override void Initialize()
        {
            ServerApi.Hooks.GamePostInitialize.Register(this, OnPostInit);
            ServerApi.Hooks.ServerLeave.Register(this, OnLeave);

            cancelToken = new CancellationTokenSource();
            _ = StartBuffLoopAsync(cancelToken.Token);
        }

        public override void Dispose()
        {
            ServerApi.Hooks.GamePostInitialize.Deregister(this, OnPostInit);
            ServerApi.Hooks.ServerLeave.Deregister(this, OnLeave);

            cancelToken.Cancel();
        }

        private async Task StartBuffLoopAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                foreach (TSPlayer player in TShock.Players)
                {
                    if (player == null || !player.Active || !player.RealPlayer) continue;
                    BuffManager.GiveBuffs(player);
                }

                try
                {
                    await Task.Delay(1500, token); // 1.5s delay
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }
        }

        private void OnPostInit(EventArgs args)
        {
            DB.Connect();
        }

        private void OnLeave(LeaveEventArgs args)
        {
            DB.RemoveOfflinePlayerBuffs(args.Who);
        }
    }
}