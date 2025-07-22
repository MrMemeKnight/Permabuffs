using System;
using System.Collections.Generic;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using OTAPI;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class PermaBuffs : TerrariaPlugin
    {
        public override string Name => "Permabuffs";
        public override string Author => "Myoni, updated by Gian";
        public override string Description => "Gives permanent buffs if 30 of a potion is in piggy bank.";
        public override Version Version => new Version(1, 0, 0, 0);

        private static Dictionary<int, bool> enabledPlayers = new();

        public PermaBuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
            Commands.ChatCommands.Add(new Command("permabuffs.toggle", Toggle, "pbenable", "pbdisable"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameUpdate.Deregister(this, OnUpdate);
            }
            base.Dispose(disposing);
        }

        private void Toggle(CommandArgs args)
        {
            if (!enabledPlayers.ContainsKey(args.Player.Index))
            {
                enabledPlayers[args.Player.Index] = false;
            }

            string command = args.Message.TrimStart('/').ToLower();

            if (command == "pbenable")
            {
                enabledPlayers[args.Player.Index] = true;
                args.Player.SendSuccessMessage("Permabuffs enabled.");
            }
            else if (command == "pbdisable")
            {
                enabledPlayers[args.Player.Index] = false;
                args.Player.SendSuccessMessage("Permabuffs disabled.");
            }
        }

        private void OnUpdate(EventArgs args)
        {
            foreach (TSPlayer player in TShock.Players)
            {
                if (player == null || !player.Active || !player.RealPlayer)
                    continue;

                if (!enabledPlayers.ContainsKey(player.Index) || !enabledPlayers[player.Index])
                    continue;

                var piggyBank = Main.player[player.Index].bank.item;

                foreach (var item in piggyBank)
                {
                    if (item == null || item.stack < 30 || string.IsNullOrEmpty(item.Name))
                        continue;

                    if (Potions.BuffMap.TryGetValue(item.Name.ToLowerInvariant(), out int buffID))
                    {
                        if (!Main.player[player.Index].HasBuff(buffID))
                        {
                            Main.player[player.Index].AddBuff(buffID, 3600, true);
                        }
                    }
                }
            }
        }
    }
}