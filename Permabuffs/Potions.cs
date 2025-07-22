using System.Collections.Generic;
using Terraria;

namespace Permabuffs
{
    public class Potions
    {
        public static Dictionary<string, int> buffIDs = new();

        public Potions()
        {
            // Standard Potions
            buffIDs.Add("Regeneration Potion", BuffID.Regeneration);
            buffIDs.Add("Swiftness Potion", BuffID.Swiftness);
            buffIDs.Add("Ironskin Potion", BuffID.Ironskin);
            buffIDs.Add("Mana Regeneration Potion", BuffID.ManaRegeneration);
            buffIDs.Add("Magic Power Potion", BuffID.MagicPower);
            buffIDs.Add("Featherfall Potion", BuffID.Featherfall);
            buffIDs.Add("Spelunker Potion", BuffID.Spelunker);
            buffIDs.Add("Invisibility Potion", BuffID.Invisibility);
            buffIDs.Add("Shine Potion", BuffID.Shine);
            buffIDs.Add("Night Owl Potion", BuffID.NightOwl);
            buffIDs.Add("Battle Potion", BuffID.Battle);
            buffIDs.Add("Thorns Potion", BuffID.Thorns);
            buffIDs.Add("Water Walking Potion", BuffID.WaterWalking);
            buffIDs.Add("Archery Potion", BuffID.Archery);
            buffIDs.Add("Hunter Potion", BuffID.Hunter);
            buffIDs.Add("Gravitation Potion", BuffID.Gravitation);
            buffIDs.Add("Obsidian Skin Potion", BuffID.ObsidianSkin);
            buffIDs.Add("Regeneration", BuffID.Regeneration);
            buffIDs.Add("Gills Potion", BuffID.Gills);
            buffIDs.Add("Endurance Potion", BuffID.Endurance);
            buffIDs.Add("Lifeforce Potion", BuffID.Lifeforce);
            buffIDs.Add("Rage Potion", BuffID.Rage);
            buffIDs.Add("Wrath Potion", BuffID.Wrath);
            buffIDs.Add("Heartreach Potion", BuffID.Heartreach);
            buffIDs.Add("Inferno Potion", BuffID.Inferno);
            buffIDs.Add("Calming Potion", BuffID.Calm);
            buffIDs.Add("Titan Potion", BuffID.Titan);
            buffIDs.Add("Summoning Potion", BuffID.Summoning);
            buffIDs.Add("Fishing Potion", BuffID.Fishing);
            buffIDs.Add("Crate Potion", BuffID.Crate);
            buffIDs.Add("Sonar Potion", BuffID.Sonar);
            buffIDs.Add("Ammo Reservation Potion", BuffID.AmmoReservation);
            buffIDs.Add("Builder Potion", BuffID.Builder);
            buffIDs.Add("Dangersense Potion", BuffID.Dangersense);
            buffIDs.Add("Mining Potion", BuffID.Mining);
            buffIDs.Add("Warmth Potion", BuffID.Warmth);
            buffIDs.Add("Teleportation Potion", BuffID.ChaosState); // this may not have a direct buff
            buffIDs.Add("Flipper Potion", BuffID.Flipper);
            buffIDs.Add("Invisibility", BuffID.Invisibility);
            buffIDs.Add("Swiftness", BuffID.Swiftness);
            buffIDs.Add("Shine", BuffID.Shine);
            buffIDs.Add("Night Owl", BuffID.NightOwl);
            buffIDs.Add("Battle", BuffID.Battle);
            buffIDs.Add("Thorns", BuffID.Thorns);
            buffIDs.Add("Water Walking", BuffID.WaterWalking);
            buffIDs.Add("Archery", BuffID.Archery);
            buffIDs.Add("Hunter", BuffID.Hunter);
            buffIDs.Add("Gravitation", BuffID.Gravitation);
            buffIDs.Add("Obsidian Skin", BuffID.ObsidianSkin);
            buffIDs.Add("Mana Regeneration", BuffID.ManaRegeneration);
            buffIDs.Add("Magic Power", BuffID.MagicPower);
            buffIDs.Add("Featherfall", BuffID.Featherfall);
            buffIDs.Add("Spelunker", BuffID.Spelunker);
            buffIDs.Add("Endurance", BuffID.Endurance);
            buffIDs.Add("Lifeforce", BuffID.Lifeforce);
            buffIDs.Add("Rage", BuffID.Rage);
            buffIDs.Add("Wrath", BuffID.Wrath);
            buffIDs.Add("Heartreach", BuffID.Heartreach);
            buffIDs.Add("Inferno", BuffID.Inferno);
            buffIDs.Add("Calm", BuffID.Calm);
            buffIDs.Add("Titan", BuffID.Titan);
            buffIDs.Add("Summoning", BuffID.Summoning);
            buffIDs.Add("Fishing", BuffID.Fishing);
            buffIDs.Add("Crate", BuffID.Crate);
            buffIDs.Add("Sonar", BuffID.Sonar);
            buffIDs.Add("Ammo Reservation", BuffID.AmmoReservation);
            buffIDs.Add("Builder", BuffID.Builder);
            buffIDs.Add("Dangersense", BuffID.Dangersense);
            buffIDs.Add("Mining", BuffID.Mining);
            buffIDs.Add("Warmth", BuffID.Warmth);
            buffIDs.Add("Flipper", BuffID.Flipper);
        }
    }
}