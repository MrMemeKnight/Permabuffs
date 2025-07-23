using Microsoft.Xna.Framework;
using OTAPI;
using System;
using System.Collections.Generic;
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
        public override string Author => "SyntaxVoid (fixed for ARM64 by user)";
        public override string Description => "Automatically gives players buffs if they have 30+ of the related item in their piggy bank.";

        private Dictionary<int, bool> enabledPlayers = new Dictionary<int, bool>();

        public Permabuffs(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
            Commands.ChatCommands.Add(new Command("permabuffs.toggle", TogglePermabuffs, "pbenable", "pbdisable"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameUpdate.Deregister(this, OnUpdate);
            }
            base.Dispose(disposing);
        }

        private void TogglePermabuffs(CommandArgs args)
        {
            var player = args.Player;
            if (!enabledPlayers.ContainsKey(player.Index))
            {
                enabledPlayers[player.Index] = false;
            }

            if (args.Message == "/pbenable")
            {
                enabledPlayers[player.Index] = true;
                player.SendSuccessMessage("Permabuffs enabled.");
            }
            else if (args.Message == "/pbdisable")
            {
                enabledPlayers[player.Index] = false;
                player.SendSuccessMessage("Permabuffs disabled.");
            }
        }

        private void OnUpdate(EventArgs args)
        {
            foreach (var player in TShock.Players)
            {
                if (player == null || !player.Active || !player.ConnectionAlive || !player.RealPlayer)
                    continue;

                if (!enabledPlayers.TryGetValue(player.Index, out bool enabled) || !enabled)
                    continue;

                var tsPlayer = player;
                var terrariaPlayer = Main.player[tsPlayer.Index];

                var buffMap = Potions.GetBuffMap();
                HashSet<int> appliedBuffs = new HashSet<int>();

                foreach (var item in terrariaPlayer.bank.item)
                {
                    if (item == null || item.stack < 30 || string.IsNullOrEmpty(item.Name))
                        continue;

                    if (buffMap.TryGetValue(item.Name, out int buffId))
                    {
                        if (!appliedBuffs.Contains(buffId))
                        {
                            terrariaPlayer.AddBuff(buffId, 60 * 60); // Apply for 60 seconds
                            appliedBuffs.Add(buffId);
                            TShock.Log.Info($"[PB] Applied buff {buffId} to {tsPlayer.Name}");
                        }
                    }
                }
            }
        }
    }
}