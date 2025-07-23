using System.Collections.Generic;
using Terraria.ID;

namespace Permabuffs
{
    public class Potions
    {
        public static Dictionary<string, int> buffMap = new Dictionary<string, int>();

        public static void PopulateBuffMap()
        {
            if (buffMap.Count > 0) return;

            buffMap.Add("Ironskin Potion", BuffID.Ironskin);
            buffMap.Add("Swiftness Potion", BuffID.Swiftness);
            buffMap.Add("Regeneration Potion", BuffID.Regeneration);
            buffMap.Add("Shine Potion", BuffID.Shine);
            buffMap.Add("Night Owl Potion", BuffID.NightOwl);
            buffMap.Add("Mining Potion", BuffID.Mining);
            buffMap.Add("Heartreach Potion", BuffID.Heartreach);
            buffMap.Add("Calming Potion", BuffID.Calm);
            buffMap.Add("Builder Potion", BuffID.Builder);
            buffMap.Add("Titan Potion", BuffID.Titan);
            buffMap.Add("Flipper Potion", BuffID.Flipper);
            buffMap.Add("Spelunker Potion", BuffID.Spelunker);
            buffMap.Add("Obsidian Skin Potion", BuffID.ObsidianSkin);
            buffMap.Add("Hunter Potion", BuffID.Hunter);
            buffMap.Add("Gravitation Potion", BuffID.Gravitation);
            buffMap.Add("Thorns Potion", BuffID.Thorns);
            buffMap.Add("Invisibility Potion", BuffID.Invisibility);
            buffMap.Add("Magic Power Potion", BuffID.MagicPower);
            buffMap.Add("Mana Regeneration Potion", BuffID.ManaRegeneration);
            buffMap.Add("Summoning Potion", BuffID.Summoning);
            buffMap.Add("Wrath Potion", BuffID.Wrath);
            buffMap.Add("Rage Potion", BuffID.Rage);
            buffMap.Add("Endurance Potion", BuffID.Endurance);
            buffMap.Add("Lifeforce Potion", BuffID.Lifeforce);
            buffMap.Add("Warmth Potion", BuffID.Warmth);
            buffMap.Add("Archery Potion", BuffID.Archery);
            buffMap.Add("Ammo Reservation Potion", BuffID.AmmoReservation);
            buffMap.Add("Battle Potion", BuffID.Battle);
            buffMap.Add("Fishing Potion", BuffID.Fishing);
            buffMap.Add("Sonar Potion", BuffID.Sonar);
            buffMap.Add("Crate Potion", BuffID.Crate);
            buffMap.Add("Sharpening Station", BuffID.Sharpened);
            buffMap.Add("Tipsy", BuffID.Tipsy);
            buffMap.Add("Ale", BuffID.Tipsy);
            buffMap.Add("Sake", BuffID.Tipsy);
            buffMap.Add("Cooked Fish", BuffID.WellFed);
            buffMap.Add("Cooked Shrimp", BuffID.WellFed);
            buffMap.Add("Bowl of Soup", BuffID.WellFed);
            buffMap.Add("Pumpkin Pie", BuffID.WellFed);
            buffMap.Add("Grilled Squirrel", BuffID.WellFed);
            buffMap.Add("Buggy Pudding", BuffID.WellFed);
            buffMap.Add("Spaghetti", BuffID.WellFed2);
            buffMap.Add("Bacon", BuffID.WellFed2);
            buffMap.Add("Seafood Dinner", BuffID.WellFed3);
            buffMap.Add("Stink Potion", BuffID.Stinky);
        }
    }
}