using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Buildings;

namespace StardewRoomRandomizer.Extensions
{
    public static class LocationRequestExtensions
    {
        public static (LocationRequest, Point) PerformLastLocationRequestChanges(this LocationRequest locationRequest, GameLocation origin, Point warpPoint, Point warpPointTarget)
        {
            // locationRequest = MakeBeachNightMarketChanges(locationRequest);
            (locationRequest, warpPointTarget) = MakeFarmToGreenhouseChanges(locationRequest, origin, warpPointTarget);
            // warpPointTarget = MakeIslandSouthChanges(locationRequest, warpPointTarget);
            // Warp offset volcano dungeon
            // Dismount horse on warp
            // (locationRequest, warpPointTarget) = MakeTrailerChanges(locationRequest, warpPointTarget);
            warpPointTarget = MakeFarmChanges(locationRequest, origin, warpPointTarget);
            // Club Bouncer attacks you
            // Bypass festival if exiting the hospital
            // Enter Festival
            return (locationRequest, warpPointTarget);
        }

        private static (LocationRequest, Point) MakeFarmToGreenhouseChanges(LocationRequest locationRequest, GameLocation origin, Point warpPointTarget)
        {
            if (!locationRequest.Name.Equals("Farm") || origin.NameOrUniqueName != "Greenhouse")
            {
                return (locationRequest, warpPointTarget);
            }

            foreach (var warp in origin.warps)
            {
                if (warp.TargetX != warpPointTarget.X || warp.TargetY != warpPointTarget.Y)
                {
                    continue;
                }

                foreach (var building in Game1.getFarm().buildings)
                {
                    if (building is not GreenhouseBuilding greenhouse)
                    {
                        continue;
                    }

                    warpPointTarget = new Point(greenhouse.getPointForHumanDoor().X, greenhouse.getPointForHumanDoor().Y + 1);
                    return (locationRequest, warpPointTarget);
                }

                return (locationRequest, warpPointTarget);
            }

            return (locationRequest, warpPointTarget);
        }

        private static Point MakeFarmChanges(LocationRequest locationRequest, GameLocation origin,
            Point warpPointTarget)
        {
            if (!locationRequest.Name.Equals("Farm"))
            {
                return warpPointTarget;
            }

            var farm = Game1.getFarm();
            warpPointTarget = MakeFarmcaveToFarmChanges(origin, warpPointTarget, farm);
            warpPointTarget = MakeForestToFarmChanges(origin, warpPointTarget, farm);
            warpPointTarget = MakeBusStopToFarmChanges(origin, warpPointTarget, farm);
            warpPointTarget = MakeBackwoodsToFarmChanges(origin, warpPointTarget, farm);
            warpPointTarget = MakeFarmhouseToFarmChanges(origin, warpPointTarget, farm);

            return warpPointTarget;
        }

        private static Point MakeFarmcaveToFarmChanges(GameLocation origin, Point warpPointTarget, Farm farm)
        {
            if (origin.NameOrUniqueName == "FarmCave" && warpPointTarget.X == 34 && warpPointTarget.Y == 6)
            {
                switch (Game1.whichFarm)
                {
                    case 5:
                        warpPointTarget = new Point(30, 36);
                        break;
                    case 6:
                        warpPointTarget = new Point(34, 16);
                        break;
                }

                return farm.GetMapPropertyPosition("FarmCaveEntry", warpPointTarget.X, warpPointTarget.Y);
            }

            return warpPointTarget;
        }

        private static Point MakeForestToFarmChanges(GameLocation origin, Point warpPointTarget, Farm farm)
        {
            if (origin.NameOrUniqueName == "Forest" && warpPointTarget.X == 41 && warpPointTarget.Y == 64)
            {
                switch (Game1.whichFarm)
                {
                    case 5:
                        warpPointTarget = new Point(40, 64);
                        break;
                    case 6:
                        warpPointTarget = new Point(82, 103);
                        break;
                }

                return farm.GetMapPropertyPosition("ForestEntry", warpPointTarget.X, warpPointTarget.Y);
            }

            return warpPointTarget;
        }

        private static Point MakeBusStopToFarmChanges(GameLocation origin, Point warpPointTarget, Farm farm)
        {
            if (origin.NameOrUniqueName == "BusStop" && warpPointTarget.X == 79 && warpPointTarget.Y == 17)
            {
                return farm.GetMapPropertyPosition("BusStopEntry", warpPointTarget.X, warpPointTarget.Y);
            }

            return warpPointTarget;
        }

        private static Point MakeBackwoodsToFarmChanges(GameLocation origin, Point warpPointTarget, Farm farm)
        {
            if (origin.NameOrUniqueName == "Backwoods" && warpPointTarget.X == 40 && warpPointTarget.Y == 0)
            {
                return farm.GetMapPropertyPosition("BackwoodsEntry", warpPointTarget.X, warpPointTarget.Y);
            }

            return warpPointTarget;
        }

        private static Point MakeFarmhouseToFarmChanges(GameLocation origin, Point warpPointTarget, Farm farm)
        {
            if (origin.NameOrUniqueName == "FarmHouse" && warpPointTarget.X == 64 && warpPointTarget.Y == 15)
            {
                return farm.GetMainFarmHouseEntry();
            }

            return warpPointTarget;
        }
    }
}