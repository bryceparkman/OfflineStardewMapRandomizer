using System.Collections.Generic;
using StardewRoomRandomizer.Constants;

namespace StardewRoomRandomizer.ModCompatability
{
    public class SVEEntranceManager
    {
        public List<string> GetSVERequirementWarps()
        {
            List<string> requirementWarps = new();
            foreach(var warp in SVEMapData.sveWarps)
            {
                if (!SVEMapData.sveEarlyWarps.Contains(warp)){
                    requirementWarps.Add(warp);
                }
            }
            return requirementWarps;
        }
    }
}