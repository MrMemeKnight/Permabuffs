using System;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using System.Collections.Generic;
using System.Linq;
using Permabuffs;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class PermaBuffs : TerrariaPlugin
    {
        public override string Name => "PermaBuffs";
        public override Version Version => new Version(1, 0);
        public override string Author => "SyntaxVoid (Modified)";
        public override string Description => "Allows permanent buffs from potions stored in Piggy Bank";

        public PermaBuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            DB.Connect();
            Commands.ChatCommands.Add(new Command("permabuffs.manage", PermaBuffCommand, "permabuff"));

            // TEMPORARILY DISABLED: This may be the cause of the crash on ARM64
            // ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
        }

        private void PermaBuffCommand(CommandArgs args)
        {
            var player = args.Player;

            if (args.Parameters.Count < 1)
            {
                player.SendInfoMessage("Usage: /permabuff <add/remove> <buffname>");
                return;
            }

            int buffId;
            switch (args.Parameters[0].ToLower())
            {
                case "add":
                    if (args.Parameters.Count < 2)
                    {
                        player.SendErrorMessage("Specify a buff name to add.");
                        return;
                    }
                    if (!Potions.buffIDs.TryGetValue(args.Parameters[1].ToLower(), out buffId))
                    {
                        player.SendErrorMessage("Invalid buff name.");
                        return;
                    }
                    DB.AddBuff(player.Account.ID, buffId);
                    player.SendSuccessMessage($"Added permanent buff: {args.Parameters[1]}");
                    break;

                case "remove":
                    if (args.Parameters.Count < 2)
                    {
                        player.SendErrorMessage("Specify a buff name to remove.");
                        return;
                    }
                    if (!Potions.buffIDs.TryGetValue(args.Parameters[1].ToLower(), out buffId))
                    {
                        player.SendErrorMessage("Invalid buff name.");
                        return;
                    }
                    DB.RemoveBuff(player.Account.ID, buffId);
                    player.SendSuccessMessage($"Removed permanent buff: {args.Parameters[1]}");
                    break;

                default:
                    player.SendErrorMessage("Unknown subcommand.");
                    break;
            }
        }

        // OnUpdate intentionally removed from hook registration
        private void OnUpdate(EventArgs args)
        {
            foreach (var player in TShock.Players.Where(p => p != null && p.Active && p.RealPlayer))
            {
                var buffs = DB.GetBuffs(player.Account.ID);
                foreach (var buffId in buffs)
                {
                    if (!player.TPlayer.buffType.Contains(buffId))
                    {
                        player.SetBuff(buffId, 60 * 2); // 2 seconds
                    }
                }
            }
        }
    }
}