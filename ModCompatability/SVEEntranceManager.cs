using System.Collections.Generic;

namespace StardewRoomRandomizer.ModCompatability
{
    public class SVEEntranceManager
    {

        private static readonly Dictionary<string, string> _sveWarpConversions = new Dictionary<string, string>()
        {
            { "Mountain to AdventureGuild", "Custom_AdventurerSummit to AdventureGuild" },
            { "Mountain to Mine|18|13", "Custom_AdventurerSummit to Mine|18|13" },
            { "Mountain to Mine|67|17", "Custom_AdventurerSummit to Mine|67|17" }
        };

        private static readonly List<string> _sveEarlyMapWarps = new List<string>()
        {
            "Custom_ForestWest to Forest",
            "Forest to Custom_ForestWest",
            "Forest to Custom_BlueMoonVineyard",
            "Custom_BlueMoonVineyard to Forest",
            "Mountain to Custom_AdventurerSummit",
            "Custom_AdventurerSummit to Mountain"
        };

        private static readonly List<string> _sveEarlyWarps = new List<string>()
        {
            "Town to Custom_JenkinsHouse",
            "Custom_JenkinsHouse to Town",
            "Forest to Custom_AndyHouse",
            "Custom_AndyHouse to Forest",
            "Custom_OliviaCellar to Custom_JenkinsHouse",
            "Custom_JenkinsHouse to Custom_OliviaCellar",
            "Custom_SophiaHouse to Custom_BlueMoonVineyard",            
            "Custom_BlueMoonVineyard to Custom_SophiaHouse",   
        };

        private static readonly List<string> _sveMapLocations = new List<string>()
        {
            "Custom_ForestWest",
            "Custom_BlueMoonVineyard",
            "Custom_AdventurerSummit",
            "Custom_CrimsonBadlands",
            "Custom_GrampletonSuburbs",
            "Custom_Highlands",
            "Custom_FableReef",
            "Custom_JunimoWoods",
            "Custom_EnchantedGrove",
            "Custom_SpriteSpring2"
        };

        private static readonly List<string> _sveWarps = new List<string>()
        {
            "Custom_CrimsonBadlands to Custom_TreasureCave",
            "FishShop to Custom_WillyRoom",
            "Museum to Custom_GunthersRoom",
            "Custom_ForestWest to Custom_SpriteSpring2",
            "Custom_GrampletonSuburbs to Custom_ScarlettHouse",
            "Forest to Custom_MarnieShed",
            "Custom_SpriteSpring2 to Custom_EnchantedGrove|20|10",
            "Custom_DesertRailway to Custom_CrimsonBadlands",
            "Custom_GrandpasShedRuins to Custom_GrandpasShedOutside",
            "Custom_EnchantedGrove to Backwoods",
            "Custom_ApplesRoom to Custom_EnchantedGrove|20|41",
            "Custom_DesertRailway to Custom_CastleVillageOutpost",
            "Custom_SusanHouse to Railroad",
            "Town to Custom_JenkinsHouse",
            "Custom_GrandpasShedRuins to Custom_GrandpasShedGreenhouse",
            "Custom_ApplesRoom to Custom_AuroraVineyard",
            "Custom_FableReef to Custom_FirstSlashGuild",
            "Custom_WillyRoom to FishShop",
            "Custom_OliviaCellar to Custom_JenkinsHouse",
            "Custom_FirstSlashGuild to Custom_FirstSlashHallway",
            "Custom_SpriteSpring2 to Custom_ForestWest",
            "Custom_SpriteSpring2 to Custom_SpriteSpringCave",
            "Forest to Custom_AndyHouse",
            "Custom_AndyCellar to Custom_AndyHouse",
            "Custom_AndyHouse to Custom_AndyCellar",
            "Custom_FirstSlashHallway to Custom_FirstSlashGuestRoom",
            "Custom_HighlandsCavern to Custom_HighlandsCavernPrison",
            "Custom_GrandpasShedGreenhouse to Custom_GrandpasShedRuins",
            "Custom_EnchantedGrove|20|10 to Custom_SpriteSpring2",
            "Custom_FableReef to Custom_WizardBasement",
            "Custom_SophiaHouse to Custom_BlueMoonVineyard",
            "Summit to Railroad",
            "Custom_Highlands to Custom_HighlandsOutpost|7|9",
            "Railroad to Custom_SusanHouse",
            "Custom_SpriteSpringCave to Custom_SpriteSpring2",
            "Custom_WizardBasement to Custom_FableReef",
            "Custom_HighlandsOutpost|7|9 to Custom_Highlands",
            "Custom_CastleVillageOutpost to Custom_EnchantedGrove|40|10",
            "Custom_WizardBasement to Custom_EnchantedGrove|17|25",
            "Custom_AndyHouse to Forest",
            "Custom_JunimoWoods to Custom_EnchantedGrove|40|40",
            "Custom_EnchantedGrove|20|41 to Custom_ApplesRoom",
            "Custom_CastleVillageOutpost to Custom_DesertRailway",
            "Custom_MarnieShed to Forest",
            "Custom_EnchantedGrove|40|40 to Custom_JunimoWoods",
            "Custom_FirstSlashGuestRoom to Custom_FirstSlashHallway",
            "Custom_EnchantedGrove|43|25 to Custom_AdventurerSummit",
            "Railroad to Summit",
            "Custom_GunthersRoom to Museum",
            "Custom_JenkinsHouse to Custom_OliviaCellar",
            "Custom_HighlandsCavernPrison to Custom_HighlandsCavern",
            "Custom_FirstSlashHallway to Custom_FirstSlashGuild",
            "Custom_FirstSlashGuild to Custom_FableReef",
            "Custom_HighlandsOutpost|12|5 to Custom_Highlands",
            "Custom_TreasureCave to Custom_CrimsonBadlands",
            "Custom_GrandpasShedOutside to Custom_GrandpasShedRuins",
            "Custom_Highlands to Custom_HighlandsCavern",
            "Custom_JenkinsHouse to Town",
            "Custom_BlueMoonVineyard to Custom_SophiaHouse",
            "Custom_SophiaCellar to Custom_SophiaHouse",
            "Custom_SophiaHouse to Custom_SophiaCellar",
            "Backwoods to Custom_EnchantedGrove",
            "Custom_CrimsonBadlands to Custom_DesertRailway",
            "Custom_EnchantedGrove|17|25 to Custom_WizardBasement",
            "Custom_ScarlettHouse to Custom_GrampletonSuburbs",
            "Custom_ForestWest to Custom_AuroraVineyard",
            "Custom_Highlands to Custom_HighlandsOutpost|12|5",
            "Custom_AuroraVineyard to Custom_ApplesRoom",
            "Custom_EnchantedGrove|40|10 to Custom_CastleVillageOutpost",
            "Custom_HighlandsCavern to Custom_Highlands",
            "Custom_AuroraVineyard to Custom_ForestWest",
            "Custom_AdventurerSummit to Custom_EnchantedGrove|43|25"
        };

        public Dictionary<string, string> GetSVEWarpConversions()
        {
            return _sveWarpConversions;
        }

        public List<string> GetSVEWarps()
        {
            return _sveWarps;
        }

        public List<string> GetSVEEarlyMapWarps()
        {
            return _sveEarlyMapWarps;
        }

        public List<string> GetSVEMaps()
        {
            return _sveMapLocations;
        }

        public List<string> GetSVERequirementWarps()
        {
            List<string> requirementWarps = new List<string>();
            foreach(var warp in _sveWarps)
            {
                if (!_sveEarlyWarps.Contains(warp)){
                    requirementWarps.Add(warp);
                }
            }
            return requirementWarps;
        }
    }
}