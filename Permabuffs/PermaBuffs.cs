using System;
using System.Collections.Generic;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using Microsoft.Xna.Framework;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class Permabuffs : TerrariaPlugin
    {
        public override string Name => "PermaBuffs";
        public override string Author => "SyntaxVoid (Myoni)";
        public override string Description => "Automatically gives players buffs based on items in their piggy bank.";
        public override Version Version => new Version(1, 0, 0, 0);

        public Permabuffs(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
            Commands.ChatCommands.Add(new Command("permabuffs.use", EnableBuffs, "pbenable"));
            Commands.ChatCommands.Add(new Command("permabuffs.use", DisableBuffs, "pbdisable"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameUpdate.Deregister(this, OnUpdate);
            }
            base.Dispose(disposing);
        }

        private void EnableBuffs(CommandArgs args)
        {
            DB.ToggleBuffs(args.Player.Account.ID, true);
            args.Player.SendSuccessMessage("PermaBuffs enabled.");
        }

        private void DisableBuffs(CommandArgs args)
        {
            DB.ToggleBuffs(args.Player.Account.ID, false);
            args.Player.SendSuccessMessage("PermaBuffs disabled.");
        }

        private void OnUpdate(EventArgs args)
        {
            foreach (TSPlayer player in TShock.Players)
            {
                if (player == null || !player.Active || player.TPlayer == null || !player.TPlayer.active)
                    continue;

                if (!DB.IsEnabled(player.Account.ID))
                    continue;

                List<int> buffList = Potions.GetBuffsFromPiggyBank(player.TPlayer);

                foreach (int buff in buffList)
                {
                    if (!player.TPlayer.HasBuff(buff))
                    {
                        player.TPlayer.AddBuff(buff, 1800); // 30 seconds = 60 * 30 = 1800 ticks
                    }
                }
            }
        }
    }
}