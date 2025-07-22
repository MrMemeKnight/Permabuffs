using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using OTAPI;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class Permabuffs : TerrariaPlugin
    {
        public override string Name => "Permabuffs";
        public override Version Version => new Version(1, 0);
        public override string Author => "Myoni (SyntaxVoid)";
        public override string Description => "Piggy bank permabuffs!";

        public Permabuffs(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
            ServerApi.Hooks.ServerJoin.Register(this, OnJoin);

            Commands.ChatCommands.Add(new Command("permbuff", CmdPermabuff, "permabuff"));

            Potions.Initialize();
            DB.Load();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
                ServerApi.Hooks.ServerJoin.Deregister(this, OnJoin);
            }
            base.Dispose(disposing);
        }

        private void OnInitialize(EventArgs args)
        {
            DB.Load();
        }

        private void OnJoin(JoinEventArgs args)
        {
            var player = TShock.Players[args.Who];
            if (player == null || !player.Active)
                return;

            List<int> buffs = DB.Get(args.Who);
            foreach (int buff in buffs)
            {
                player.SetBuff(buff, 60 * 60 * 30); // 30-minute duration (auto-refreshed)
            }
        }

        private void CmdPermabuff(CommandArgs args)
        {
            if (args.Parameters.Count != 1)
            {
                args.Player.SendErrorMessage("Usage: /permabuff <potion name>");
                return;
            }

            string potionName = args.Parameters[0].ToLowerInvariant();

            if (!Potions.NameToBuffIDs.ContainsKey(potionName))
            {
                args.Player.SendErrorMessage("Invalid potion name.");
                return;
            }

            int buffID = Potions.NameToBuffIDs[potionName];
            int itemType = Potions.PotionToItem[potionName];

            var piggy = args.Player.TPlayer.bank.item;

            int totalCount = 0;
            foreach (var item in piggy)
            {
                if (item != null && item.type == itemType)
                {
                    totalCount += item.stack;
                }
            }

            if (totalCount < 30)
            {
                args.Player.SendErrorMessage("You need at least 30 of that potion in your piggy bank.");
                return;
            }

            DB.Add(args.Player.Index, buffID);
            args.Player.SendSuccessMessage("Permanent buff added: " + potionName);

            args.Player.SetBuff(buffID, 60 * 60 * 30);
        }
    }
}