using System;
using System.Collections.Generic;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class PermaBuffs : TerrariaPlugin
    {
        public override string Name => "PermaBuffs";
        public override string Author => "SyntaxVoid + updated by Gian";
        public override string Description => "Applies permanent buffs if 30+ buff items are in Piggy Bank";
        public override Version Version => new Version(1, 0, 0, 0);

        private static DateTime lastLogTime = DateTime.MinValue;

        private static Dictionary<int, bool> EnabledUsers = new();
        private static Dictionary<int, DateTime> LastBuffApplied = new();

        public PermaBuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
            Commands.ChatCommands.Add(new Command("permabuff.toggle", TogglePermaBuffs, "pbenable", "pbdisable"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                ServerApi.Hooks.GameUpdate.Deregister(this, OnUpdate);
            base.Dispose(disposing);
        }

        private void TogglePermaBuffs(CommandArgs args)
        {
            int userId = args.Player.Index;
            if (!EnabledUsers.ContainsKey(userId))
                EnabledUsers[userId] = false;

            if (args.Message.Contains("pbenable"))
            {
                EnabledUsers[userId] = true;
                args.Player.SendSuccessMessage("Permabuffs enabled.");
            }
            else if (args.Message.Contains("pbdisable"))
            {
                EnabledUsers[userId] = false;
                args.Player.SendSuccessMessage("Permabuffs disabled.");
            }
        }

        private void OnUpdate(EventArgs args)
        {
            foreach (TSPlayer tsPlayer in TShock.Players)
            {
                if (tsPlayer == null || !tsPlayer.Active || tsPlayer.TPlayer == null || !tsPlayer.TPlayer.active)
                    continue;

                if (!EnabledUsers.TryGetValue(tsPlayer.Index, out bool enabled) || !enabled)
                    continue;

                Player player = tsPlayer.TPlayer;

                if (!LastBuffApplied.TryGetValue(tsPlayer.Index, out DateTime last) || (DateTime.UtcNow - last).TotalSeconds >= 5)
                {
                    LastBuffApplied[tsPlayer.Index] = DateTime.UtcNow;

                    var buffsToApply = Potions.GetBuffsFromPiggyBank(player);

                    if (buffsToApply.Count == 0)
                    {
                        if ((DateTime.UtcNow - lastLogTime).TotalSeconds > 2)
                        {
                            TShock.Log.ConsoleInfo($"[PB] No valid buffs found in Piggy Bank for {tsPlayer.Name}.");
                            lastLogTime = DateTime.UtcNow;
                        }
                        continue;
                    }

                    foreach (int buffID in buffsToApply)
                    {
                        if (!Array.Exists(player.buffType, b => b == buffID))
                        {
                            if (player.buffCount < player.buffType.Length)
                            {
                                player.buffType[player.buffCount] = buffID;
                                player.buffTime[player.buffCount] = 3600;
                                player.buffCount++;

                                NetMessage.SendData(55, -1, -1, null, tsPlayer.Index, buffID);

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
        } // 👈 MISSING BRACE FIXED
    }     // 👈 MISSING BRACE FIXED
}