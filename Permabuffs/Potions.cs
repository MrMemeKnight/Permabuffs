using System.Collections.Generic;
using Terraria;
using TShockAPI;

namespace Permabuffs
{
    public class Potions
    {
        public static Dictionary<string, int> buffIDs = new Dictionary<string, int>();

        public static void Initialize()
        {
            buffIDs.Clear();

            // Common Combat Potions
            buffIDs.Add("Regeneration", BuffID.Regeneration);
            buffIDs.Add("Swiftness", BuffID.Swiftness);
            buffIDs.Add("Ironskin", BuffID.Ironskin);
            buffIDs.Add("Endurance", BuffID.Endurance);
            buffIDs.Add("Lifeforce", BuffID.Lifeforce);
            buffIDs.Add("Wrath", BuffID.Wrath);
            buffIDs.Add("Rage", BuffID.Rage);
            buffIDs.Add("Magic Power", BuffID.MagicPower);
            buffIDs.Add("Mana Regeneration", BuffID.ManaRegeneration);
            buffIDs.Add("Archery", BuffID.Archery);
            buffIDs.Add("Ammo Reservation", BuffID.AmmoReservation);
            buffIDs.Add("Summoning", BuffID.Summoning);
            buffIDs.Add("Thorns", BuffID.Thorns);
            buffIDs.Add("Sharpened", BuffID.Sharpened);

            // Exploration & Utility Potions
            buffIDs.Add("Spelunker", BuffID.Spelunker);
            buffIDs.Add("Hunter", BuffID.Hunter);
            buffIDs.Add("Dangersense", BuffID.Dangersense);
            buffIDs.Add("Shine", BuffID.Shine);
            buffIDs.Add("Night Owl", BuffID.NightOwl);
            buffIDs.Add("Mining", BuffID.Mining);
            buffIDs.Add("Builder", BuffID.Builder);
            buffIDs.Add("Calm", BuffID.Calm);
            buffIDs.Add("Luck", BuffID.LuckPotion);

            // Movement & Mobility Potions
            buffIDs.Add("Featherfall", BuffID.Featherfall);
            buffIDs.Add("Gravitation", BuffID.Gravitation);
            buffIDs.Add("Gills", BuffID.Gills);
            buffIDs.Add("Water Walking", BuffID.WaterWalking);
            buffIDs.Add("Obsidian Skin", BuffID.ObsidianSkin);
            buffIDs.Add("Flipper", BuffID.Flipper);
            buffIDs.Add("Invisibility", BuffID.Invisibility);

            // Other Special Potions
            buffIDs.Add("Inferno", BuffID.Inferno);
            buffIDs.Add("Battle", BuffID.Battle);
            buffIDs.Add("Warmth", BuffID.Warmth);
            buffIDs.Add("Titan", BuffID.Titan);
            buffIDs.Add("Heartreach", BuffID.Heartreach);
            buffIDs.Add("Bewitched", BuffID.Bewitched);
            buffIDs.Add("Clairvoyance", BuffID.Clairvoyance);

            // Food Buffs (from actual consumables only)
            buffIDs.Add("Well Fed", BuffID.WellFed);
            buffIDs.Add("Well Fed 2", BuffID.WellFed2);
            buffIDs.Add("Well Fed 3", BuffID.WellFed3);

            // Optional: Alcohol
            buffIDs.Add("Tipsy", BuffID.Tipsy); // From Ale/Sake

            //