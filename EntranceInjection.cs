﻿using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewRoomRandomizer.GameModifications.EntranceRandomizer;
using StardewValley;
using System;

namespace StardewRoomRandomizer.GameModifications.CodeInjections
{
    public class EntranceInjections
    {
        private static IMonitor _monitor;
        private static EntranceManager _entranceManager;

        public static void Initialize(IMonitor monitor, EntranceManager entranceManager)
        {
            _monitor = monitor;
            _entranceManager = entranceManager;
        }

        public static bool PerformWarpFarmer_EntranceRandomization_Prefix(ref LocationRequest locationRequest, ref int tileX,
            ref int tileY, ref int facingDirectionAfterWarp)
        {
            try
            {
                if (Game1.currentLocation.Name.ToLower() == locationRequest.Name.ToLower() || Game1.player.passedOut || Game1.player.FarmerSprite.isPassingOut() || Game1.player.isInBed.Value)
                {
                    return true; // run original logic
                }

                var targetPosition = new Point(tileX, tileY);
                var entranceIsReplaced = _entranceManager.TryGetEntranceReplacement(Game1.currentLocation.Name, locationRequest.Name, targetPosition, out var replacedWarp);
                if (!entranceIsReplaced)
                {
                    return true; // run original logic
                }

                locationRequest.Name = replacedWarp.LocationRequest.Name;
                locationRequest.Location = replacedWarp.LocationRequest.Location;
                locationRequest.IsStructure = replacedWarp.LocationRequest.IsStructure;
                tileX = replacedWarp.TileX;
                tileY = replacedWarp.TileY;
                facingDirectionAfterWarp = (int)replacedWarp.FacingDirectionAfterWarp;

                SetCorrectSwimsuitState(locationRequest, tileX, tileY);

                return true; // run original logic
            }
            catch (Exception ex)
            {
                _monitor.Log($"Failed in {nameof(PerformWarpFarmer_EntranceRandomization_Prefix)}:\n{ex}", LogLevel.Error);
                return true; // run original logic
            }
        }

        private static void SetCorrectSwimsuitState(LocationRequest locationRequest, int tileX, int tileY)
        {
            var shouldBeInSwimsuit = GetCorrectSwimsuitState(locationRequest, tileX, tileY);
            if (shouldBeInSwimsuit)
            {
                Game1.player.changeIntoSwimsuit();
            }
            else
            {
                Game1.player.changeOutOfSwimSuit();
            }
        }

        private static bool GetCorrectSwimsuitState(LocationRequest locationRequest, int tileX, int tileY)
        {
            if (locationRequest.Location.Name.Equals("BathHouse_Pool"))
            {
                return true;
            }

            if (!locationRequest.Location.Name.StartsWith("BathHouse_", StringComparison.OrdinalIgnoreCase) ||
                 !locationRequest.Location.Name.EndsWith("Locker", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (locationRequest.Name.Contains("Women", StringComparison.OrdinalIgnoreCase))
            {
                return tileX < 5;
            }
            else
            {
                return tileX > 12;
            }
        }
    }
}