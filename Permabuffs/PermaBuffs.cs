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
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
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

        private void OnUpdate(EventArgs args)
        {
            foreach (var player in TShock.Players.Where(p => p?.Active == true && p.RealPlayer))
            {
                var buffs = DB.GetBuffs(player.Account.ID);

                foreach (var buffId in buffs)
                {
                    if (buffId <= 0 || buffId >= Main.maxBuffTypes)
                        continue;

                    if (IsAmbientOrInvalidBuff(buffId))
                        continue;

                    try
                    {
                        if (!player.TPlayer.buffType.Contains(buffId))
                        {
                            player.SetBuff(buffId, 60 * 2);
                        }
                    }
                    catch (Exception ex)
                    {
                        TShock.Log.ConsoleError($"[Permabuffs] Failed to apply buff {buffId} to {player.Name}: {ex.Message}");
                    }
                }
            }
        }

        private bool IsAmbientOrInvalidBuff(int buffId)
        {
            int[] forbidden = new int[]
            {
                87,  // Campfire
                89,  // Honey
                96,  // Sunflower
                104, // Ammo Box
                105, // Bewitched
                106, // Sharpened
                115, // Peace Candle
                116, // War Table
                117, // Heart Lantern
                148, // Bast Statue
                157, // Star in a Bottle
            };

            return forbidden.Contains(buffId);
        }
    }
}