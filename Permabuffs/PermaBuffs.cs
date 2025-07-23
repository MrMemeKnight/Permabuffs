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
        public override string Author => "SyntaxVoid, updated for OTAPI by you";
        public override string Description => "Applies permanent potion buffs from piggy bank items";
        public override string Name => "Permabuffs";
        public override Version Version => new Version(1, 0);

        private Dictionary<int, DateTime> EnabledPlayers = new Dictionary<int, DateTime>();
        private Potions potions;

        public Permabuffs(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
            Commands.ChatCommands.Add(new Command("permabuffs.use", ToggleBuffs, "pbenable", "pbdisable"));
            potions = new Potions();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameUpdate.Deregister(this, OnUpdate);
            }
            base.Dispose(disposing);
        }

        private void ToggleBuffs(CommandArgs args)
        {
            int playerId = args.Player.Index;

            if (args.Message.Trim().ToLower().EndsWith("pbenable"))
            {
                if (!EnabledPlayers.ContainsKey(playerId))
                {
                    EnabledPlayers[playerId] = DateTime.UtcNow;
                    args.Player.SendSuccessMessage("Permabuffs enabled.");
                }
                else
                {
                    args.Player.SendInfoMessage("Permabuffs already enabled.");
                }
            }
            else if (args.Message.Trim().ToLower().EndsWith("pbdisable"))
            {
                if (EnabledPlayers.ContainsKey(playerId))
                {
                    EnabledPlayers.Remove(playerId);
                    args.Player.SendSuccessMessage("Permabuffs disabled.");
                }
                else
                {
                    args.Player.SendInfoMessage("Permabuffs already disabled.");
                }
            }
        }

        private DateTime lastLogTime = DateTime.UtcNow;

        private void OnUpdate(EventArgs args)
        {
            foreach (TSPlayer tsPlayer in TShock.Players)
            {
                if (tsPlayer == null || !tsPlayer.Active || !tsPlayer.RealPlayer)
                    continue;

                int playerId = tsPlayer.Index;
                Player player = Main.player[playerId];

                if (!EnabledPlayers.ContainsKey(playerId))
                    continue;

                foreach (Item item in player.bank?.item ?? Array.Empty<Item>())
                {
                    if (item == null || item.stack < 30)
                        continue;

                    if (potions.TryGetBuffID(item.Name, out int buffID))
                    {
                        if (!player.HasBuff(buffID))
                        {
                            tsPlayer.SetBuff(buffID, 1800); // 30 seconds

                            if ((DateTime.UtcNow - lastLogTime).TotalSeconds > 2)
                            {
                                TShock.Log.ConsoleInfo($"[PB] Applied buff {buffID} to {tsPlayer.Name}");
                                lastLogTime = DateTime.UtcNow;
                            }
                        }
                    }
                }
            }
        }
    }
}