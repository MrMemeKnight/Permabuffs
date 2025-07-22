using System.Collections.Generic;

namespace Permabuffs
{
    public static class Potions
    {
        public static readonly Dictionary<string, int> Buffs = new Dictionary<string, int>();

        public static void Initialize()
        {
            // BUFF POTIONS
            Buffs["Ammo Reservation Potion"] = 112;
            Buffs["Archery Potion"] = 16;
            Buffs["Battle Potion"] = 13;
            Buffs["Builder Potion"] = 107;
            Buffs["Calming Potion"] = 106;
            Buffs["Crate Potion"] = 123;
            Buffs["Dangersense Potion"] = 111;
            Buffs["Endurance Potion"] = 114;
            Buffs["Featherfall Potion"] = 8;
            Buffs["Fishing Potion"] = 121;
            Buffs["Flipper Potion"] = 109;
            Buffs["Gills Potion"] = 4;
            Buffs["Gravitation Potion"] = 18;
            Buffs["Greater Luck Potion"] = 257;
            Buffs["Heartreach Potion"] = 105;
            Buffs["Hunter Potion"] = 17;
            Buffs["Inferno Potion"] = 116;
            Buffs["Invisibility Potion"] = 10;
            Buffs["Ironskin Potion"] = 5;
            Buffs["Lesser Luck Potion"] = 257;
            Buffs["Lifeforce Potion"] = 113;
            Buffs["Luck Potion"] = 257;
            Buffs["Magic Power Potion"] = 7;
            Buffs["Mana Regeneration Potion"] = 6;
            Buffs["Mining Potion"] = 104;
            Buffs["Night Owl Potion"] = 12;
            Buffs["Obsidian Skin Potion"] = 1;
            Buffs["Rage Potion"] = 115;
            Buffs["Regeneration Potion"] = 2;
            Buffs["Shine Potion"] = 11;
            Buffs["Sonar Potion"] = 122;
            Buffs["Spelunker Potion"] = 9;
            Buffs["Stink Potion"] = 371;
            Buffs["Summoning Potion"] = 110;
            Buffs["Swiftness Potion"] = 3;
            Buffs["Thorns Potion"] = 14;
            Buffs["Titan Potion"] = 108;
            Buffs["Warmth Potion"] = 124;
            Buffs["Water Walking Potion"] = 15;
            Buffs["Wrath Potion"] = 117;
            Buffs["Flask of Cursed Flames"] = 73;
            Buffs["Flask of Fire"] = 74;
            Buffs["Flask of Gold"] = 75;
            Buffs["Flask of Ichor"] = 76;
            Buffs["Flask of Nanites"] = 77;
            Buffs["Flask of Party"] = 78;
            Buffs["Flask of Poison"] = 79;
            Buffs["Flask of Venom"] = 71;

            // WELL FED
            Buffs["Apple Juice"] = 26;
            Buffs["Bloody Moscato"] = 26;
            Buffs["Bunny Stew"] = 26;
            Buffs["Cooked Fish"] = 26;
            Buffs["Frozen Banana Daiquiri"] = 26;
            Buffs["Fruit Juice"] = 26;
            Buffs["Fruit Salad"] = 26;
            Buffs["Grilled Squirrel"] = 26;
            Buffs["Lemonade"] = 26;
            Buffs["Peach Sangria"] = 26;
            Buffs["Roasted Bird"] = 26;
            Buffs["Smoothie of Darkness"] = 26;
            Buffs["Tropical Smoothie"] = 26;
            Buffs["Teacup"] = 26;
            Buffs["Apple"] = 26;
            Buffs["Apricot"] = 26;
            Buffs["Banana"] = 26;
            Buffs["Blackcurrant"] = 26;
            Buffs["Blood Orange"] = 26;
            Buffs["Cherry"] = 26;
            Buffs["Coconut"] = 26;
            Buffs["Elderberry"] = 26;
            Buffs["Grapefruit"] = 26;
            Buffs["Lemon"] = 26;
            Buffs["Mango"] = 26;
            Buffs["Peach"] = 26;
            Buffs["Pineapple"] = 26;
            Buffs["Plum"] = 26;
            Buffs["Rambutan"] = 26;
            Buffs["Carton of Milk"] = 26;
            Buffs["Potato Chips"] = 26;
            Buffs["Shucked Oyster"] = 26;
            Buffs["Marshmallow"] = 26;

            // PLENTY SATISFIED
            Buffs["Grub Soup"] = 206;
            Buffs["Bowl of Soup"] = 206;
            Buffs["Cooked Shrimp"] = 206;
            Buffs["Pumpkin Pie"] = 206;
            Buffs["Sashimi"] = 206;
            Buffs["Escargot"] = 206;
            Buffs["Lobster Tail"] = 206;
            Buffs["Prismatic Punch"] = 206;
            Buffs["Roasted Duck"] = 206;
            Buffs["Sauteed Frog Legs"] = 206;
            Buffs["Pho"] = 206;
            Buffs["Pad Thai"] = 206;
            Buffs["Dragon Fruit"] = 206;
            Buffs["Star Fruit"] = 206;
            Buffs["Banana Split"] = 206;
            Buffs["Chicken Nugget"] = 206;
            Buffs["Chocolate Chip Cookie"] = 206;
            Buffs["Coffee"] = 206;
            Buffs["Cream Soda"] = 206;
            Buffs["Fried Egg"] = 206;
            Buffs["Fries"] = 206;
            Buffs["Grapes"] = 206;
            Buffs["Hotdog"] = 206;
            Buffs["Ice Cream"] = 206;
            Buffs["Nachos"] = 206;

            // EXQUISITELY STUFFED
            Buffs["Golden Delight"] = 207;
            Buffs["Grape Juice"] = 207;
            Buffs["Seafood Dinner"] = 207;
            Buffs["Bacon"] = 207;
            Buffs["Christmas Pudding"] = 207;
            Buffs["Gingerbread Cookie"] = 207;
            Buffs["Sugar Cookie"] = 207;
            Buffs["Apple Pie"] = 207;
            Buffs["BBQ Ribs"] = 207;
            Buffs["Burger"] = 207;
            Buffs["Milkshake"] = 207;
            Buffs["Pizza"] = 207;
            Buffs["Spaghetti"] = 207;
            Buffs["Steak"] = 207;

            // TIPSY
            Buffs["Sake"] = 25;
            Buffs["Ale"] = 25;
        }
    }
}