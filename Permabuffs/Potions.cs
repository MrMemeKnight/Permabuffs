using System.Collections.Generic;

namespace Permabuffs
{
    public class Potions
    {
        public static Dictionary<string, int> BuffMap { get; private set; }

        static Potions()
        {
            PopulateBuffMap();
        }

        private static void PopulateBuffMap()
        {
            BuffMap = new Dictionary<string, int>()
            {
                // --- Attack Buff Potions ---
                { "Ammo Reservation Potion", Terraria.ID.BuffID.AmmoReservation },
                { "Archery Potion", Terraria.ID.BuffID.Archery },
                { "Battle Potion", Terraria.ID.BuffID.Battle },
                { "Magic Power Potion", Terraria.ID.BuffID.MagicPower },
                { "Rage Potion", Terraria.ID.BuffID.Rage },
                { "Summoning Potion", Terraria.ID.BuffID.Summoning },
                { "Titan Potion", Terraria.ID.BuffID.Titan },
                { "Wrath Potion", Terraria.ID.BuffID.Wrath },

                // --- Defense Buff Potions ---
                { "Calming Potion", Terraria.ID.BuffID.Calm },
                { "Endurance Potion", Terraria.ID.BuffID.Endurance },
                { "Heartreach Potion", Terraria.ID.BuffID.Heartreach },
                { "Inferno Potion", Terraria.ID.BuffID.Inferno },
                { "Invisibility Potion", Terraria.ID.BuffID.Invisibility },
                { "Ironskin Potion", Terraria.ID.BuffID.Ironskin },
                { "Lifeforce Potion", Terraria.ID.BuffID.Lifeforce },
                { "Mana Regeneration Potion", Terraria.ID.BuffID.ManaRegeneration },
                { "Regeneration Potion", Terraria.ID.BuffID.Regeneration },
                { "Thorns Potion", Terraria.ID.BuffID.Thorns },
                { "Warmth Potion", Terraria.ID.BuffID.Warmth },
                { "Wiesnbräu", Terraria.ID.BuffID.WellFed3 },

                // --- Movement Buff Potions ---
                { "Featherfall Potion", Terraria.ID.BuffID.Featherfall },
                { "Flipper Potion", Terraria.ID.BuffID.Flipper },
                { "Gills Potion", Terraria.ID.BuffID.Gills },
                { "Gravitation Potion", Terraria.ID.BuffID.Gravitation },
                { "Obsidian Skin Potion", Terraria.ID.BuffID.ObsidianSkin },
                { "Swiftness Potion", Terraria.ID.BuffID.Swiftness },
                { "Water Walking Potion", Terraria.ID.BuffID.WaterWalking },

                // --- Detection & Vision Buff Potions ---
                { "Biome Sight Potion", Terraria.ID.BuffID.BiomeSight },
                { "Dangersense Potion", Terraria.ID.BuffID.Dangersense },
                { "Hunter Potion", Terraria.ID.BuffID.Hunter },
                { "Night Owl Potion", Terraria.ID.BuffID.NightOwl },
                { "Shine Potion", Terraria.ID.BuffID.Shine },
                { "Spelunker Potion", Terraria.ID.BuffID.Spelunker },

                // --- Fishing Buff Potions ---
                { "Fishing Potion", Terraria.ID.BuffID.Fishing },
                { "Sonar Potion", Terraria.ID.BuffID.Sonar },
                { "Crate Potion", Terraria.ID.BuffID.Crate },

                // --- Luck Buff Potions ---
                { "Greater Luck Potion", Terraria.ID.BuffID.Luck },
                { "Luck Potion", Terraria.ID.BuffID.Luck2 },
                { "Lesser Luck Potion", Terraria.ID.BuffID.Luck3 },

                // --- Other Buff Potions ---
                { "Builder Potion", Terraria.ID.BuffID.Builder },
                { "Mining Potion", Terraria.ID.BuffID.Mining },

                // --- Flasks ---
                { "Flask of Poison", Terraria.ID.BuffID.WeaponImbuePoison },
                { "Flask of Fire", Terraria.ID.BuffID.WeaponImbueFire },
                { "Flask of Venom", Terraria.ID.BuffID.WeaponImbueVenom },
                { "Flask of Gold", Terraria.ID.BuffID.WeaponImbueGold },
                { "Flask of Ichor", Terraria.ID.BuffID.WeaponImbueIchor },
                { "Flask of Cursed Flames", Terraria.ID.BuffID.WeaponImbueCursedFlames },
                { "Flask of Nanites", Terraria.ID.BuffID.WeaponImbueNanites },
                { "Flask of Party", Terraria.ID.BuffID.WeaponImbueConfetti },

                // --- Food & Drink Items (Buffs: Well Fed, Plenty Satisfied, Exquisitely Stuffed) ---
                { "Ale", Terraria.ID.BuffID.Tipsy },
                { "Apple Pie", Terraria.ID.BuffID.WellFed3 },
                { "Bacon", Terraria.ID.BuffID.WellFed3 },
                { "Banana Split", Terraria.ID.BuffID.WellFed3 },
                { "BBQ Ribs", Terraria.ID.BuffID.WellFed3 },
                { "Bowl of Soup", Terraria.ID.BuffID.WellFed },
                { "Bunny Stew", Terraria.ID.BuffID.WellFed },
                { "Burger", Terraria.ID.BuffID.WellFed2 },
                { "Carton of Milk", Terraria.ID.BuffID.WellFed },
                { "Chicken Nugget", Terraria.ID.BuffID.WellFed },
                { "Chocolate Chip Cookie", Terraria.ID.BuffID.WellFed },
                { "Christmas Pudding", Terraria.ID.BuffID.WellFed3 },
                { "Coffee", Terraria.ID.BuffID.WellFed },
                { "Cooked Marshmallow", Terraria.ID.BuffID.WellFed },
                { "Cream Soda", Terraria.ID.BuffID.WellFed },
                { "Escargot", Terraria.ID.BuffID.WellFed3 },
                { "Fried Egg", Terraria.ID.BuffID.WellFed },
                { "Fries", Terraria.ID.BuffID.WellFed },
                { "Froggle Bunwich", Terraria.ID.BuffID.WellFed2 },
                { "Apple", Terraria.ID.BuffID.WellFed },
                { "Apricot", Terraria.ID.BuffID.WellFed },
                { "Banana", Terraria.ID.BuffID.WellFed },
                { "Blackcurrant", Terraria.ID.BuffID.WellFed },
                { "Blood Orange", Terraria.ID.BuffID.WellFed },
                { "Cherry", Terraria.ID.BuffID.WellFed },
                { "Coconut", Terraria.ID.BuffID.WellFed },
                { "Dragon Fruit", Terraria.ID.BuffID.WellFed },
                { "Elderberry", Terraria.ID.BuffID.WellFed },
                { "Grapefruit", Terraria.ID.BuffID.WellFed },
                { "Lemon", Terraria.ID.BuffID.WellFed },
                { "Mango", Terraria.ID.BuffID.WellFed },
                { "Peach", Terraria.ID.BuffID.WellFed },
                { "Pineapple", Terraria.ID.BuffID.WellFed },
                { "Plum", Terraria.ID.BuffID.WellFed },
                { "Pomegranate", Terraria.ID.BuffID.WellFed },
                { "Rambutan", Terraria.ID.BuffID.WellFed },
                { "Spicy Pepper", Terraria.ID.BuffID.WellFed },
                { "Star Fruit", Terraria.ID.BuffID.WellFed },
                { "Fruit Juice", Terraria.ID.BuffID.WellFed2 },
                { "Fruit Salad", Terraria.ID.BuffID.WellFed2 },
                { "Apple Juice", Terraria.ID.BuffID.WellFed },
                { "Bloody Moscato", Terraria.ID.BuffID.WellFed3 },
                { "Frozen Banana Daiquiri", Terraria.ID.BuffID.WellFed3 },
                { "Lemonade", Terraria.ID.BuffID.WellFed },
                { "Peach Sangria", Terraria.ID.BuffID.WellFed3 },
                { "Piña Colada", Terraria.ID.BuffID.WellFed3 },
                { "Prismatic Punch", Terraria.ID.BuffID.WellFed3 },
                { "Smoothie of Darkness", Terraria.ID.BuffID.WellFed3 },
                { "Tropical Smoothie", Terraria.ID.BuffID.WellFed3 },
                { "Gingerbread Cookie", Terraria.ID.BuffID.WellFed },
                { "Golden Delight", Terraria.ID.BuffID.WellFed3 },
                { "Grapes", Terraria.ID.BuffID.WellFed },
                { "Grape Juice", Terraria.ID.BuffID.WellFed },
                { "Grilled Squirrel", Terraria.ID.BuffID.WellFed },
                { "Grub Soup", Terraria.ID.BuffID.WellFed2 },
                { "Hotdog", Terraria.ID.BuffID.WellFed2 },
                { "Ice Cream", Terraria.ID.BuffID.WellFed },
                { "Joja Cola", Terraria.ID.BuffID.WellFed },
                { "Milkshake", Terraria.ID.BuffID.WellFed2 },
                { "Monster Lasagna", Terraria.ID.BuffID.WellFed },
                { "Nachos", Terraria.ID.BuffID.WellFed2 },
                { "Pad Thai", Terraria.ID.BuffID.WellFed2 },
                { "Pizza", Terraria.ID.BuffID.WellFed3 },
                { "Pho", Terraria.ID.BuffID.WellFed3 },
                { "Potato Chips", Terraria.ID.BuffID.WellFed },
                { "Pumpkin Pie", Terraria.ID.BuffID.WellFed2 },
                { "Roasted Bird", Terraria.ID.BuffID.WellFed },
                { "Roasted Duck", Terraria.ID.BuffID.WellFed2 },
                { "Sake", Terraria.ID.BuffID.Tipsy },
                { "Sauteed Frog Legs", Terraria.ID.BuffID.WellFed2 },
                { "Shrimp Po' Boy", Terraria.ID.BuffID.WellFed2 },
                { "Shucked Oyster", Terraria.ID.BuffID.WellFed },
                { "Spaghetti", Terraria.ID.BuffID.WellFed2 },
                { "Steak", Terraria.ID.BuffID.WellFed2 },
                { "Sugar Cookie", Terraria.ID.BuffID.WellFed },
                { "Teacup", Terraria.ID.BuffID.WellFed },
            };
        }
    }
}