using System;
using System.Collections.Generic;
using System.Timers;
using OTAPI;
using Terraria;
using Terraria.ID;
using TShockAPI;

namespace Permabuffs
{
    public class Permabuffs
    {
        private static Timer _timer;
        private static Dictionary<int, List<int>> _playerBuffs = new();
        private static Dictionary<string, int> _buffMap = new();

        public static void Initialize()
        {
            PopulateBuffMap();
            _timer = new Timer(5000);
            _timer.Elapsed += CheckBuffs;
            _timer.Start();

            Commands.ChatCommands.Add(new Command("permabuffs.toggle", TogglePermabuffs, "pbenable", "pbdisable"));
        }

        private static void TogglePermabuffs(CommandArgs args)
        {
            if (!_playerBuffs.ContainsKey(args.Player.Index))
            {
                _playerBuffs[args.Player.Index] = new List<int>();
            }

            if (args.Message.StartsWith("/pbenable", StringComparison.OrdinalIgnoreCase))
            {
                args.Player.SendSuccessMessage("Permabuffs enabled.");
                return;
            }

            if (args.Message.StartsWith("/pbdisable", StringComparison.OrdinalIgnoreCase))
            {
                _playerBuffs.Remove(args.Player.Index);
                args.Player.SendSuccessMessage("Permabuffs disabled.");
            }
        }

        private static void CheckBuffs(object sender, ElapsedEventArgs e)
        {
            foreach (TSPlayer tsPlayer in TShock.Players)
            {
                if (tsPlayer == null || !tsPlayer.Active || !_playerBuffs.ContainsKey(tsPlayer.Index))
                    continue;

                var player = tsPlayer.TPlayer;

                foreach (Item item in player.bank.item)
                {
                    if (item == null || item.stack < 30)
                        continue;

                    if (_buffMap.TryGetValue(item.Name, out int buffID))
                    {
                        if (!player.HasBuff(buffID, out _))
                        {
                            tsPlayer.SetBuff(buffID, 3600, true);
                        }
                    }
                }
            }
        }

        private static void PopulateBuffMap()
        {
            _buffMap = Potions.GetBuffMap();
        }
    }
}