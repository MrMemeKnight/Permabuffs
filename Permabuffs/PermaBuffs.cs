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
        public override string Description => "Permanently grants buffs based on piggy bank contents.";

        private readonly Dictionary<int, bool> playerToggles = new();

        public Permabuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            try
            {
                TShock.Log.ConsoleInfo("[Permabuffs] Starting initialization...");

                Commands.ChatCommands.Add(new Command("permabuffs.toggle", TogglePermabuffs, "pbenable", "pbdisable"));
                ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
                ServerApi.Hooks.ServerLeave.Register(this, OnLeave);
                ServerApi.Hooks.GameUpdate.Register(this, OnUpdate);

                TShock.Log.ConsoleInfo("[Permabuffs] Successfully initialized.");
            }
            catch (Exception ex)
            {
                TShock.Log.ConsoleError($"[Permabuffs] Crash during Initialize: {ex}");
                throw;
            }
        }

        private void TogglePermabuffs(CommandArgs args)
        {
            int index = args.Player.Index;
            bool isEnabled = playerToggles.ContainsKey(index) && playerToggles[index];

            playerToggles[index] = !isEnabled;
            args.Player.SendSuccessMessage(isEnabled ? "Permabuffs disabled." : "Permabuffs enabled.");
        }

        private void OnJoin(JoinEventArgs args)
        {
            playerToggles[args.Who] = true;
        }

        private void OnLeave(LeaveEventArgs args)
        {
            playerToggles.Remove(args.Who);
        }

        private void OnUpdate(EventArgs args)
        {
            foreach (TSPlayer player in TShock.Players)
            {
                if (player == null || !player.Active || !playerToggles.TryGetValue(player.Index, out bool enabled) || !enabled)
                    continue;

                var piggyBank = player.TPlayer.bank.item;

                HashSet<int> grantedBuffs = new();
                foreach (var item in piggyBank)
                {
                    if (item == null || item.stack < 30)
                        continue;

                    if (Potions.BuffMap.TryGetValue(item.Name.ToLower(), out int buffId))
                    {
                        if (!grantedBuffs.Contains(buffId))
                        {
                            player.SetBuff(buffId, 3600, quiet: true);
                            grantedBuffs.Add(buffId);
                        }
                    }
                }
            }
        }
    }
}