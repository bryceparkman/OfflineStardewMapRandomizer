using System.Collections.Generic;

namespace StardewRoomRandomizer.Constants
{
    public class VanillaMapData
    {
        public static List<string> warps = new()
        {
            "Farm to FarmCave",
            "Tent to Mountain",
            "WitchHut to WizardHouseBasement",
            "Blacksmith to Town",
            "Town to Trailer",
            "ManorHouse to Town",
            "Town to ArchaeologyHouse",
            "BathHouse_{0}Locker to BathHouse_Entry",
            "Beach to FishShop",
            "Sunroom to SeedShop",
            "IslandNorth to IslandFieldOffice",
            "QiNutRoom to IslandWest",
            "Mountain to AdventureGuild",
            "Hospital to Town",
            "Town to Hospital",
            "CaptainRoom to IslandWest",
            "IslandSouthEast to IslandSouthEastCave",
            "AnimalShop to Forest",
            "IslandWest to IslandFarmCave",
            "BoatTunnel to FishShop",
            "Mountain to LeoTreeHouse",
            "IslandNorth|12|31 to VolcanoDungeon0|6|49",
            "Town to JojaMart",
            "IslandNorthCave1 to IslandNorth",
            "Town to SeedShop",
            "SebastianRoom to ScienceHouse",
            "IslandWest to CaptainRoom",
            "WitchHut to WitchSwamp",
            "IslandWest to IslandWestCave1",
            "Saloon to Town",
            "Beach to ElliottHouse",
            "WitchSwamp to WitchWarpCave",
            "BathHouse_Entry to BathHouse_{0}Locker",
            "Witch Warp Cave to Railroad",
            "Island North to IslandNorthCave1",
            "IslandSouthEastCave to IslandSouthEast",
            "Town to Blacksmith",
            "ArchaeologyHouse to Town",
            "Town to JoshHouse",
            "IslandWestCave1 to IslandWest",
            "WitchSwamp to WitchHut",
            "AdventureGuild to Mountain",
            "IslandNorth to VolcanoDungeon0|31|53",
            "VolcanoDungeon0|31|53 to IslandNorth",
            "IslandWest to QiNutRoom",
            "LeahHouse to Forest",
            "Backwoods to Tunnel",
            "ScienceHouse|6|24 to Mountain",
            "SandyHouse to Desert",
            "IslandEast to IslandShrine",
            "SkullCave to Desert",
            "Mountain to Mine|67|17",
            "LeoTreeHouse to Mountain",
            "Mountain to ScienceHouse|6|24",
            "IslandShrine to IslandEast",
            "Town to CommunityCenter",
            "WizardHouse to Forest",
            "SandyHouse to Club",
            "Club to SandyHouse",
            "Town to ManorHouse",
            "Mine|67|17 to Mountain",
            "BathHouse_{0}Locker to BathHouse_Pool",
            "HaleyHouse to Town",
            "Town to SamHouse",
            "FishShop to Beach",
            "Sewer to BugLand",
            "IslandFarmCave to IslandWest",
            "Mountain to ScienceHouse|3|8",
            "CommunityCenter to Town",
            "WitchWarpCave to WitchSwamp",
            "ScienceHouse to SebastianRoom",
            "Tunnel to Backwoods",
            "Mountain to Mine|18|13",
            "IslandHut to IslandEast",
            "Forest to LeahHouse",
            "WizardHouseBasement to WizardHouse",
            "VolcanoDungeon0|6|49 to IslandNorth|12|31",
            "Sewer to Forest",
            "Forest to WizardHouse",
            "BathHouse_Entry to Railroad",
            "Town to HaleyHouse",
            "ElliottHouse to Beach",
            "Forest to Sewer",
            "Mine|18|13 to Mountain",
            "Forest to AnimalShop",
            "SamHouse to Town",
            "IslandEast to IslandHut",
            "IslandFieldOffice to IslandNorth",
            "Town to Saloon",
            "Trailer to Town",
            "Hospital to HarveyRoom",
            "Sewer to Town",
            "FishShop to BoatTunnel",
            "BugLand to Sewer",
            "Mountain to Tent",
            "HarveyRoom to Hospital",
            "Desert to SkullCave",
            "WizardHouse to WizardHouseBasement",
            "BathHouse_Pool to BathHouse_{0}Locker",
            "Town to Sewer",
            "SeedShop to Town",
            "Railroad to BathHouse_Entry",
            "FarmCave to Farm",
            "Railroad to WitchWarpCave",
            "ScienceHouse|3|8 to Mountain",
            "SeedShop to Sunroom",
            "JojaMart to Town",
            "Desert to SandyHouse",
            "JoshHouse to Town"
        };

        public static List<string> mapLocations = new()
        {
            "Forest", "Town", "Beach", "Backwoods", "Mountain", "Railroad", "IslandNorth", "IslandWest", "IslandEast", "IslandSouthEast", "Desert", "Farm"
        };

        public static List<string> earlyMapWarps = new()
        {
            "Farm to BusStop",
            "BusStop to Farm",
            "Farm to Forest",
            "Forest to Farm",
            "Farm to Backwoods",
            "Backwoods to Farm",
            "BusStop to Town",
            "Town to BusStop",
            "Forest to Town",
            "Town to Forest",
            "Backwoods to Mountain",
            "Mountain to Backwoods",
            "Town to Mountain",
            "Mountain to Town",
            "Town to Beach",
            "Beach to Town",
            "Mountain to Railroad",
            "Railroad to Mountain",
            "IslandEast to IslandNorth",
            "IslandNorth to IslandEast",
            "IslandEast to IslandWest",
            "IslandWest to IslandEast"
        };

        public static List<string> requirementWarps = new()
        {
            "Mountain to ScienceHouse|3|8", //Entering Maru's exterior door requires friendship
            "Forest to LeahHouse", //Entering Leah's house requries friendship
            "Beach to ElliotHouse", //Entering Elliot's house requries friendship
            "Forest to WizardHouse", //Entering the Wizard's house requires getting into CC first
            "Mountain to LeoTreeHouse", //Leo's tree house is end-end-game
            "Forest to Sewer", //Requires rusty key
        };
    }
}