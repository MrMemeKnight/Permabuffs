using System.Collections.Generic;

namespace Permabuffs
{
    public class Potions
    {
        public static Dictionary<string, int> buffIDs = new Dictionary<string, int>();

        public static void Initialize()
        {
            buffIDs["swiftness"] = 3;
            buffIDs["ironskin"] = 5;
            buffIDs["regeneration"] = 2;
            buffIDs["mana regeneration"] = 6;
            buffIDs["magic power"] = 7;
            buffIDs["archery"] = 4;
            buffIDs["thorns"] = 14;
            buffIDs["invisibility"] = 10;
            buffIDs["night owl"] = 12;
            buffIDs["shine"] = 11;
            buffIDs["hunter"] = 9;
            buffIDs["spelunker"] = 8;
            buffIDs["gills"] = 1;
            buffIDs["featherfall"] = 2;
            buffIDs["gravitation"] = 17;
            buffIDs["obsidian skin"] = 1;
            buffIDs["water walking"] = 13;
            buffIDs["endurance"] = 110;
            buffIDs["lifeforce"] = 112;
            buffIDs["wrath"] = 115;
            buffIDs["rage"] = 114;
            buffIDs["ammo reservation"] = 17;
            buffIDs["inferno"] = 116;
            buffIDs["calm"] = 117;
            buffIDs["crate"] = 118;
            buffIDs["fishing"] = 119;
            buffIDs["sonar"] = 120;
            buffIDs["battle"] = 111;
            buffIDs["builder"] = 98;
            buffIDs["titan"] = 21;
            buffIDs["flipper"] = 9;
            buffIDs["dangersense"] = 19;
            buffIDs["teleportation"] = 100; // doesn't give buff but good placeholder
            buffIDs["warmth"] = 113;
            buffIDs["summoning"] = 113;
            buffIDs["heartreach"] = 105;
            buffIDs["withered armor"] = 69; // optional: skip debuffs if needed
            buffIDs["clarity"] = 122;
            buffIDs["luck"] = 124;
            buffIDs["gravity"] = 18;
            buffIDs["well fed"] = 26;
            buffIDs["well fed 2"] = 206;
            buffIDs["well fed 3"] = 207;
        }
    }
}