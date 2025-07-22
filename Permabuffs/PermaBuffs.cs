using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class Permabuffs : TerrariaPlugin
    {
        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public override string Name => "Permabuffs";
        public override string Author => "SyntaxVoid (Myoni)";
        public override string Description => "Permanent buffs from items in your piggy bank.";

        private readonly Dictionary<int, bool> toggles = new();

        public Permabuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            Commands.ChatCommands.Add(new Command("permabuffs.toggle", Toggle, "pbenable", "pbdisable"));
            ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
            ServerApi.Hooks.ServerLeave.Register(this, OnLeave);
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
        }

        private void Toggle(CommandArgs args)
        {
            int idx = args.Player.Index;
            bool enabled = toggles.ContainsKey(idx) && toggles[idx];
            toggles[idx] = !enabled;
            args.Player.SendSuccessMessage(enabled ? "Permabuffs disabled." : "Permabuffs enabled.");
        }

        private void OnJoin(JoinEventArgs args) => toggles[args.Who] = true;
        private void OnLeave(LeaveEventArgs args) => toggles.Remove(args.Who);

        private void OnUpdate(EventArgs _)
        {
            foreach (var player in TShock.Players)
            {
                if (player == null || !player.Active) continue;
                if (!toggles.TryGetValue(player.Index, out bool on) || !on) continue;

                var items = player.TPlayer.bank?.item;
                if (items == null) continue;

                var granted = new HashSet<int>();
                foreach (var item in items)
                {
                    if (item?.stack < 30) continue;
                    if (Potions.BuffMap.TryGetValue(item.Name.ToLowerInvariant(), out int buff)
                        && !granted.Contains(buff))
                    {
                        player.SetBuff(buff, 3600);
                        granted.Add(buff);
                        TShock.Log.ConsoleInfo($"[Permabuffs] {player.Name} got buff #{buff}");
                    }
                }
            }
        }
    }
}