using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewRoomRandomizer.Extensions;
using StardewRoomRandomizer.ModCompatability;
using StardewRoomRandomizer.Warping;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace StardewRoomRandomizer.GameModifications.EntranceRandomizer
{
    public class EntranceManager
    {
        public const string TRANSITIONAL_STRING = " to ";

        private readonly IMonitor _monitor;
        private readonly EquivalentWarps _equivalentAreas;
        private readonly SVEEntranceManager _modEntranceManager;
        private bool _hasSVE;
        private ModConfig _config;
        public Dictionary<string, string> ModifiedEntrances { get; private set; }
        public Dictionary<string, string> InvertedModifiedEntrances { get; private set; }
        public Dictionary<string, string> UnModifiedEntrances { get; private set; }
        public List<string> warps = new List<string>()
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

        private HashSet<string> _checkedEntrancesToday;
        private Dictionary<string, WarpRequest> generatedWarps;
        private List<string> mapLocations = new List<string>
        {
            "Forest", "Town", "Beach", "Backwoods", "Mountain", "Railroad", "IslandNorth", "IslandWest", "IslandEast", "IslandSouthEast", "Desert", "Farm"
        };
        private List<string> earlyMapWarps = new List<string>
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

        private List<string> requirementWarps = new List<string>
        {
            "Mountain to ScienceHouse|3|8", //Entering Maru's exterior door requires friendship
            "Forest to LeahHouse", //Entering Leah's house requries friendship
            "Beach to ElliotHouse", //Entering Elliot's house requries friendship
            "Forest to WizardHouse", //Entering the Wizard's house requires getting into CC first
            "Mountain to LeoTreeHouse", //Leo's tree house is end-end-game
            "Forest to Sewer", //Requires rusty key
        };

        public EntranceManager(IMonitor monitor)
        {
            _monitor = monitor;
            _equivalentAreas = new EquivalentWarps();
            _modEntranceManager = new SVEEntranceManager();
            generatedWarps = new Dictionary<string, WarpRequest>(StringComparer.OrdinalIgnoreCase);

        }

        public void ResetCheckedEntrancesToday()
        {
            _checkedEntrancesToday = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (_config.RandomizeDoorsEveryDay)
            {
                ShuffleEntrances();
            }
        }

        private bool isAccessibleEarly(string entrance)
        {
            var parts = entrance.Split(TRANSITIONAL_STRING);
            return !requirementWarps.Contains(entrance) && !parts[0].StartsWith("Island") && !parts[0].Equals("Railroad") && !parts[0].Equals("Desert");
        }

        private string FindStartingMap()
        {
            string entrance = "FarmHouse to Farm";
            string[] parts;
            do
            {
                entrance = ModifiedEntrances[entrance];
                parts = entrance.Split(TRANSITIONAL_STRING);
            }
            while (!mapLocations.Contains(parts[1]));
            return parts[1];
        }

        private bool CommunityCenterIsEarly()
        {
            var entrance = "Town to CommunityCenter";
            if (_config.MatchEntrances)
            {
                entrance = InvertedModifiedEntrances[entrance];
                return isAccessibleEarly(entrance);
            }
            else
            {
                string startingMap = "Farm";
                if (_config.RandomizeFarmToFarmHouseDoor)
                {
                    startingMap = FindStartingMap();
                }
                //Find path from starting map to community center. If we don't hit any of the requirement warps we good. Traverse using earlymapwarps and warp
                return false;
            }
        }

        private bool matchedEntrance(string entrance1, string entrance2)
        {
            string[] parts1 = entrance1.Split(TRANSITIONAL_STRING);
            string[] parts2 = entrance2.Split(TRANSITIONAL_STRING);
            bool entrance1IsFromMap = mapLocations.Contains(parts1[0]);
            bool entrance2IsFromMap = mapLocations.Contains(parts2[0]);
            bool entrance1IsToMap = mapLocations.Contains(parts1[1]);
            bool entrance2IsToMap = mapLocations.Contains(parts2[1]);

            if (entrance1IsFromMap != entrance2IsFromMap)
            {
                return false;
            }

            if (entrance1IsToMap != entrance2IsToMap)
            {
                return false;
            }

            return true;
        }

        private void ShuffleEntrances()
        {
            var seed = (int)Game1.uniqueIDForThisGame;
            if (_config.RandomizeDoorsEveryDay)
            {
                seed += (int)Game1.stats.DaysPlayed;
            }
            var random = new Random(seed);
            var numShuffles = UnModifiedEntrances.Count * UnModifiedEntrances.Count;
            var newModifiedEntrances = UnModifiedEntrances.ToDictionary(x => x.Key, x => x.Value);

            do
            {
                for (var i = 0; i < numShuffles; i++)
                {
                    var keys = newModifiedEntrances.Keys.ToArray();
                    var chosenEntrance1 = keys[random.Next(keys.Length)];
                    var chosenEntrance2 = keys[random.Next(keys.Length)];
                    while (_config.MatchEntrances && !matchedEntrance(chosenEntrance1, chosenEntrance2))
                    {
                        chosenEntrance2 = keys[random.Next(keys.Length)];
                    }
                    SwapTwoEntrances(newModifiedEntrances, chosenEntrance1, chosenEntrance2);
                }

                ModifiedEntrances = new Dictionary<string, string>(newModifiedEntrances, StringComparer.OrdinalIgnoreCase);
                InvertedModifiedEntrances = ModifiedEntrances.ToDictionary(x => x.Value, x => x.Key);
            }
            while (_config.GuaranteeEarlyCommunityCenterAccess && !CommunityCenterIsEarly());
        }

        public void SetEntranceRandomizerSettings(bool hasSVE, ModConfig config)
        {
            UnModifiedEntrances = new Dictionary<string, string>();
            ModifiedEntrances = new Dictionary<string, string>();
            _hasSVE = hasSVE;
            _config = config;

            if (config.RandomizeFarmToFarmHouseDoor)
            {
                warps.Add("Farm to FarmHouse");
                warps.Add("FarmHouse to Farm");
            }

            foreach (var warp in warps)
            {
                var aliasedWarp = AliaseFullWarp(warp);
                UnModifiedEntrances.Add(aliasedWarp, aliasedWarp);
            }

            if (_hasSVE)
            {
                foreach (var warp in _modEntranceManager.GetSVEMaps())
                {
                    mapLocations.Add(warp);
                }

                foreach (var warp in _modEntranceManager.GetSVEEarlyMapWarps())
                {
                    earlyMapWarps.Add(warp);
                }

                foreach (var warp in _modEntranceManager.GetSVERequirementWarps())
                {
                    requirementWarps.Add(warp);
                }

                foreach (var warp in _modEntranceManager.GetSVEWarps())
                {
                    var aliasedWarp = AliaseFullWarp(warp);
                    UnModifiedEntrances.Add(aliasedWarp, aliasedWarp);
                }

                
            }

            ShuffleEntrances();
        }

        private static void SwapTwoEntrances(Dictionary<string, string> entrances, string entrance1, string entrance2)
        {
            // Trust me
            var destination1 = entrances[entrance1];
            var destination2 = entrances[entrance2];
            var reversed1 = ReverseKey(entrance1);
            var reversed2 = ReverseKey(entrance2);
            var reversedDestination1 = ReverseKey(destination1);
            var reversedDestination2 = ReverseKey(destination2);
            if (destination2 == reversed1 || destination1 == reversed2)
            {
                return;
            }
            entrances[entrance1] = destination2;
            entrances[reversedDestination1] = reversed2;
            entrances[entrance2] = destination1;
            entrances[reversedDestination2] = reversed1;
        }

        public bool TryGetEntranceReplacement(string currentLocationName, string locationRequestName, Point targetPosition, out WarpRequest warpRequest)
        {
            warpRequest = null;
            var defaultCurrentLocationName = _equivalentAreas.GetDefaultEquivalentEntrance(currentLocationName);
            var defaultLocationRequestName = _equivalentAreas.GetDefaultEquivalentEntrance(locationRequestName);
            targetPosition = targetPosition.CheckSpecialVolcanoEdgeCaseWarp(defaultLocationRequestName);
            var keys = GetKeys(defaultCurrentLocationName, defaultLocationRequestName, targetPosition);

            if (!TryGetModifiedWarpName(keys, out var desiredWarpName))
            {
                return false;
            }

            var correctDesiredWarpName = _equivalentAreas.GetCorrectEquivalentEntrance(desiredWarpName);

            if (_checkedEntrancesToday.Contains(correctDesiredWarpName))
            {
                if (generatedWarps.ContainsKey(correctDesiredWarpName))
                {
                    warpRequest = generatedWarps[correctDesiredWarpName];
                    return true;
                }

                return false;
            }

            return TryFindWarpToDestination(correctDesiredWarpName, out warpRequest);
        }

        private bool TryGetModifiedWarpName(IEnumerable<string> keys, out string desiredWarpName)
        {
            foreach (var key in keys)
            {
                if (ModifiedEntrances.ContainsKey(key))
                {
                    desiredWarpName = ModifiedEntrances[key];
                    return true;
                }
            }

            desiredWarpName = "";
            return false;
        }

        private bool TryFindWarpToDestination(string desiredWarpKey, out WarpRequest warpRequest)
        {
            var (locationOriginName, locationDestinationName) = GetLocationNames(desiredWarpKey);
            _checkedEntrancesToday.Add(desiredWarpKey);

            if (!locationOriginName.TryGetClosestWarpPointTo(ref locationDestinationName, _equivalentAreas, out var locationOrigin, out var warpPoint))
            {
                warpRequest = null;
                return false;
            }

            var warpPointTarget = locationOrigin.GetWarpPointTarget(warpPoint, locationDestinationName, _equivalentAreas);
            var locationDestination = Game1.getLocationFromName(locationDestinationName);
            var locationRequest = new LocationRequest(locationDestinationName, locationDestination.isStructure.Value, locationDestination);
            (locationRequest, warpPointTarget) = locationRequest.PerformLastLocationRequestChanges(locationOrigin, warpPoint, warpPointTarget);
            var warpAwayPoint = locationDestination.GetClosestWarpPointTo(locationOriginName, warpPointTarget);
            var facingDirection = warpPointTarget.GetFacingAwayFrom(warpAwayPoint);
            warpRequest = new WarpRequest(locationRequest, warpPointTarget.X, warpPointTarget.Y, facingDirection);
            generatedWarps[desiredWarpKey] = warpRequest;
            return true;
        }

        private static List<string> GetKeys(string currentLocationName, string locationRequestName,
            Point targetPosition)
        {
            var currentPosition = Game1.player.getTileLocationPoint();
            var currentPositions = new List<Point>();
            var targetPositions = new List<Point>();
            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    currentPositions.Add(new Point(currentPosition.X + x, currentPosition.Y + y));
                    targetPositions.Add(new Point(targetPosition.X + x, targetPosition.Y + y));
                }
            }

            var keys = new List<string>
            {
                GetKey(currentLocationName, locationRequestName)
            };
            keys.AddRange(targetPositions.Select(targetPositionWithOffset => GetKey(currentLocationName, locationRequestName, targetPositionWithOffset)));
            keys.AddRange(currentPositions.Select(currentPositionWithOffset => GetKey(currentLocationName, currentPositionWithOffset, locationRequestName)));
            foreach (var currentPositionWithOffset in currentPositions)
            {
                keys.AddRange(targetPositions.Select(targetPositionWithOffset => GetKey(currentLocationName, currentPositionWithOffset, locationRequestName, targetPositionWithOffset)));
            }

            return keys;
        }

        private static string GetKey(string currentLocationName, string locationRequestName)
        {
            var key = $"{currentLocationName}{TRANSITIONAL_STRING}{locationRequestName}";
            return key;
        }

        private static string GetKey(string currentLocationName, string locationRequestName, Point targetPosition)
        {
            var key = $"{currentLocationName}{TRANSITIONAL_STRING}{locationRequestName}|{targetPosition.X}|{targetPosition.Y}";
            return key;
        }

        private static string GetKey(string currentLocationName, Point currentPosition, string locationRequestName)
        {
            var key = $"{currentLocationName}|{currentPosition.X}|{currentPosition.Y}{TRANSITIONAL_STRING}{locationRequestName}";
            return key;
        }

        private static string GetKey(string currentLocationName, Point currentPosition, string locationRequestName, Point targetPosition)
        {
            var key = $"{currentLocationName}|{currentPosition.X}|{currentPosition.Y}{TRANSITIONAL_STRING}{locationRequestName}|{targetPosition.X}|{targetPosition.Y}";
            return key;
        }

        private static string ReverseKey(string key)
        {
            var parts = key.Split(TRANSITIONAL_STRING);
            return $"{parts[1]}{TRANSITIONAL_STRING}{parts[0]}";
        }

        private static (string, string) GetLocationNames(string key)
        {
            var split = key.Split(TRANSITIONAL_STRING);
            return (split[0], split[1]);
        }

        private string AliaseFullWarp(string key)
        {
            var parts = key.Split(TRANSITIONAL_STRING);
            var newEntrance = $"{AliaseLocation(parts[0])}{TRANSITIONAL_STRING}{AliaseLocation(parts[1])}";

            if (_hasSVE)
            {
                var warpConversions = _modEntranceManager.GetSVEWarpConversions();
                if (warpConversions.ContainsKey(newEntrance))
                {
                    newEntrance = warpConversions[newEntrance];
                }
                else if (warpConversions.ContainsKey(ReverseKey(newEntrance)))
                {
                    newEntrance = ReverseKey(warpConversions[ReverseKey(newEntrance)]);
                }
            }

            return newEntrance;
        }

        private string AliaseLocation(string location)
        {
            var modifiedString = location;
            if (modifiedString.Contains("{0}"))
            {
                modifiedString = string.Format(modifiedString, Game1.player.IsMale ? "Mens" : "Womens");
            }
            if (_hasSVE && modifiedString == "WizardHouseBasement")
            {
                modifiedString = "Custom_WizardBasement";
            }

            return modifiedString;
        }
    }

    public enum FacingDirection
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
    }
}