﻿using StardewArchipelago.Archipelago;
using StardewArchipelago.Locations.Festival;
using StardewModdingAPI;
using StardewArchipelago.Stardew;
using StardewArchipelago.Locations.CodeInjections.Vanilla;
using StardewArchipelago.Locations.CodeInjections.Vanilla.Quests;
using StardewArchipelago.Locations.CodeInjections.Vanilla.Relationship;
using StardewArchipelago.Serialization;
using StardewArchipelago.Locations.CodeInjections.Vanilla.MonsterSlayer;
using StardewArchipelago.Serialization;

namespace StardewArchipelago.Locations.CodeInjections.Initializers
{
    public static class VanillaCodeInjectionInitializer
    {
        public static void Initialize(IMonitor monitor, IModHelper modHelper, ArchipelagoClient archipelago, ArchipelagoStateDto state, BundleReader bundleReader, LocationChecker locationChecker, StardewItemManager itemManager, WeaponsManager weaponsManager)
        {
            var shopReplacer = new ShopReplacer(monitor, modHelper, archipelago, locationChecker);
            BackpackInjections.Initialize(monitor, archipelago, locationChecker);
            ToolInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
            ScytheInjections.Initialize(monitor, locationChecker);
            FishingRodInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
            CommunityCenterInjections.Initialize(monitor, archipelago, bundleReader, locationChecker);
            MineshaftInjections.Initialize(monitor, archipelago, locationChecker);
            SkillInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
            QuestInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
            DarkTalismanInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
            CarpenterInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
            WizardInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
            IsolatedEventInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
            AdventurerGuildInjections.Initialize(monitor, modHelper, archipelago, locationChecker, weaponsManager);
            ArcadeMachineInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
            TravelingMerchantInjections.Initialize(monitor, modHelper, archipelago, locationChecker, archipelagoState);
            FishingInjections.Initialize(monitor, modHelper, archipelago, locationChecker, itemManager);
            MuseumInjections.Initialize(monitor, modHelper, archipelago, locationChecker, itemManager);
            FriendshipInjections.Initialize(monitor, modHelper, archipelago, locationChecker, itemManager);
            SpecialOrderInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
            PregnancyInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
            CropsanityInjections.Initialize(monitor, archipelago, locationChecker, itemManager);
            InitializeFestivalPatches(monitor, modHelper, archipelago, locationChecker, shopReplacer);
            ShippingInjections.Initialize(monitor, archipelago, locationChecker);
            MonsterSlayerInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
            CookingInjections.Initialize(monitor, archipelago, locationChecker, itemManager);
            QueenOfSauceInjections.Initialize(monitor, modHelper, archipelago, state, locationChecker, itemManager);
            RecipePurchaseInjections.Initialize(monitor, modHelper, archipelago, locationChecker, itemManager);
            RecipeDataInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
            RecipeLevelUpInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
        }

        private static void InitializeFestivalPatches(IMonitor monitor, IModHelper modHelper, ArchipelagoClient archipelago,
            LocationChecker locationChecker, ShopReplacer shopReplacer)
        {
            EggFestivalInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
            FlowerDanceInjections.Initialize(monitor, modHelper, archipelago, locationChecker, shopReplacer);
            LuauInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
            MoonlightJelliesInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
            FairInjections.Initialize(monitor, modHelper, archipelago, locationChecker, shopReplacer);
            SpiritEveInjections.Initialize(monitor, modHelper, archipelago, locationChecker, shopReplacer);
            IceFestivalInjections.Initialize(monitor, modHelper, archipelago, locationChecker, shopReplacer);
            MermaidHouseInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
            BeachNightMarketInjections.Initialize(monitor, modHelper, archipelago, locationChecker, shopReplacer);
            WinterStarInjections.Initialize(monitor, modHelper, archipelago, locationChecker);
        }
    }
}
