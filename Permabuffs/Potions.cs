using System.Collections.Generic;
using Terraria;
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
            buffMap["Crate Potion"] = BuffID.Crate;
            buffMap["Fishing Potion"] = BuffID.Fishing;
            buffMap["Sonar Potion"] = BuffID.Sonar;

            // Other
            buffMap["Builder Potion"] = BuffID.Builder;
            buffMap["Mining Potion"] = BuffID.Mining;

            // Flasks
            buffMap["Flask of Poison"] = BuffID.WeaponImbuePoison;
            buffMap["Flask of Fire"] = BuffID.WeaponImbueFire;
            buffMap["Flask of Gold"] = BuffID.WeaponImbueGold;
            buffMap["Flask of Ichor"] = BuffID.WeaponImbueIchor;
            buffMap["Flask of Cursed Flames"] = BuffID.WeaponImbueCursedFlames;
            buffMap["Flask of Nanites"] = BuffID.WeaponImbueNanites;
            buffMap["Flask of Party"] = BuffID.WeaponImbueConfetti;
            buffMap["Flask of Venom"] = BuffID.WeaponImbueVenom;

            // Drinks
            buffMap["Ale"] = BuffID.Tipsy;
            buffMap["Sake"] = BuffID.Tipsy;
            buffMap["Bloody Moscato"] = BuffID.WellFed;
            buffMap["Smoothie of Darkness"] = BuffID.WellFed;
            buffMap["Prismatic Punch"] = BuffID.WellFed2;
            buffMap["Tropical Smoothie"] = BuffID.WellFed;
            buffMap["Peach Sangria"] = BuffID.WellFed;
            buffMap["Fruit Juice"] = BuffID.WellFed;
            buffMap["Grape Juice"] = BuffID.WellFed3;
            buffMap["Apple Juice"] = BuffID.WellFed;
            buffMap["Carton of Milk"] = BuffID.WellFed;

            // Food – Well Fed (Tier 1)
            buffMap["Apple"] = BuffID.WellFed;
            buffMap["Apricot"] = BuffID.WellFed;
            buffMap["Banana"] = BuffID.WellFed;
            buffMap["Blackcurrant"] = BuffID.WellFed;
            buffMap["Blood Orange"] = BuffID.WellFed;
            buffMap["Bowl of Soup"] = BuffID.WellFed;
            buffMap["Bunny Stew"] = BuffID.WellFed;
            buffMap["Cherry"] = BuffID.WellFed;
            buffMap["Coconut"] = BuffID.WellFed;
            buffMap["Cooked Marshmallow"] = BuffID.WellFed;
            buffMap["Dragon Fruit"] = BuffID.WellFed;
            buffMap["Elderberry"] = BuffID.WellFed;
            buffMap["Escargot"] = BuffID.WellFed;
            buffMap["Fried Egg"] = BuffID.WellFed;
            buffMap["Fries"] = BuffID.WellFed;
            buffMap["Grapefruit"] = BuffID.WellFed;
            buffMap["Grilled Squirrel"] = BuffID.WellFed;
            buffMap["Grub Soup"] = BuffID.WellFed;
            buffMap["Joja Cola"] = BuffID.WellFed;
            buffMap["Lemon"] = BuffID.WellFed;
            buffMap["Lemonade"] = BuffID.WellFed;
            buffMap["Mango"] = BuffID.WellFed;
            buffMap["Marshmallow"] = BuffID.WellFed;
            buffMap["Pad Thai"] = BuffID.WellFed;
            buffMap["Peach"] = BuffID.WellFed;
            buffMap["Pho"] = BuffID.WellFed;
            buffMap["Pineapple"] = BuffID.WellFed;
            buffMap["Pizza"] = BuffID.WellFed;
            buffMap["Plum"] = BuffID.WellFed;
            buffMap["Pomegranate"] = BuffID.WellFed;
            buffMap["Potato Chips"] = BuffID.WellFed;
            buffMap["Pumpkin Pie"] = BuffID.WellFed;
            buffMap["Rambutan"] = BuffID.WellFed;
            buffMap["Roasted Bird"] = BuffID.WellFed;
            buffMap["Roasted Duck"] = BuffID.WellFed;
            buffMap["Sautéed Frog Legs"] = BuffID.WellFed;
            buffMap["Shucked Oyster"] = BuffID.WellFed;
            buffMap["Spicy Pepper"] = BuffID.WellFed;
            buffMap["Star Fruit"] = BuffID.WellFed;
            buffMap["Steak"] = BuffID.WellFed;
            buffMap["Sugar Cookie"] = BuffID.WellFed;
            buffMap["Teacup"] = BuffID.WellFed;
            buffMap["Cooked Fish"] = BuffID.WellFed;
            buffMap["Grilled Squirrel"] = BuffID.WellFed;
            buffMap["Shucked Oyster"] = BuffID.WellFed;
            buffMap["Lemonade"] = BuffID.WellFed;
            buffMap["Peach Sangria"] = BuffID.WellFed;
            buffMap["Roasted Bird"] = BuffID.WellFed;
            buffMap["Sauteed Frog Legs"] = BuffID.WellFed;
            buffMap["Fruit Juice"] = BuffID.WellFed;
            buffMap["Bloody Moscato"] = BuffID.WellFed;
            buffMap["Carton of Milk"] = BuffID.WellFed;
            buffMap["Piña Colada"] = BuffID.WellFed;
            buffMap["Smoothie of Darkness"] = BuffID.WellFed;
            buffMap["Tropical Smoothie"] = BuffID.WellFed;
            buffMap["Potato Chips"] = BuffID.WellFed;

            // Food – Plenty Satisfied (Tier 2)
            buffMap["Bacon"] = BuffID.WellFed2;
            buffMap["BBQ Ribs"] = BuffID.WellFed2;
            buffMap["Burger"] = BuffID.WellFed2;
            buffMap["Christmas Pudding"] = BuffID.WellFed2;
            buffMap["Chocolate Chip Cookie"] = BuffID.WellFed2;
            buffMap["Chicken Nugget"] = BuffID.WellFed2;
            buffMap["Froggle Bunwich"] = BuffID.WellFed2;
            buffMap["Ice Cream"] = BuffID.WellFed2;
            buffMap["Monster Lasagna"] = BuffID.WellFed2;
            buffMap["Sashimi"] = BuffID.WellFed2;
            buffMap["Cooked Shrimp"] = BuffID.WellFed2;
            buffMap["Lobster Tail"] = BuffID.WellFed2;
            buffMap["Seafood Dinner"] = BuffID.WellFed2;
            buffMap["Cream Soda"] = BuffID.WellFed2;
            buffMap["Grapes"] = BuffID.WellFed2;
            buffMap["Nachos"] = BuffID.WellFed2;
            buffMap["Shrimp Po' Boy"] = BuffID.WellFed2;
            buffMap["Chocolate Chip Cookie"] = BuffID.WellFed2;
            buffMap["Prismatic Punch"] = BuffID.WellFed2;

            // Food – Exquisitely Stuffed (Tier 3)
            buffMap["Apple Pie"] = BuffID.WellFed3;
            buffMap["Golden Delight"] = BuffID.WellFed3;
            buffMap["Grape Juice"] = BuffID.WellFed3;
            buffMap["Milkshake"] = BuffID.WellFed3;
            buffMap["Hotdog"] = BuffID.WellFed3;

        }

        public static List<int> GetBuffsFromPiggyBank(Player player)
        {
            List<int> buffs = new List<int>();

            foreach (Item item in player.bank.item)
            {
                if (item != null && !item.IsAir && item.stack >= 30)
                {
                    string name = Lang.GetItemNameValue(item.type);
                    if (buffMap.TryGetValue(name, out int buffID))
                    {
                        if (buffID > 0 && !buffs.Contains(buffID))
                        {
                            buffs.Add(buffID);
                        }
                    }
                }
            }

            return buffs;
        }

        public static Dictionary<string, int> BuffMap => buffMap;
    }
}