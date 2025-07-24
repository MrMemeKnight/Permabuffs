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
        public override string Author => "Myoni (SyntaxVoid)";
        public override string Description => "Automatically grants buffs from potions in piggy bank";
        public override Version Version => new Version(1, 0, 0);

        private Dictionary<int, bool> toggledPlayers = new Dictionary<int, bool>();

        public Permabuffs(Main game) : base(game) { }

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
            int plr = args.Player.Index;

            if (!toggledPlayers.ContainsKey(plr))
                toggledPlayers[plr] = false;

            if (args.Message.Contains("pbenable"))
            {
                toggledPlayers[plr] = true;
                args.Player.SendSuccessMessage("Permabuffs enabled!");
            }
            else if (args.Message.Contains("pbdisable"))
            {
                toggledPlayers[plr] = false;
                args.Player.SendSuccessMessage("Permabuffs disabled!");
            }
        }

        private void OnUpdate(EventArgs args)
        {
            foreach (TSPlayer tsPlayer in TShock.Players)
            {
                if (tsPlayer == null || !tsPlayer.Active || tsPlayer.TPlayer.dead || tsPlayer.TPlayer.inventory == null)
                    continue;

                int plr = tsPlayer.Index;
                if (!toggledPlayers.ContainsKey(plr) || !toggledPlayers[plr])
                    continue;

                Player player = tsPlayer.TPlayer;

                // Check piggy bank for qualifying potions
                Dictionary<int, int> buffCounts = new Dictionary<int, int>();

                foreach (Item item in player.bank.item)
                {
                    if (item == null || string.IsNullOrWhiteSpace(item.Name))
                        continue;

                    string name = Lang.GetItemNameValue(item.type);
                    if (item.stack >= 30 && Potions.BuffMap.TryGetValue(name, out int buffID))
                    {
                        if (!buffCounts.ContainsKey(buffID))
                            buffCounts[buffID] = item.stack;
                    }
                }

                foreach (var kvp in buffCounts)
                {
                    int buffID = kvp.Key;

                    if (!player.HasBuff(buffID))
                    {
                        // Apply full duration server-side
                        player.AddBuff(buffID, 54000, true);

                        // Find index of the applied buff
                        int index = player.FindBuffIndex(buffID);
                        if (index != -1)
                        {
                            // Send it to clients with only 1-tick duration to hide timer
                            NetMessage.SendData((int)PacketTypes.PlayerBuff, -1, -1, null, tsPlayer.Index, player.buffType[index], 1);
                            }
                        }
                    }
                }
            }
        }
    }
}