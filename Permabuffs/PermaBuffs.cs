using System.Collections.Generic;
using Microsoft.Xna.Framework;
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
        public override string Author => "Myoni (SyntaxVoid), modified";
        public override string Description => "Automatically grants permanent buffs from potions in piggy bank.";

        private static readonly Dictionary<int, bool> playerToggles = new();

        public Permabuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
            Commands.ChatCommands.Add(new Command("permabuffs.enable", EnableCommand, "pbenable"));
            Commands.ChatCommands.Add(new Command("permabuffs.disable", DisableCommand, "pbdisable"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameUpdate.Deregister(this, OnUpdate);
            }
            base.Dispose(disposing);
        }

        private void EnableCommand(CommandArgs args)
        {
            playerToggles[args.Player.Index] = true;
            args.Player.SendSuccessMessage("Permabuffs enabled.");
        }

        private void DisableCommand(CommandArgs args)
        {
            playerToggles[args.Player.Index] = false;
            args.Player.SendSuccessMessage("Permabuffs disabled.");
        }

        private void OnUpdate(EventArgs args)
        {
            TShock.Log.ConsoleInfo("[Permabuffs] OnUpdate triggered.");

            foreach (TSPlayer player in TShock.Players)
            {
                if (player == null || !player.Active)
                    continue;

                if (!playerToggles.TryGetValue(player.Index, out bool enabled) || !enabled)
                {
                    TShock.Log.ConsoleInfo($"[Permabuffs] {player.Name} not enabled.");
                    continue;
                }

                var piggyBank = player.TPlayer.bank?.item;
                if (piggyBank == null)
                {
                    TShock.Log.ConsoleInfo($"[Permabuffs] {player.Name}'s piggy bank is null.");
                    continue;
                }

                TShock.Log.ConsoleInfo($"[Permabuffs] Piggy item count: {piggyBank.Length}");

                foreach (var item in piggyBank)
                {
                    if (item == null || item.stack < 30)
                        continue;

                    string lowerName = item.Name.ToLower();
                    TShock.Log.ConsoleInfo($"[Permabuffs] Scanning item: {item.Name} x{item.stack}");
                    TShock.Log.ConsoleInfo($"[Permabuffs] Checking {lowerName}");

                    if (Potions.BuffMap.TryGetValue(lowerName, out int buffId))
                    {
                        TShock.Log.ConsoleInfo($"[Permabuffs] Applying buff {buffId} to {player.Name}");
                        player.SetBuff(buffId, 3600);
                    }
                }
            }
        }
    }
}