using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class Permabuffs : TerrariaPlugin
    {
        public override string Author => "Myoni (SyntaxVoid)";
        public override string Description => "Gives buffs based on items in Piggy Bank";
        public override string Name => "Permabuffs";
        public override Version Version => new Version(1, 0);

        private Dictionary<int, bool> playerBuffStatus = new Dictionary<int, bool>();

        public Permabuffs(Main game) : base(game)
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
            if (args.Parameters.Count != 0)
            {
                args.Player.SendErrorMessage("Usage: /pbenable OR /pbdisable");
                return;
            }

            if (!playerBuffStatus.ContainsKey(args.Player.Account.ID))
            {
                playerBuffStatus.Add(args.Player.Account.ID, false);
            }

            if (args.Message.Equals("/pbenable", StringComparison.OrdinalIgnoreCase))
            {
                playerBuffStatus[args.Player.Account.ID] = true;
                args.Player.SendSuccessMessage("Permabuffs enabled.");
            }
            else if (args.Message.Equals("/pbdisable", StringComparison.OrdinalIgnoreCase))
            {
                playerBuffStatus[args.Player.Account.ID] = false;
                args.Player.SendSuccessMessage("Permabuffs disabled.");
            }
            else
            {
                args.Player.SendErrorMessage("Usage: /pbenable OR /pbdisable");
            }
        }

        private void OnUpdate(EventArgs args)
        {
            foreach (TSPlayer tsplr in TShock.Players)
            {
                if (tsplr == null || tsplr.Account == null || !playerBuffStatus.ContainsKey(tsplr.Account.ID) || !playerBuffStatus[tsplr.Account.ID])
                    continue;

                var piggyBank = tsplr.TPlayer.bank.item;

                foreach (var kvp in Potions.buffMap)
                {
                    string itemName = kvp.Key;
                    int buffID = kvp.Value;

                    if (!Array.Exists(tsplr.TPlayer.buffType, id => id == buffID))
                    {
                        int itemCount = piggyBank
                            .Where(item => item != null && item.stack >= 30 && item.Name == itemName)
                            .Sum(item => item.stack);

                        if (itemCount >= 30)
                        {
                            tsplr.TPlayer.AddBuff(buffID, 60 * 10); // 10 seconds
                        }
                    }
                }
            }
        }
    }
}