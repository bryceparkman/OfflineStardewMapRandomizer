using HarmonyLib;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewRoomRandomizer.GameModifications.CodeInjections;
using StardewRoomRandomizer.GameModifications.EntranceRandomizer;
using StardewValley;
using System.Linq;

namespace StardewRoomRandomizer
{
    internal sealed class ModEntry : Mod
    {
        private ModConfig _config;
        private EntranceManager _entranceManager;
        private bool _hasSVE;
        private Harmony _harmony;
        private Unstucker _unstucker = new Unstucker();

        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.SaveLoaded += OnSaveLoaded;
            helper.Events.GameLoop.DayStarted += OnDayStarted;

            _config = Helper.ReadConfig<ModConfig>();

            _harmony = new Harmony(ModManifest.UniqueID);

            _hasSVE = Helper.ModRegistry.IsLoaded("FlashShifter.SVECode");
            Monitor.Log($"SVE detected?: {_hasSVE}", LogLevel.Debug);

        }

        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            _entranceManager = new EntranceManager(Monitor);
            _entranceManager.SetEntranceRandomizerSettings(_hasSVE, _config);

            EntranceInjections.Initialize(Monitor, _entranceManager);
            _harmony.Patch(
               original: AccessTools.Method(typeof(Game1), "performWarpFarmer"),
               prefix: new HarmonyMethod(typeof(EntranceInjections), nameof(EntranceInjections.PerformWarpFarmer_EntranceRandomization_Prefix))
            );
        }

        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            _entranceManager.ResetCheckedEntrancesToday();
        }

        private void ButtonChanged(object sender, ButtonsChangedEventArgs e)
        {
            if (e.Pressed.Contains(SButton.U))
            {
                if (Context.IsMainPlayer)
                {
                    _unstucker.Unstuck(Game1.player);
                }
                else
                {
                    foreach (Farmer farmer in Game1.getOnlineFarmers())
                    {
                        _unstucker.Unstuck(farmer);
                    } 
                }
            }
        }
    }
}