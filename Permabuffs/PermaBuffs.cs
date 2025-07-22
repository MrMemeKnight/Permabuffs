using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using OTAPI;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace Permabuffs
{
    [ApiVersion(2, 1)]
    public class PermaBuffs : TerrariaPlugin
    {
        public override string Author => "SyntaxVoid (Myoni)";
        public override string Description => "Permabuffs using Piggy Bank potion stacks.";
        public override string Name => "Permabuffs";
        public override Version Version => new Version(1, 0, 0);

        private static Dictionary<int, bool> EnabledUsers = new();

        public PermaBuffs(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            GeneralHooks.ReloadEvent += OnReload;
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
            ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
            ServerApi.Hooks.ServerLeave.Register(this, OnLeave);
            ServerApi.Hooks.NetGetData.Register(this, OnGetData);
        }

        private void OnReload(ReloadEventArgs args)
        {
            EnabledUsers.Clear();
        }

        private void OnInitialize(EventArgs args)
        {
            DB.Connect();

            Commands.ChatCommands.Add(new Command("permabuff.toggle", ToggleBuffs, "pbenable", "pbdisable"));
        }

        private void ToggleBuffs(CommandArgs args)
        {
            int userId = args.Player.Account.ID;

            if (!EnabledUsers.TryGetValue(userId, out bool enabled))
            {
                enabled = false;
            }

            if (args.Message.Contains("disable"))
            {
                EnabledUsers[userId] = false;
                args.Player.SendSuccessMessage("Permabuffs disabled.");
            }
            else
            {
                EnabledUsers[userId] = true;
                args.Player.SendSuccessMessage("Permabuffs enabled.");
            }
        }

        private void OnJoin(JoinEventArgs args)
        {
            int playerIndex = args.Who;
            var player = TShock.Players[playerIndex];

            if (player == null || !player.RealPlayer)
                return;

            int userId = player.Account.ID;

            EnabledUsers[userId] = true;
            DB.CreatePlayer(userId);
        }

        private void OnLeave(LeaveEventArgs args)
        {
            int playerIndex = args.Who;
            var player = TShock.Players[playerIndex];

            if (player == null)
                return;

            int userId = player.Account?.ID ?? -1;

            if (userId > 0)
            {
                EnabledUsers.Remove(userId);
            }
        }

        private void OnGetData(GetDataEventArgs args)
        {
            if (args.MsgID != PacketTypes.PlayerUpdate)
                return;

            var player = TShock.Players[args.Msg.whoAmI];

            if (player == null || !player.RealPlayer || player.Account == null)
                return;

            int userId = player.Account.ID;

            if (!EnabledUsers.TryGetValue(userId, out bool enabled) || !enabled)
                return;

            ApplyPermabuffs(player);
        }

        private void ApplyPermabuffs(TSPlayer tsPlayer)
        {
            var piggyBank = tsPlayer.TPlayer.bank.item;
            var buffsGiven = new HashSet<int>();

            foreach (var item in piggyBank)
            {
                if (item == null || item.stack < 30 || string.IsNullOrEmpty(item.Name))
                    continue;

                if (Potions.PotionToBuffId.TryGetValue(item.Name.ToLowerInvariant(), out int buffId))
                {
                    if (!tsPlayer.TPlayer.HasBuff(buffId))
                    {
                        tsPlayer.TPlayer.AddBuff(buffId, 60 * 10); // 10 seconds
                        buffsGiven.Add(buffId);
                    }
                }
            }
        }
    }
}