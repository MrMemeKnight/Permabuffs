using System.Collections.Generic;
using Terraria.ID;

namespace Permabuffs
{
    /// <summary>
    /// Maps item IDs to corresponding buff IDs for potions, flasks, and food.
    /// Used to grant buffs when players have 30+ of an item in the Piggy Bank.
    /// </summary>
    public static class Potions
    {
        public static Dictionary<int, int> Map = new Dictionary<int, int>();

        public static void Initialize()
        {
            Map.Clear();

            // Standard Buff Potions
            Map[ItemID.AmmoReservationPotion] = BuffID.AmmoReservation;
            Map[ItemID.ArcheryPotion] = BuffID.Archery;
            Map[ItemID.BattlePotion] = BuffID.Battle;
            Map[ItemID.BuilderPotion] = BuffID.Builder;
            Map[ItemID.CalmPotion] = BuffID.Calm;
            Map[ItemID.CratePotion] = BuffID.Crate;
            Map[ItemID.DangersensePotion] = BuffID.Dangersense;
            Map[ItemID.EndurancePotion] = BuffID.Endurance;
            Map[ItemID.FeatherfallPotion] = BuffID.Featherfall;
            Map[ItemID.FishingPotion] = BuffID.Fishing;
            Map[ItemID.FlipperPotion] = BuffID.Flipper;
            Map[ItemID.GillsPotion] = BuffID.Gills;
            Map[ItemID.GravitationPotion] = BuffID.Gravitation;
            Map[ItemID.GreaterLuckPotion] = BuffID.Lucky;
            Map[ItemID.HeartreachPotion] = BuffID.Heartreach;
            Map[ItemID.HunterPotion] = BuffID.Hunter;
            Map[ItemID.InfernoPotion] = BuffID.Inferno;
            Map[ItemID.InvisibilityPotion] = BuffID.Invisibility;
            Map[ItemID.IronskinPotion] = BuffID.Ironskin;
            Map[ItemID.LifeforcePotion] = BuffID.Lifeforce;
            Map[ItemID.LesserLuckPotion] = BuffID.Lucky;
            Map[ItemID.LuckPotion] = BuffID.Lucky;
            Map[ItemID.MagicPowerPotion] = BuffID.MagicPower;
            Map[ItemID.ManaRegenerationPotion] = BuffID.ManaRegeneration;
            Map[ItemID.MiningPotion] = BuffID.Mining;
            Map[ItemID.NightOwlPotion] = BuffID.NightOwl;
            Map[ItemID.ObsidianSkinPotion] = BuffID.ObsidianSkin;
            Map[ItemID.RagePotion] = BuffID.Rage;
            Map[ItemID.RegenerationPotion] = BuffID.Regeneration;
            Map[ItemID.ShinePotion] = BuffID.Shine;
            Map[ItemID.SonarPotion] = BuffID.Sonar;
            Map[ItemID.SpelunkerPotion] = BuffID.Spelunker;
            Map[ItemID.StinkPotion] = BuffID.Stink;
            Map[ItemID.SummoningPotion] = BuffID.Summoning;
            Map[ItemID.SwiftnessPotion] = BuffID.Swiftness;
            Map[ItemID.ThornsPotion] = BuffID.Thorns;
            Map[ItemID.TitanPotion] = BuffID.Titan;
            Map[ItemID.WarmthPotion] = BuffID.Warmth;
            Map[ItemID.WaterWalkingPotion] = BuffID.WaterWalking;
            Map[ItemID.WrathPotion] = BuffID.Wrath;

            // Flasks
            Map[ItemID.FlaskofCursedFlames] = BuffID.CursedInferno;
            Map[ItemID.FlaskofFire] = BuffID.Inferno;
            Map[ItemID.FlaskofGold] = BuffID.Gold;
            Map[ItemID.FlaskofIchor] = BuffID.Ichor;
            Map[ItemID.FlaskofNanites] = BuffID.Nanites;
            Map[ItemID.FlaskofParty] = BuffID.Party;
            Map[ItemID.FlaskofPoison] = BuffID.Poisoned;
            Map[ItemID.FlaskofVenom] = BuffID.Venom;

            // Food Buffs (Well Fed Tiers)
            var wellFedItems = new int[]
            {
                ItemID.AppleJuice, ItemID.BloodyMoscato, ItemID.BunnyStew,
                ItemID.CookedFish, ItemID.FrozenBananaDaiquiri,
                ItemID.FruitJuice, ItemID.FruitSalad, ItemID.GrilledSquirrel,
                ItemID.Lemonade, ItemID.PeachSangria, ItemID.RoastedBird,
                ItemID.SmoothieofDarkness, ItemID.TropicalSmoothie,
                ItemID.Teacup, ItemID.Apple, ItemID.Apricot, ItemID.Banana,
                ItemID.BlackCurrant, ItemID.BloodOrange, ItemID.Cherry,
                ItemID.Coconut, ItemID.Elderberry, ItemID.Grapefruit,
                ItemID.Lemon, ItemID.Mango, ItemID.Peach, ItemID.Pineapple,
                ItemID.Plum, ItemID.Rambutan, ItemID.CartonofMilk,
                ItemID.PotatoChips, ItemID.ShuckedOyster, ItemID.Marshmallow
            };
            foreach (var id in wellFedItems) Map[id] = BuffID.WellFed;

            var plentyItems = new int[]
            {
                ItemID.GrubSoup, ItemID.BowlofSoup, ItemID.CookedShrimp,
                ItemID.PumpkinPie, ItemID.Sashimi, ItemID.Escargot,
                ItemID.LobsterTail, ItemID.PrismaticPunch, ItemID.RoastedDuck,
                ItemID.SauteedFrogLegs, ItemID.Pho, ItemID.PadThai,
                ItemID.DragonFruit, ItemID.StarFruit, ItemID.BananaSplit,
                ItemID.ChickenNugget, ItemID.ChocolateChipCookie,
                ItemID.Coffee, ItemID.CreamSoda, ItemID.FriedEgg,
                ItemID.Fries, ItemID.Grapes, ItemID.Hotdog,
                ItemID.IceCream, ItemID.Nachos
            };
            foreach (var id in plentyItems) Map[id] = BuffID.WellFed2;

            var exaltedItems = new int[]
            {
                ItemID.GoldenDelight, ItemID.GrapeJuice, ItemID.SeafoodDinner,
                ItemID.Bacon, ItemID.ChristmasPudding, ItemID.GingerbreadCookie,
                ItemID.SugarCookie, ItemID.ApplePie, ItemID.BBQRibs,
                ItemID.Burger, ItemID.Milkshake, ItemID.Pizza,
                ItemID.Spaghetti, ItemID.Steak
            };
            foreach (var id in exaltedItems) Map[id] = BuffID.WellFed3;
        }
    }
}