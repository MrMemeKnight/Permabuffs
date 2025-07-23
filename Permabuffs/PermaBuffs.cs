using System;
using System.Collections.Generic;
using Terraria;
using TShockAPI;
using TerrariaApi.Server;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class Permabuffs : TerrariaPlugin
    {
        public override string Author => "SyntaxVoid, updated by you";
        public override string Description => "Applies permanent buffs if 30 of the potion are in piggy bank";
        public override string Name => "Permabuffs";
        public override Version Version => new Version(1, 0);

        private static Dictionary<int, bool> EnabledForPlayers = new Dictionary<int, bool>();

        public Permabuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
            Commands.ChatCommands.Add(new Command("permabuffs.toggle", TogglePermabuff, "pbenable", "pbdisable"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameUpdate.Deregister(this, OnUpdate);
            }
            base.Dispose(disposing);
        }

        private void TogglePermabuff(CommandArgs args)
        {
            int id = args.Player.Index;

            if (!EnabledForPlayers.ContainsKey(id))
                EnabledForPlayers[id] = false;

            EnabledForPlayers[id] = !EnabledForPlayers[id];
            args.Player.SendSuccessMessage($"Permabuffs {(EnabledForPlayers[id] ? "enabled" : "disabled")}.");
        }

        private void OnUpdate(EventArgs args)
{
    foreach (TSPlayer player in TShock.Players)
    {
        if (player?.Active != true || !player.TPlayer.active)
            continue;

        int id = player.Index;

        if (!EnabledForPlayers.ContainsKey(id) || !EnabledForPlayers[id])
            continue;

        Player tPlayer = player.TPlayer;

        // Make sure piggy bank is loaded
        if (tPlayer.bank?.item == null)
            continue;

        List<int> buffs = Potions.GetBuffsFromPiggyBank(tPlayer);

        foreach (int buffID in buffs)
        {
            bool alreadyHasBuff = false;

            for (int i = 0; i < Player.MaxBuffs; i++)
            {
                if (tPlayer.buffType[i] == buffID)
                {
                    alreadyHasBuff = true;
                    break;
                }
            }

            if (!alreadyHasBuff)
            {
                tPlayer.AddBuff(buffID, 60 * 10); // 10 seconds to ensure overlap with update interval
            }
        }
    }
}