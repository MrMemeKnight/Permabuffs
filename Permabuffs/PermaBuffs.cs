using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class PermaBuffs : TerrariaPlugin
    {
        public override string Author => "Myoni, modified for ARM64 by Gian";
        public override string Description => "Gives permanent buffs based on potions/food in piggy bank.";
        public override string Name => "PermaBuffs";
        public override Version Version => new Version(1, 0, 0, 0);

        private static Dictionary<string, int> Potions = new Dictionary<string, int>();

        public PermaBuffs(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
            TShockAPI.Hooks.PlayerHooks.PlayerPostLogin += OnPlayerLogin;

            Potions = PotionData.GetAll(); // from Potions.cs
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameUpdate.Deregister(this, OnUpdate);
                TShockAPI.Hooks.PlayerHooks.PlayerPostLogin -= OnPlayerLogin;
            }
            base.Dispose(disposing);
        }

        private void OnPlayerLogin(TShockAPI.Hooks.PlayerPostLoginEventArgs args)
        {
            args.Player.SetData("permabuffs_enabled", true);
        }

        private void OnUpdate(EventArgs args)
        {
            foreach (TSPlayer tsplayer in TShock.Players)
            {
                if (tsplayer == null || !tsplayer.Active)
                    continue;

                bool enabled = tsplayer.GetData<bool>("permabuffs_enabled");

                if (!enabled)
                    continue;

                var player = tsplayer.TPlayer;

                var piggyBank = player.bank.item;

                foreach (var potion in Potions)
                {
                    string potionName = potion.Key.ToLowerInvariant();
                    int buffId = potion.Value;

                    // Check if the player already has the buff
                    if (player.buffType.Contains(buffId))
                        continue;

                    foreach (var item in piggyBank)
                    {
                        if (item == null || item.stack < 30)
                            continue;

                        if (item.Name.ToLowerInvariant() == potionName)
                        {
                            player.AddBuff(buffId, 60 * 2); // apply for 2 ticks (1/3 second)
                            break;
                        }
                    }
                }
            }
        }
    }
}