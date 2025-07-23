using System.Collections.Generic;
using Terraria.ID;

namespace Permabuffs
{
    public static class Potions
    {
        public static Dictionary<string, int> buffMap = new();

        static Potions()
        {
            PopulateBuffMap();
        }

        private static void PopulateBuffMap()
        {
            // Attack Potions (8)
            buffMap["Ammo Reservation Potion"] = BuffID.AmmoReservation;
            buffMap["Archery Potion"] = BuffID.Archery;
            buffMap["Battle Potion"] = BuffID.Battle;
            buffMap["Magic Power Potion"] = BuffID.MagicPower;
            buffMap["Rage Potion"] = BuffID.Rage;
            buffMap["Summoning Potion"] = BuffID.Summoning;
            buffMap["Titan Potion"] = BuffID.Titan;
            buffMap["Wrath Potion"] = BuffID.Wrath;

            // Defense Potions (12)
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
            buffMap["Worm Scarf"] = BuffID.WellFed3; // if included by name logic, else remove

            // Movement Potions (7)
            buffMap["Featherfall Potion"] = BuffID.Featherfall;
            buffMap["Flipper Potion"] = BuffID.Flipper;
            buffMap["Gills Potion"] = BuffID.Gills;
            buffMap["Gravitation Potion"] = BuffID.Gravitation;
            buffMap["Obsidian Skin Potion"] = BuffID.ObsidianSkin;
            buffMap["Swiftness Potion"] = BuffID.Swiftness;
            buffMap["Water Walking Potion"] = BuffID.WaterWalking;

            // Detection and Vision Potions (6)
            buffMap["Biome Sight Potion"] = BuffID.BiomeSight;
            buffMap["Dangersense Potion"] = BuffID.Dangersense;
            buffMap["Hunter Potion"] = BuffID.Hunter;
            buffMap["Night Owl Potion"] = BuffID.NightOwl;
            buffMap["Shine Potion"] = BuffID.Shine;
            buffMap["Spelunker Potion"] = BuffID.Spelunker;

            // Fishing Potions (3)
            buffMap["Fishing Potion"] = BuffID.Fishing;
            buffMap["Sonar Potion"] = BuffID.Sonar;
            buffMap["Crate Potion"] = BuffID.Crate;

            // Other Potions (2)
            buffMap["Builder Potion"] = BuffID.Builder;
            buffMap["Mining Potion"] = BuffID.Mining;

            // Flasks (7)
            buffMap["Flask of Poison"] = BuffID.WeaponImbuePoison;
            buffMap["Flask of Fire"] = BuffID.WeaponImbueFire;
            buffMap["Flask of Venom"] = BuffID.WeaponImbueVenom;
            buffMap["Flask of Gold"] = BuffID.WeaponImbueGold;
            buffMap["Flask of Ichor"] = BuffID.WeaponImbueIchor;
            buffMap["Flask of Cursed Flames"] = BuffID.WeaponImbueCursedFlames;
            buffMap["Flask of Nanites"] = BuffID.WeaponImbueNanites;
            buffMap["Flask of Party"] = BuffID.WeaponImbueConfetti;

            // Foods and Drinks (80+)
            // These give Well Fed, Plenty Satisfied, or Exquisitely Stuffed
            buffMap["Ale"] = BuffID.Tipsy;
            buffMap["Apple Pie"] = BuffID.ExquisitelyStuffed;
            buffMap["Bacon"] = BuffID.ExquisitelyStuffed;
            buffMap["Banana Split"] = BuffID.ExquisitelyStuffed;
            buffMap["BBQ Ribs"] = BuffID.ExquisitelyStuffed;
            buffMap["Bowl of Soup"] = BuffID.WellFed;
            buffMap["Bunny Stew"] = BuffID.PlentySatisfied;
            buffMap["Burger"] = BuffID.ExquisitelyStuffed;
            buffMap["Carton of Milk"] = BuffID.WellFed;
            buffMap["Chicken Nugget"] = BuffID.PlentySatisfied;
            buffMap["Chocolate Chip Cookie"] = BuffID.WellFed;
            buffMap["Christmas Pudding"] = BuffID.ExquisitelyStuffed;
            buffMap["Coffee"] = BuffID.WellFed;
            buffMap["Cooked Marshmallow"] = BuffID.WellFed;
            buffMap["Cream Soda"] = BuffID.WellFed;
            buffMap["Escargot"] = BuffID.PlentySatisfied;
            buffMap["Fried Egg"] = BuffID.WellFed;
            buffMap["Fries"] = BuffID.WellFed;
            buffMap["Froggle Bunwich"] = BuffID.PlentySatisfied;
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
            buffMap["Fruit Juice"] = BuffID.PlentySatisfied;
            buffMap["Fruit Salad"] = BuffID.PlentySatisfied;
            buffMap["Apple Juice"] = BuffID.PlentySatisfied;
            buffMap["Bloody Moscato"] = BuffID.PlentySatisfied;
            buffMap["Frozen Banana Daiquiri"] = BuffID.PlentySatisfied;
            buffMap["Lemonade"] = BuffID.PlentySatisfied;
            buffMap["Peach Sangria"] = BuffID.PlentySatisfied;
            buffMap["Piña Colada"] = BuffID.PlentySatisfied;
            buffMap["Prismatic Punch"] = BuffID.PlentySatisfied;
            buffMap["Smoothie of Darkness"] = BuffID.PlentySatisfied;
            buffMap["Tropical Smoothie"] = BuffID.PlentySatisfied;
            buffMap["Gingerbread Cookie"] = BuffID.PlentySatisfied;
            buffMap["Golden Delight"] = BuffID.ExquisitelyStuffed;
            buffMap["Grapes"] = BuffID.WellFed;
            buffMap["Grape Juice"] = BuffID.PlentySatisfied;
            buffMap["Grilled Squirrel"] = BuffID.PlentySatisfied;
            buffMap["Grub Soup"] = BuffID.PlentySatisfied;
            buffMap["Hotdog"] = BuffID.PlentySatisfied;
            buffMap["Ice Cream"] = BuffID.PlentySatisfied;
            buffMap["Joja Cola"] = BuffID.WellFed;
            buffMap["Milkshake"] = BuffID.PlentySatisfied;
            buffMap["Monster Lasagna"] = BuffID.PlentySatisfied;
            buffMap["Nachos"] = BuffID.PlentySatisfied;
            buffMap["Pad Thai"] = BuffID.PlentySatisfied;
            buffMap["Pizza"] = BuffID.PlentySatisfied;
            buffMap["Pho"] = BuffID.PlentySatisfied;
            buffMap["Potato Chips"] = BuffID.PlentySatisfied;
            buffMap["Pumpkin Pie"] = BuffID.PlentySatisfied;
            buffMap["Roasted Bird"] = BuffID.PlentySatisfied;
            buffMap["Roasted Duck"] = BuffID.PlentySatisfied;
            buffMap["Sake"] = BuffID.Tipsy;
            buffMap["Sauteed Frog Legs"] = BuffID.PlentySatisfied;
            buffMap["Shrimp Po' Boy"] = BuffID.PlentySatisfied;
            buffMap["Shucked Oyster"] = BuffID.PlentySatisfied;
            buffMap["Spaghetti"] = BuffID.PlentySatisfied;
            buffMap["Steak"] = BuffID.PlentySatisfied;
            buffMap["Sugar Cookie"] = BuffID.PlentySatisfied;
            buffMap["Teacup"] = BuffID.PlentySatisfied;
        }
    }
}