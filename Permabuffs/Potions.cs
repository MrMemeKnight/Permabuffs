using System.Collections.Generic;
using Terraria.ID;

namespace Permabuffs
{
    public static class Potions
    {
        public static readonly Dictionary<string, int> BuffMap = new(StringComparer.OrdinalIgnoreCase)
        {
            // Buff Potions
            { "ammo reservation potion", BuffID.AmmoReservation },
            { "archery potion", BuffID.Archery },
            { "battle potion", BuffID.Battle },
            { "builder potion", BuffID.Builder },
            { "calming potion", BuffID.Calm },
            { "crate potion", BuffID.Crate },
            { "dangersense potion", BuffID.Dangersense },
            { "endurance potion", BuffID.Endurance },
            { "featherfall potion", BuffID.Featherfall },
            { "fishing potion", BuffID.Fishing },
            { "flipper potion", BuffID.Flipper },
            { "gills potion", BuffID.Gills },
            { "gravitation potion", BuffID.Gravitation },
            { "greater luck potion", BuffID.Luck },
            { "heartreach potion", BuffID.Heartreach },
            { "hunter potion", BuffID.Hunter },
            { "inferno potion", BuffID.Inferno },
            { "invisibility potion", BuffID.Invisibility },
            { "ironskin potion", BuffID.Ironskin },
            { "lesser luck potion", BuffID.Luck },
            { "lifeforce potion", BuffID.Lifeforce },
            { "luck potion", BuffID.Luck },
            { "magic power potion", BuffID.MagicPower },
            { "mana regeneration potion", BuffID.ManaRegeneration },
            { "mining potion", BuffID.Mining },
            { "night owl potion", BuffID.NightOwl },
            { "obsidian skin potion", BuffID.ObsidianSkin },
            { "rage potion", BuffID.Rage },
            { "regeneration potion", BuffID.Regeneration },
            { "shine potion", BuffID.Shine },
            { "sonar potion", BuffID.Sonar },
            { "spelunker potion", BuffID.Spelunker },
            { "stink potion", BuffID.StinkyToy }, // approximate
            { "summoning potion", BuffID.Summoning },
            { "swiftness potion", BuffID.Swiftness },
            { "thorns potion", BuffID.Thorns },
            { "titan potion", BuffID.Titan },
            { "warmth potion", BuffID.Warmth },
            { "water walking potion", BuffID.WaterWalking },
            { "wrath potion", BuffID.Wrath },

            // Weapon Flasks
            { "flask of cursed flames", BuffID.WeaponImbueCursedFlames },
            { "flask of fire", BuffID.WeaponImbueFire },
            { "flask of gold", BuffID.WeaponImbueGold },
            { "flask of ichor", BuffID.WeaponImbueIchor },
            { "flask of nanites", BuffID.WeaponImbueNanites },
            { "flask of party", BuffID.WeaponImbueConfetti },
            { "flask of poison", BuffID.WeaponImbueVenom },
            { "flask of venom", BuffID.WeaponImbueVenom },

            // Drinks (Tipsy)
            { "ale", BuffID.Tipsy },
            { "sake", BuffID.Tipsy },

            // Well-Fed (26)
            { "apple", BuffID.WellFed },
            { "apple juice", BuffID.WellFed },
            { "banana", BuffID.WellFed },
            { "bunny stew", BuffID.WellFed },
            { "carton of milk", BuffID.WellFed },
            { "cherry", BuffID.WellFed },
            { "coconut", BuffID.WellFed },
            { "fruit salad", BuffID.WellFed },
            { "grilled squirrel", BuffID.WellFed },
            { "lemonade", BuffID.WellFed },
            { "marshmallow", BuffID.WellFed },
            { "peach sangria", BuffID.WellFed },
            { "potato chips", BuffID.WellFed },
            { "roasted bird", BuffID.WellFed },
            { "smoothie of darkness", BuffID.WellFed },
            { "teacup", BuffID.WellFed },
            { "tropical smoothie", BuffID.WellFed },

            // Plenty Satisfied (206)
            { "cooked shrimp", BuffID.WellFed2 },
            { "lobster tail", BuffID.WellFed2 },
            { "prismatic punch", BuffID.WellFed2 },
            { "roasted duck", BuffID.WellFed2 },
            { "pho", BuffID.WellFed2 },
            { "banana split", BuffID.WellFed2 },
            { "chicken nugget", BuffID.WellFed2 },
            { "nachos", BuffID.WellFed2 },
            { "hotdog", BuffID.WellFed2 },

            // Exquisitely Stuffed (207)
            { "apple pie", BuffID.WellFed3 },
            { "bacon", BuffID.WellFed3 },
            { "burger", BuffID.WellFed3 },
            { "pizza", BuffID.WellFed3 },
            { "steak", BuffID.WellFed3 },
        };
    }
}