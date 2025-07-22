using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using OTAPI;
using Terraria;
using Terraria.ID;
using TerrariaApi.Server;
using TShockAPI;
using Permabuffs;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class PermaBuffs : TerrariaPlugin
    {
        public override string Name => "PermaBuffs";
        public override Version Version => new Version(1, 1, 0);
        public override string Author => "Myoni";
        public override string Description => "Permanent potion effects for TShock";

        public PermaBuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
            ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
            ServerApi.Hooks.GamePostUpdate.Register(this, OnUpdate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
                ServerApi.Hooks.ServerJoin.Deregister(this, OnJoin);
                ServerApi.Hooks.GamePostUpdate.Deregister(this, OnUpdate);
            }
            base.Dispose(disposing);
        }

        private void OnInitialize(EventArgs args)
        {
            DB.Connect();
            NameToBuffIDs.PopulateBuffIDs();
        }

        private void OnJoin(JoinEventArgs args)
        {
            TSPlayer player = TShock.Players[args.Who];
            if (player != null && player.Active)
            {
                DB.CreatePlayer(player);
            }
        }

        private void OnUpdate(EventArgs args)
        {
            foreach (TSPlayer player in TShock.Players)
            {
                if (player == null || !player.Active || player.TPlayer.dead)
                    continue;

                List<int> permBuffs = DB.GetBuffs(player.Name);
                if (permBuffs == null)
                    continue;

                foreach (int buffID in permBuffs)
                {
                    if (buffID < Main.buffName.Length && !player.TPlayer.HasBuff(buffID))
                    {
                        player.TPlayer.AddBuff(buffID, 18010, true);
                    }
                }
            }
        }
    }
}