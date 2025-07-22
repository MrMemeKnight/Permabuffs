using System.Collections.Generic;

namespace Permabuffs
{
    /// <summary>
    /// Maps potion, flask, food, and drink names to their respective buff IDs
    /// for permanent buff detection.
    /// </summary>
    public class Potions
    {
        public static Dictionary<string, int> BuffMap = new Dictionary<string, int>();

        public static void PopulateBuffMap()
        {
            // Standard buff potions (IDs from 1–18, 112–123, 257 etc.)
            BuffMap.Add("Ammo Reservation Potion", 112);
            BuffMap.Add("Archery Potion", 16);
            BuffMap.Add("Battle Potion", 13);
            BuffMap.Add("Builder Potion", 107);
            BuffMap.Add("Calming Potion", 106);
            BuffMap.Add("Crate Potion", 123);
            BuffMap.Add("Dangersense Potion", 111);
            BuffMap.Add("Endurance Potion", 114);
            BuffMap.Add("Featherfall Potion", 8);
            BuffMap.Add("Fishing Potion", 121);
            BuffMap.Add("Flipper Potion", 109);
            BuffMap.Add("Gills Potion", 4);
            BuffMap.Add("Gravitation Potion", 18);
            BuffMap.Add("Hunter Potion", 17);
            BuffMap.Add("Inferno Potion", 116);
            BuffMap.Add("Invisibility Potion", 10);
            BuffMap.Add("Ironskin Potion", 5);
            BuffMap.Add("Lifeforce Potion", 113);
            BuffMap.Add("Magic Power Potion", 7);
            BuffMap.Add("Mana Regeneration Potion", 6);
            BuffMap.Add("Mining Potion", 104);
            BuffMap.Add("Night Owl Potion", 12);
            BuffMap.Add("Obsidian Skin Potion", 1);
            BuffMap.Add("Rage Potion", 115);
            BuffMap.Add("Regeneration Potion", 2);
            BuffMap.Add("Shine Potion", 11);
            BuffMap.Add("Sonar Potion", 122);
            BuffMap.Add("Spelunker Potion", 9);
            BuffMap.Add("Stink Potion", 371);
            BuffMap.Add("Summoning Potion", 110);
            BuffMap.Add("Swiftness Potion", 3);
            BuffMap.Add("Thorns Potion", 14);
            BuffMap.Add("Titan Potion", 108);
            BuffMap.Add("Warmth Potion", 124);
            BuffMap.Add("Water Walking Potion", 15);
            BuffMap.Add("Wrath Potion", 117);
            BuffMap.Add("Greater Luck Potion", 257);
            BuffMap.Add("Luck Potion", 257);
            BuffMap.Add("Lesser Luck Potion", 257);

            // Flasks (IDs 71–79)
            BuffMap.Add("Flask of Cursed Flames", 73);
            BuffMap.Add("Flask of Fire", 74);
            BuffMap.Add("Flask of Gold", 75);
            BuffMap.Add("Flask of Ichor", 76);
            BuffMap.Add("Flask of Nanites", 77);
            BuffMap.Add("Flask of Party", 78);
            BuffMap.Add("Flask of Poison", 79);
            BuffMap.Add("Flask of Venom", 71);

            // Drinks (Tipsy buff, ID 25)
            BuffMap.Add("Sake", 25);
            BuffMap.Add("Ale", 25);

            // Well‑Fed (ID 26)
            var wellFed = new[]
            {
                "Apple Juice","Bloody Moscato","Bunny Stew","Cooked Fish",
                "Frozen Banana Daiquiri","Fruit Juice","Fruit Salad","Grilled Squirrel",
                "Lemonade","Peach Sangria","Roasted Bird","Smoothie of Darkness",
                "Tropical Smoothie","Teacup","Apple","Apricot","Banana",
                "Blackcurrant","Blood Orange","Cherry","Coconut","Elderberry",
                "Grapefruit","Lemon","Mango","Peach","Pineapple","Plum",
                "Rambutan","Carton of Milk","Potato Chips","Shucked Oyster","Marshmallow"
            };
            foreach (var name in wellFed)
                BuffMap.Add(name, 26);

            // Plenty Satisfied (ID 206)
            var plenty = new[]
            {
                "Grub Soup","Bowl of Soup","Cooked Shrimp","Pumpkin Pie",
                "Sashimi","Escargot","Lobster Tail","Prismatic Punch",
                "Roasted Duck","Sauteed Frog Legs","Pho","Pad Thai",
                "Dragon Fruit","Star Fruit","Banana Split","Chicken Nugget",
                "Chocolate Chip Cookie","Coffee","Cream Soda","Fried Egg",
                "Fries","Grapes","Hotdog","Ice Cream","Nachos"
            };
            foreach (var name in plenty)
                BuffMap.Add(name, 206);

            // Exquisitely Stuffed (ID 207)
            var stuffed = new[]
            {
                "Golden Delight","Grape Juice","Seafood Dinner","Bacon",
                "Christmas Pudding","Gingerbread Cookie","Sugar Cookie",
                "Apple Pie","BBQ Ribs","Burger","Milkshake","Pizza",
                "Spaghetti","Steak"
            };
            foreach (var name in stuffed)
                BuffMap.Add(name, 207);
        }
    }
}