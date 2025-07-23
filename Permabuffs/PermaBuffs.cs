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

        // Track timers per player
        private readonly Dictionary<int, DateTime> lastScanTime = new();
        private readonly Dictionary<int, DateTime> lastBuffTime = new();
        private readonly Dictionary<int, List<int>> detectedBuffs = new();

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
            int id = args.Player.Account.ID;
            if (!enabledPlayers.Contains(id))
                enabledPlayers.Add(id);
        }

        private void EnableCommand(CommandArgs args)
        {
            int id = args.Player.Account.ID;
            if (!enabledPlayers.Contains(id))
            {
                enabledPlayers.Add(id);
                args.Player.SendSuccessMessage("Permabuffs enabled.");
            }
            else
            {
                args.Player.SendInfoMessage("Permabuffs already enabled.");
            }
        }

        private void DisableCommand(CommandArgs args)
        {
            int id = args.Player.Account.ID;
            if (enabledPlayers.Contains(id))
            {
                enabledPlayers.Remove(id);
                args.Player.SendSuccessMessage("Permabuffs disabled.");
            }
            else
            {
                args.Player.SendInfoMessage("Permabuffs already disabled.");
            }
        }

        private void OnUpdate(EventArgs args)
        {
            DateTime now = DateTime.UtcNow;

            foreach (TSPlayer tsplr in TShock.Players)
            {
                if (tsplr == null || !tsplr.Active || tsplr.Account == null)
                    continue;

                int id = tsplr.Account.ID;
                Player plr = tsplr.TPlayer;

                if (!enabledPlayers.Contains(id))
                    continue;

                // Scan piggy bank every 60s
                if (!lastScanTime.ContainsKey(id) || (now - lastScanTime[id]).TotalSeconds >= 60)
                {
                    lastScanTime[id] = now;
                    detectedBuffs[id] = new List<int>();

                    for (int i = 0; i < plr.bank.item.Length; i++)
                    {
                        var item = plr.bank.item[i];
                        if (item == null || item.stack < 30)
                            continue;

                        if (buffMap.TryGetValue(item.Name, out int buffID))
                        {
                            detectedBuffs[id].Add(buffID);
                            tsplr.SendInfoMessage($"[DEBUG] Found {item.Name} in piggy bank -> Buff {buffID}");
                        }
                    }
                }

                // Apply buffs every 30s
                if (!lastBuffTime.ContainsKey(id) || (now - lastBuffTime[id]).TotalSeconds >= 30)
                {
                    lastBuffTime[id] = now;

                    if (!detectedBuffs.ContainsKey(id)) continue;

                    foreach (int buffID in detectedBuffs[id])
                    {
                        bool alreadyHas = false;
                        foreach (int activeBuff in plr.buffType)
                        {
                            if (activeBuff == buffID)
                            {
                                alreadyHas = true;
                                break;
                            }
                        }

                        if (!alreadyHas)
                        {
                            plr.AddBuff(buffID, 60 * 10); // 10 seconds
                            NetMessage.SendData((int)PacketTypes.AddBuff, -1, -1, null, tsplr.Index, buffID, 10f);
                            tsplr.SendInfoMessage($"[DEBUG] Applying buff ID: {buffID}");
                        }
                    }
                }
            }
        }
    }
}