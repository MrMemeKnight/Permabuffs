using OTAPI;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class Permabuffs : TerrariaPlugin
    {
        public override string Name => "Permabuffs";
        public override string Author => "Myoni (SyntaxVoid) Scuffed update by MK and ChatGPT";
        public override string Description => "Automatically grants buffs from potions, flasks, foods drinks in piggy bank, safe, forge and void vault";
        public override Version Version => new Version(1, 0, 0);

        private Dictionary<int, bool> toggledPlayers = new Dictionary<int, bool>();

        private Dictionary<int, DateTime> lastScanTime = new Dictionary<int, DateTime>();
        private readonly TimeSpan scanInterval = TimeSpan.FromSeconds(5);

        private readonly Dictionary<string, int> BuffDurations = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
       {
            { "Lesser Luck Potion", 18000 }, // 5 minutes
            { "Luck Potion", 36000 },        // 10 minutes
            { "Greater Luck Potion", 54000 } // 15 minutes
       };


        public Permabuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
            Commands.ChatCommands.Add(new Command("permabuffs.toggle", EnablePermabuffs, "pbenable"));
            Commands.ChatCommands.Add(new Command("permabuffs.toggle", DisablePermabuffs, "pbdisable"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameUpdate.Deregister(this, OnUpdate);
            }
            base.Dispose(disposing);
        }

        private void EnablePermabuffs(CommandArgs args)
        {
            int plr = args.Player.Index;
            toggledPlayers[plr] = true;
            args.Player.SendSuccessMessage("Permabuffs enabled!");
        }

        private void DisablePermabuffs(CommandArgs args)
        {
            int plr = args.Player.Index;
            toggledPlayers[plr] = false;
            args.Player.SendSuccessMessage("Permabuffs disabled!");
        }

        private void OnUpdate(EventArgs args)
        {
            foreach (TSPlayer tsPlayer in TShock.Players)
            {
                if (tsPlayer == null || !tsPlayer.Active || tsPlayer.TPlayer.dead || tsPlayer.TPlayer.inventory == null)
                    continue;

                if (lastScanTime.TryGetValue(tsPlayer.Index, out DateTime lastTime))
                {
                    if (DateTime.UtcNow - lastTime < scanInterval)
                        continue; // Skip this player; not enough time has passed
                }

                // Record the current time for next interval
                lastScanTime[tsPlayer.Index] = DateTime.UtcNow;

                int plr = tsPlayer.Index;
                if (!toggledPlayers.ContainsKey(plr) || !toggledPlayers[plr])
                    continue;

                Player player = tsPlayer.TPlayer;

                // Check piggy bank + safe + forge + void vault for qualifying potions
                Dictionary<int, int> buffCounts = new Dictionary<int, int>();

                Dictionary<int, string> buffNames = new Dictionary<int, string>();

                foreach (Item item in EnumerateStorageItems(player))
                {
                    if (item == null || string.IsNullOrWhiteSpace(item.Name))
                        continue;

                    string name = Lang.GetItemNameValue(item.type);
                    if (item.stack >= 30 && Potions.BuffMap.TryGetValue(name, out int buffID))
                    {

                        if (buffID <= 0) continue;

                        buffCounts.TryAdd(buffID, item.stack);

                        if (!buffNames.ContainsKey(buffID))
                             buffNames[buffID] = name;
                    }
                }

                foreach (var kvp in buffCounts)
                {
                    int buffID = kvp.Key;

                    // Skip if the player already has the buff
                    if (player.buffType.Contains(buffID))
                        continue;

                    string sourceName = buffNames.TryGetValue(buffID, out string val) ? val : "";
                    
                    // Only Luck potions show duration, others get hidden (0 duration)
                    int duration = BuffDurations.ContainsKey(sourceName)
                        ? BuffDurations[sourceName]
                        : 0;

                    // Apply buff with optional visible or hidden timer using same method as /buff
                    tsPlayer.SetBuff(buffID, duration, true);
                }
            }
        }

        private IEnumerable<Item> EnumerateStorageItems(Player player)
        {
            foreach (var item in player.bank?.item ?? Array.Empty<Item>()) yield return item;
            foreach (var item in player.bank2?.item ?? Array.Empty<Item>()) yield return item;
            foreach (var item in player.bank3?.item ?? Array.Empty<Item>()) yield return item;
            foreach (var item in player.bank4?.item ?? Array.Empty<Item>()) yield return item;
        }
    }
}