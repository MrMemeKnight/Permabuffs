using System.Collections.Generic;

namespace Permabuffs
{
    /// <summary>
    /// Creates a dictionary mapping all potion, food, flask, and drink names
    /// to their corresponding buff IDs for permabuff detection.
    /// </summary>
    public class Potions
    {
        public static Dictionary<string, int> BuffMap = new Dictionary<string, int>();

        public static void PopulateBuffMap()
        {
            // BUFF POTIONS
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
            BuffMap.Add("Greater Luck Potion", 257);
            BuffMap.Add("Heartreach Potion", 105);
            BuffMap.Add("Hunter Potion", 17);
            BuffMap.Add("Inferno Potion", 116);
            BuffMap.Add("Invisibility Potion", 10);
            BuffMap.Add("Ironskin Potion", 5);
            BuffMap.Add("Lesser Luck Potion", 257);
            BuffMap.Add("Lifeforce Potion", 113);
            BuffMap.Add("Luck Potion", 257);
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

            // FLASKS
            BuffMap.Add("Flask of Cursed Flames", 73);
            BuffMap.Add("Flask of Fire", 74);
            BuffMap.Add("Flask of Gold", 75);
            BuffMap.Add("Flask of Ichor", 76);
            BuffMap.Add("Flask of Nanites", 77);
            BuffMap.Add("Flask of Party", 78);
            BuffMap.Add("Flask of Poison", 79);
            BuffMap.Add("Flask of Venom", 71);

            // WELL FED (Buff ID 26)
            BuffMap.Add("Apple Juice", 26);
            BuffMap.Add("Bloody Moscato", 26);
            BuffMap.Add("Bunny Stew", 26);
            BuffMap.Add("Cooked Fish", 26);
            BuffMap.Add("Frozen Banana Daiquiri", 26);
            BuffMap.Add("Fruit Juice", 26);
            BuffMap.Add("Fruit Salad", 26);
            BuffMap.Add("Grilled Squirrel", 26);
            BuffMap.Add("Lemonade", 26);
            BuffMap.Add("Peach Sangria", 26);
            // BuffMap.Add("Piña Colada", 26); // Uncomment if needed, confirm exact name
            BuffMap.Add("Roasted Bird", 26);
            BuffMap.Add("Smoothie of Darkness", 26);
            BuffMap.Add("Tropical Smoothie", 26);
            BuffMap.Add("Teacup", 26);
            BuffMap.Add("Apple", 26);
            BuffMap.Add("Apricot", 26);
            BuffMap.Add("Banana", 26);
            BuffMap.Add("Blackcurrant", 26);
            BuffMap.Add("Blood Orange", 26);
            BuffMap.Add("Cherry", 26);
            BuffMap.Add("Coconut", 26);
            BuffMap.Add("Elderberry", 26);
            BuffMap.Add("Grapefruit", 26);
            BuffMap.Add("Lemon", 26);
            BuffMap.Add("Mango", 26);
            BuffMap.Add("Peach", 26);
            BuffMap.Add("Pineapple", 26);
            BuffMap.Add("Plum", 26);
            BuffMap.Add("Rambutan", 26);
            BuffMap.Add("Carton of Milk", 26);
            BuffMap.Add("Potato Chips", 26);
            BuffMap.Add("Shucked Oyster", 26);
            BuffMap.Add("Marshmallow", 26);

            // PLENTY SATISFIED (Buff ID 206)
            BuffMap.Add("Grub Soup", 206);
            BuffMap.Add("Bowl of Soup", 206);
            BuffMap.Add("Cooked Shrimp", 206);
            BuffMap.Add("Pumpkin Pie", 206);
            BuffMap.Add("Sashimi", 206);
            BuffMap.Add("Escargot", 206);
            BuffMap.Add("Lobster Tail", 206);
            BuffMap.Add("Prismatic Punch", 206);
            BuffMap.Add("Roasted Duck", 206);
            BuffMap.Add("Sauteed Frog Legs", 206);
            BuffMap.Add("Pho", 206);
            BuffMap.Add("Pad Thai", 206);
            BuffMap.Add("Dragon Fruit", 206);
            BuffMap.Add("Star Fruit", 206);
            BuffMap.Add("Banana Split", 206);
            BuffMap.Add("Chicken Nugget", 206);
            BuffMap.Add("Chocolate Chip Cookie", 206);
            BuffMap.Add("Coffee", 206);
            BuffMap.Add("Cream Soda", 206);
            BuffMap.Add("Fried Egg", 206);
            BuffMap.Add("Fries", 206);
            BuffMap.Add("Grapes", 206);
            BuffMap.Add("Hotdog", 206);
            BuffMap.Add("Ice Cream", 206);
            BuffMap.Add("Nachos", 206);
            // BuffMap.Add("Shrimp Po' Boy", 206); // Uncomment if needed, confirm exact name

            // EXQUISITELY STUFFED (Buff ID 207)
            BuffMap.Add("Golden Delight", 207);
            BuffMap.Add("Grape Juice", 207);
            BuffMap.Add("Seafood Dinner", 207);
            BuffMap.Add("Bacon", 207);
            BuffMap.Add("Christmas Pudding", 207);
            BuffMap.Add("Gingerbread Cookie", 207);
            BuffMap.Add("Sugar Cookie", 207);
            BuffMap.Add("Apple Pie", 207);
            BuffMap.Add("BBQ Ribs", 207);
            BuffMap.Add("Burger", 207);
            BuffMap.Add("Milkshake", 207);
            BuffMap.Add("Pizza", 207);
            BuffMap.Add("Spaghetti", 207);
            BuffMap.Add("Steak", 207);

            // TIPSY (Buff ID 25)
            BuffMap.Add("Sake", 25);
            BuffMap.Add("Ale", 25);
        }
    }
}