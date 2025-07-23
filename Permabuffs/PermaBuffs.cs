using System;
using System.Collections.Generic;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class Permabuffs : TerrariaPlugin
    {
        public override string Author => "SyntaxVoid (Myoni)";
        public override string Description => "A plugin that allows players to gain buffs from potions placed in piggy banks.";
        public override string Name => "Permabuffs";
        public override Version Version => new Version(1, 0, 0, 0);

        private static readonly List<int> enabledPlayers = new List<int>();
        private static readonly Dictionary<string, int> buffMap = Potions.BuffMap;

        public Permabuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);
            PlayerHooks.PlayerPostLogin += OnLogin;

            Commands.ChatCommands.Add(new Command("permabuffs.use", EnableCommand, "pbenable"));
            Commands.ChatCommands.Add(new Command("permabuffs.use", DisableCommand, "pbdisable"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameUpdate.Deregister(this, OnUpdate);
                PlayerHooks.PlayerPostLogin -= OnLogin;
            }
            base.Dispose(disposing);
        }

        private void OnLogin(PlayerPostLoginEventArgs args)
        {
            if (!enabledPlayers.Contains(args.Player.Account.ID))
                enabledPlayers.Add(args.Player.Account.ID);
        }

        private void EnableCommand(CommandArgs args)
        {
            var tsplr = args.Player;
            if (!enabledPlayers.Contains(tsplr.Account.ID))
            {
                enabledPlayers.Add(tsplr.Account.ID);
                tsplr.SendSuccessMessage("Permabuffs enabled.");
            }
            else
            {
                tsplr.SendInfoMessage("Permabuffs are already enabled.");
            }
        }

        private void DisableCommand(CommandArgs args)
        {
            var tsplr = args.Player;
            if (enabledPlayers.Contains(tsplr.Account.ID))
            {
                enabledPlayers.Remove(tsplr.Account.ID);
                tsplr.SendSuccessMessage("Permabuffs disabled.");
            }
            else
            {
                tsplr.SendInfoMessage("Permabuffs are already disabled.");
            }
        }

        private void OnUpdate(EventArgs args)
        {
            foreach (TSPlayer tsplr in TShock.Players)
            {
                if (tsplr == null || !tsplr.Active || tsplr.Account == null)
                    continue;

                Player plr = tsplr.TPlayer;

                if (!enabledPlayers.Contains(tsplr.Account.ID))
                    continue;

                TSPlayer.Server.SendInfoMessage($"[Permabuffs] Checking {tsplr.Name}'s piggy bank...");

                for (int i = 0; i < plr.bank.item.Length; i++)
                {
                    var item = plr.bank.item[i];
                    if (item == null || item.stack < 30)
                        continue;

                    TSPlayer.Server.SendInfoMessage($"[Permabuffs] Found item: {item.Name} x{item.stack}");

                    if (buffMap.TryGetValue(item.Name, out int buffID))
                    {
                        TSPlayer.Server.SendInfoMessage($"[Permabuffs] Item '{item.Name}' maps to BuffID {buffID}");

                        bool hasBuff = false;
                        for (int j = 0; j < plr.buffType.Length; j++)
                        {
                            if (plr.buffType[j] == buffID)
                            {
                                hasBuff = true;
                                TSPlayer.Server.SendInfoMessage($"[Permabuffs] Player already has buff {buffID}");
                                break;
                            }
                        }

                        if (!hasBuff)
                        {
                            plr.AddBuff(buffID, 60 * 10);
                            TSPlayer.Server.SendInfoMessage($"[Permabuffs] Applied buff {buffID} to {tsplr.Name}");
                        }
                    }
                    else
                    {
                        TSPlayer.Server.SendInfoMessage($"[Permabuffs] Item '{item.Name}' not found in buff map");
                    }
                }
            }
        }
    }
}