using System.Collections.Generic;
using Terraria.ID;

namespace Permabuffs
{
    public class Potions
    {
        public static Dictionary<string, int> buffIDs = new();

        public static void Initialize()
        {
            buffIDs["swiftness"] = BuffID.Swiftness;
            buffIDs["ironskin"] = BuffID.Ironskin;
            buffIDs["regeneration"] = BuffID.Regeneration;
            buffIDs["mana regeneration"] = BuffID.ManaRegeneration;
            buffIDs["magic power"] = BuffID.MagicPower;
            buffIDs["archery"] = BuffID.Archery;
            buffIDs["thorns"] = BuffID.Thorns;
            buffIDs["invisibility"] = BuffID.Invisibility;
            buffIDs["night owl"] = BuffID.NightOwl;
            buffIDs["shine"] = BuffID.Shine;
            buffIDs["hunter"] = BuffID.Hunter;
            buffIDs["spelunker"] = BuffID.Spelunker;
            buffIDs["gills"] = BuffID.Gills;
            buffIDs["featherfall"] = BuffID.Featherfall;
            buffIDs["gravitation"] = BuffID.Gravitation;
            buffIDs["obsidian skin"] = BuffID.ObsidianSkin;
            buffIDs["water walking"] = BuffID.WaterWalking;
            buffIDs["endurance"] = BuffID.Endurance;
            buffIDs["lifeforce"] = BuffID.Lifeforce;
            buffIDs["wrath"] = BuffID.Wrath;
            buffIDs["rage"] = BuffID.Rage;
            buffIDs["ammo reservation"] = BuffID.AmmoReservation;
            buffIDs["inferno"] = BuffID.Inferno;
            buffIDs["calm"] = BuffID.Calm;
            buffIDs["crate"] = BuffID.Crate;
            buffIDs["fishing"] = BuffID.Fishing;
            buffIDs["sonar"] = BuffID.Sonar;
            buffIDs["battle"] = BuffID.Battle;
            buffIDs["builder"] = BuffID.Builder;
            buffIDs["titan"] = BuffID.Titan;
            buffIDs["flipper"] = BuffID.Flipper;
            buffIDs["dangersense"] = BuffID.Dangersense;
            buffIDs["warmth"] = BuffID.Warmth;
            buffIDs["summoning"] = BuffID.Summoning;
            buffIDs["heartreach"] = BuffID.Heartreach;
            buffIDs["clarity"] = BuffID.Clairvoyance;
            buffIDs["luck"] = BuffID.Luck;
        }
    }
}