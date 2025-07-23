using System;
using System.Collections.Generic;
using System.Linq;
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

        private static Dictionary<int, bool> EnabledUsers = new();

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
            foreach (TSPlayer player in TShock.Players)
{
    if (player?.Active != true || !player.TPlayer.active)
        continue;

    if (!EnabledUsers.TryGetValue(player.Index, out bool enabled) || !enabled)
        continue;

    var buffs = Potions.GetBuffsFromPiggyBank(player.TPlayer);
    foreach (int buffID in buffs)
    {
        player.TPlayer.AddBuff(buffID, 1800);
NetMessage.SendData(55, -1, -1, null, player.Index, buffID);
    }

    // Force the buffs to be updated
    player.TPlayer.UpdateBuffs();
}

}