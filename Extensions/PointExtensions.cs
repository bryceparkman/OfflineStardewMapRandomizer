using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using StardewRoomRandomizer.GameModifications.EntranceRandomizer;

namespace StardewRoomRandomizer.Extensions
{
    public static class PointExtensions
    {
        public static int GetTotalDistance(this Point point1, Point point2)
        {
            return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
        }

        public static FacingDirection GetFacingAwayFrom(this Point currentPoint, Point otherPoint)
        {
            if (currentPoint == otherPoint)
            {
                return FacingDirection.Down;
            }

            if (currentPoint.Y == otherPoint.Y)
            {
                return currentPoint.X > otherPoint.X ? FacingDirection.Right : FacingDirection.Left;
            }

            return currentPoint.Y > otherPoint.Y ? FacingDirection.Down : FacingDirection.Up;
        }
    }
}