using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using OTAPI;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class PermaBuffs : TerrariaPlugin
    {
        public override string Author => "Myoni (SyntaxVoid)";
        public override string Description => "Automatically grants players buffs if they have a stack of 30+ potions in their piggy bank.";
        public override string Name => "PermaBuffs";
        public override Version Version => new Version(1, 0);

        private Timer _timer;
        private Dictionary<int, bool> EnabledPlayers = new Dictionary<int, bool>();

        public PermaBuffs(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
            Commands.ChatCommands.Add(new Command("permabuffs.toggle", ToggleBuffs, "pbenable", "pbdisable"));
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
            if (args.Parameters.Count < 1)
            {
                args.Player.SendInfoMessage("Usage: /pbenable or /pbdisable");
                return;
            }

            bool enable = args.Parameters[0].ToLower() == "pbenable";
            EnabledPlayers[args.Player.Index] = enable;
            args.Player.SendSuccessMessage(enable ? "PermaBuffs enabled!" : "PermaBuffs disabled.");
        }

        private void OnUpdate(EventArgs args)
        {
            foreach (TSPlayer player in TShock.Players.Where(p => p != null && p.Active))
            {
                if (!EnabledPlayers.TryGetValue(player.Index, out bool enabled) || !enabled)
                    continue;

                var inventory = player.TPlayer.bank.item;

                foreach (var item in inventory)
                {
                    if (item == null || item.stack < 30)
                        continue;

                    if (Potions.BuffMap.TryGetValue(item.Name.ToLower(), out int buffID))
                    {
                        if (!player.TPlayer.buffType.Contains(buffID))
                        {
                            player.TPlayer.AddBuff(buffID, 60 * 10); // 10 seconds
                        }
                    }
                }
            }
        }
    }
}