using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;
using System.Collections.Generic;
using System.Linq;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class PermaBuffs : TerrariaPlugin
    {
        public override string Name => "Permabuffs";
        public override string Author => "Myoni (SyntaxVoid)";
        public override string Description => "Grants buffs based on Piggy Bank inventory.";
        public override Version Version => new Version(1, 0);

        public static Dictionary<int, List<int>> PlayerBuffs = new Dictionary<int, List<int>>();
        public static HashSet<int> EnabledPlayers = new HashSet<int>();

        public PermaBuffs(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GameUpdate.Register(this, OnGameUpdate);
            PlayerHooks.PlayerPostLogin += OnPlayerPostLogin;
            Commands.ChatCommands.Add(new Command("permabuffs.toggle", TogglePermabuffs, "pbenable", "pbdisable"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameUpdate.Deregister(this, OnGameUpdate);
                PlayerHooks.PlayerPostLogin -= OnPlayerPostLogin;
            }
            base.Dispose(disposing);
        }

        private void OnPlayerPostLogin(PlayerPostLoginEventArgs args)
        {
            int who = args.Player.Index;
            if (!PlayerBuffs.ContainsKey(who))
                PlayerBuffs[who] = new List<int>();

            EnabledPlayers.Add(who);
        }

        private void TogglePermabuffs(CommandArgs args)
        {
            if (args.CommandName == "pbenable")
            {
                EnabledPlayers.Add(args.Player.Index);
                args.Player.SendSuccessMessage("PermaBuffs enabled.");
            }
            else
            {
                EnabledPlayers.Remove(args.Player.Index);
                args.Player.SendSuccessMessage("PermaBuffs disabled.");
            }
        }

        private void OnGameUpdate(EventArgs args)
        {
            foreach (TSPlayer tsplr in TShock.Players.Where(p => p != null && p.Active))
            {
                int who = tsplr.Index;
                if (!EnabledPlayers.Contains(who)) continue;

                Player player = Main.player[who];
                List<int> buffs = new List<int>();

                foreach (Item item in player.bank.item)
                {
                    if (item != null && item.stack >= 30 && Potions.buffMap.TryGetValue(item.Name, out int buffId))
                    {
                        if (!player.HasBuff(buffId))
                        {
                            player.AddBuff(buffId, 60 * 10, true); // 10 seconds
                        }
                        buffs.Add(buffId);
                    }
                }

                PlayerBuffs[who] = buffs;
            }
        }
    }
}