using System.Collections.Generic;
using Terraria.ID;

namespace Permabuffs
{
    public static class Potions
    {
        public static Dictionary<string, int> buffMap = new Dictionary<string, int>();

        static Potions()
        {
            PopulateBuffMap();
        }

        private static void PopulateBuffMap()
        {
            // Attack
            buffMap["Ammo Reservation Potion"] = BuffID.AmmoReservation;
            buffMap["Archery Potion"] = BuffID.Archery;
            buffMap["Battle Potion"] = BuffID.Battle;
            buffMap["Magic Power Potion"] = BuffID.MagicPower;
            buffMap["Rage Potion"] = BuffID.Rage;
            buffMap["Summoning Potion"] = BuffID.Summoning;
            buffMap["Titan Potion"] = BuffID.Titan;
            buffMap["Wrath Potion"] = BuffID.Wrath;

            // Defense
            buffMap["Calming Potion"] = BuffID.Calm;
            buffMap["Endurance Potion"] = BuffID.Endurance;
            buffMap["Heartreach Potion"] = BuffID.Heartreach;
            buffMap["Inferno Potion"] = BuffID.Inferno;
            buffMap["Invisibility Potion"] = BuffID.Invisibility;
            buffMap["Ironskin Potion"] = BuffID.Ironskin;
            buffMap["Lifeforce Potion"] = BuffID.Lifeforce;
            buffMap["Mana Regeneration Potion"] = BuffID.ManaRegeneration;
            buffMap["Regeneration Potion"] = BuffID.Regeneration;
            buffMap["Thorns Potion"] = BuffID.Thorns;
            buffMap["Warmth Potion"] = BuffID.Warmth;

            // Movement
            buffMap["Featherfall Potion"] = BuffID.Featherfall;
            buffMap["Flipper Potion"] = BuffID.Flipper;
            buffMap["Gills Potion"] = BuffID.Gills;
            buffMap["Gravitation Potion"] = BuffID.Gravitation;
            buffMap["Obsidian Skin Potion"] = BuffID.ObsidianSkin;
            buffMap["Swiftness Potion"] = BuffID.Swiftness;
            buffMap["Water Walking Potion"] = BuffID.WaterWalking;

            // Detection and Vision
            buffMap["Biome Sight Potion"] = BuffID.BiomeSight;
            buffMap["Dangersense Potion"] = BuffID.Dangersense;
            buffMap["Hunter Potion"] = BuffID.Hunter;
            buffMap["Night Owl Potion"] = BuffID.NightOwl;
            buffMap["Shine Potion"] = BuffID.Shine;
            buffMap["Spelunker Potion"] = BuffID.Spelunker;

            // Fishing
            buffMap["Fishing Potion"] = BuffID.Fishing;
            buffMap["Sonar Potion"] = BuffID.Sonar;
            buffMap["Crate Potion"] = BuffID.Crate;

            // Other
            buffMap["Builder Potion"] = BuffID.Builder;
            buffMap["Mining Potion"] = BuffID.Mining;

            // Foods and Drinks
            buffMap["Ale"] = BuffID.Tipsy;
            buffMap["Apple Pie"] = BuffID.WellFed3;
            buffMap["Bacon"] = BuffID.WellFed3;
            buffMap["Banana Split"] = BuffID.WellFed3;
            buffMap["BBQ Ribs"] = BuffID.WellFed3;
            buffMap["Bowl of Soup"] = BuffID.WellFed;
            buffMap["Bunny Stew"] = BuffID.WellFed;
            buffMap["Burger"] = BuffID.WellFed3;
            buffMap["Carton of Milk"] = BuffID.WellFed;
            buffMap["Chicken Nugget"] = BuffID.WellFed;
            buffMap["Chocolate Chip Cookie"] = BuffID.WellFed2;
            buffMap["Christmas Pudding"] = BuffID.WellFed2;
            buffMap["Coffee"] = BuffID.WellFed;
            buffMap["Cooked Marshmallow"] = BuffID.WellFed;
            buffMap["Cream Soda"] = BuffID.WellFed;
            buffMap["Escargot"] = BuffID.WellFed3;
            buffMap["Fried Egg"] = BuffID.WellFed;
            buffMap["Fries"] = BuffID.WellFed;
            buffMap["Froggle Bunwich"] = BuffID.WellFed2;
            buffMap["Apple"] = BuffID.WellFed;
            buffMap["Apricot"] = BuffID.WellFed;
            buffMap["Banana"] = BuffID.WellFed;
            buffMap["Blackcurrant"] = BuffID.WellFed;
            buffMap["Blood Orange"] = BuffID.WellFed;
            buffMap["Cherry"] = BuffID.WellFed;
            buffMap["Coconut"] = BuffID.WellFed;
            buffMap["Dragon Fruit"] = BuffID.WellFed;
            buffMap["Elderberry"] = BuffID.WellFed;
            buffMap["Grapefruit"] = BuffID.WellFed;
            buffMap["Lemon"] = BuffID.WellFed;
            buffMap["Mango"] = BuffID.WellFed;
            buffMap["Peach"] = BuffID.WellFed;
            buffMap["Pineapple"] = BuffID.WellFed;
            buffMap["Plum"] = BuffID.WellFed;
            buffMap["Pomegranate"] = BuffID.WellFed;
            buffMap["Rambutan"] = BuffID.WellFed;
            buffMap["Spicy Pepper"] = BuffID.WellFed;
            buffMap["Star Fruit"] = BuffID.WellFed;
            buffMap["Fruit Juice"] = BuffID.WellFed;
            buffMap["Fruit Salad"] = BuffID.WellFed2;
            buffMap["Apple Juice"] = BuffID.WellFed;
            buffMap["Bloody Moscato"] = BuffID.WellFed2;
            buffMap["Frozen Banana Daiquiri"] = BuffID.WellFed2;
            buffMap["Lemonade"] = BuffID.WellFed;
            buffMap["Peach Sangria"] = BuffID.WellFed2;
            buffMap["Piña Colada"] = BuffID.WellFed2;
            buffMap["Prismatic Punch"] = BuffID.WellFed2;
            buffMap["Smoothie of Darkness"] = BuffID.WellFed2;
            buffMap["Tropical Smoothie"] = BuffID.WellFed2;
            buffMap["Gingerbread Cookie"] = BuffID.WellFed;
            buffMap["Golden Delight"] = BuffID.WellFed3;
            buffMap["Grapes"] = BuffID.WellFed;
            buffMap["Grape Juice"] = BuffID.WellFed;
            buffMap["Grilled Squirrel"] = BuffID.WellFed;
            buffMap["Grub Soup"] = BuffID.WellFed;
            buffMap["Hotdog"] = BuffID.WellFed;
            buffMap["Ice Cream"] = BuffID.WellFed2;
            buffMap["Joja Cola"] = BuffID.WellFed;
            buffMap["Milkshake"] = BuffID.WellFed2;
            buffMap["Monster Lasagna"] = BuffID.WellFed2;
            buffMap["Nachos"] = BuffID.WellFed2;
            buffMap["Pad Thai"] = BuffID.WellFed2;
            buffMap["Pizza"] = BuffID.WellFed3;
            buffMap["Pho"] = BuffID.WellFed2;
            buffMap["Potato Chips"] = BuffID.WellFed;
            buffMap["Pumpkin Pie"] = BuffID.WellFed2;
            buffMap["Roasted Bird"] = BuffID.WellFed2;
            buffMap["Roasted Duck"] = BuffID.WellFed3;
            buffMap["Sake"] = BuffID.Tipsy;
            buffMap["Sauteed Frog Legs"] = BuffID.WellFed2;
            buffMap["Shrimp Po' Boy"] = BuffID.WellFed3;
            buffMap["Shucked Oyster"] = BuffID.WellFed2;
            buffMap["Spaghetti"] = BuffID.WellFed3;
            buffMap["Steak"] = BuffID.WellFed3;
            buffMap["Sugar Cookie"] = BuffID.WellFed;
            buffMap["Teacup"] = BuffID.WellFed;

            // Flasks
            buffMap["Flask of Poison"] = BuffID.WeaponImbuePoison;
            buffMap["Flask of Fire"] = BuffID.WeaponImbueFire;
            buffMap["Flask of Venom"] = BuffID.WeaponImbueVenom;
            buffMap["Flask of Gold"] = BuffID.WeaponImbueGold;
            buffMap["Flask of Ichor"] = BuffID.WeaponImbueIchor;
            buffMap["Flask of Cursed Flames"] = BuffID.WeaponImbueCursedFlames;
            buffMap["Flask of Nanites"] = BuffID.WeaponImbueNanites;
            buffMap["Flask of Party"] = BuffID.WeaponImbueConfetti;
        }

        public static List<int> GetBuffsFromPiggyBank()
        {
            List<int> buffs = new List<int>();

            foreach (var kvp in buffMap)
            {
                buffs.Add(kvp.Value);
            }

            return buffs;
        }
    }
}