﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using StardewRoomRandomizer.Extensions;
using StardewValley;
using StardewValley.Locations;

namespace StardewRoomRandomizer
{
    public class Unstucker
    {
        public bool CanPathFindToAnyWarp(GameLocation location, Point startPoint, int minimumDistance = 0, int maximumDistance = 500)
        {
            if (location.warps == null || location.warps.Count < 1)
            {
                return false;
            }

            if (location.isCollidingPosition(new Microsoft.Xna.Framework.Rectangle(startPoint.X * 64 + 1, startPoint.Y * 64 + 1, 62, 62),
                    Game1.viewport, true, 0, false, Game1.player, true))
            {
                return false;
            }

            foreach (var warp in location.warps)
            {
                var endPoint = new Point(warp.X, warp.Y);
                var endPointFunction = new PathFindController.isAtEnd(PathFindController.isAtEndPoint);
                var character = (Character)Game1.player;
                var path = PathFindController.findPath(startPoint, endPoint, endPointFunction, location, character, 250);
                if (path != null && path.Count >= minimumDistance && path.Count <= maximumDistance)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Unstuck(Farmer player)
        {
            var map = player.currentLocation;
            var tiles = new List<Point>();
            var islandSouth = map as IslandSouth;
            for (var x = 0; x < map.Map.Layers[0].LayerWidth; x++)
            {
                for (var y = 0; y < map.Map.Layers[0].LayerHeight; y++)
                {
                    var tilePosition = new Rectangle(x * 64 + 1, y * 64 + 1, 62, 62);
                    if (map.isCollidingPosition(tilePosition, Game1.viewport, true, 0, false, Game1.player))
                    {
                        continue;
                    }

                    tiles.Add(new Point(x, y));
                }
            }

            tiles = tiles.OrderBy(x => x.GetTotalDistance(player.getTileLocationPoint())).ToList();

            foreach (var tile in tiles)
            {
                if (CanPathFindToAnyWarp(map, tile, 2))
                {
                    player.setTileLocation(new Vector2(tile.X, tile.Y));
                    return true;
                }
            }

            return false;
        }
    }
}