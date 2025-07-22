using System.Collections.Generic;
using Terraria.ID;

namespace Permabuffs
{
    public static class Potions
    {
        public static Dictionary<string, int> BuffMap = new Dictionary<string, int>(System.StringComparer.OrdinalIgnoreCase)
        {
            ["Ironskin Potion"] = BuffID.Ironskin,
            ["Regeneration Potion"] = BuffID.Regeneration,
            ["Swiftness Potion"] = BuffID.Swiftness,
            ["Well Fed"] = BuffID.WellFed,
            ["Well Fed 2"] = BuffID.WellFed2,
            ["Well Fed 3"] = BuffID.WellFed3,
            ["Exquisitely Stuffed"] = BuffID.ExquisitelyStuffed,
            ["Tipsy"] = BuffID.Tipsy,
            ["Honey"] = BuffID.Honey,
            ["Archery Potion"] = BuffID.Archery,
            ["Magic Power Potion"] = BuffID.MagicPower,
            ["Endurance Potion"] = BuffID.Endurance,
            ["Lifeforce Potion"] = BuffID.Lifeforce,
            ["Summoning Potion"] = BuffID.Summoning,
            ["Wrath Potion"] = BuffID.Wrath,
            ["Rage Potion"] = BuffID.Rage,
            ["Inferno Potion"] = BuffID.Inferno,
            ["Thorns Potion"] = BuffID.Thorns,
            ["Obsidian Skin Potion"] = BuffID.ObsidianSkin,
            ["Night Owl Potion"] = BuffID.NightOwl,
            ["Mining Potion"] = BuffID.Mining,
            ["Gills Potion"] = BuffID.Gills,
            ["Featherfall Potion"] = BuffID.Featherfall,
            ["Water Walking Potion"] = BuffID.WaterWalking,
            ["Gravitation Potion"] = BuffID.Gravitation,
            ["Battle Potion"] = BuffID.Battle,
            ["Builder Potion"] = BuffID.Builder,
            ["Calming Potion"] = BuffID.Calm,
            ["Dangersense Potion"] = BuffID.Dangersense,
            ["Fishing Potion"] = BuffID.Fishing,
            ["Flipper Potion"] = BuffID.Flipper,
            ["Heartreach Potion"] = BuffID.Heartreach,
            ["Invisibility Potion"] = BuffID.Invisibility,
            ["Love Potion"] = BuffID.Lovestruck,
            ["Mana Regeneration Potion"] = BuffID.ManaRegeneration,
            ["Sonar Potion"] = BuffID.Sonar,
            ["Spelunker Potion"] = BuffID.Spelunker,
            ["Warmth Potion"] = BuffID.Warmth,
            ["Crate Potion"] = BuffID.Crate,
            ["Ammo Reservation Potion"] = BuffID.AmmoReservation,
            ["Hunter Potion"] = BuffID.Hunter
            // Add more if needed
        };
    }
}