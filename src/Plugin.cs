using HarmonyLib;
using UnityEngine;


#if BEPINEX
using BepInEx;
using BepInEx.Configuration;

namespace TweaksOfYore {
    [BepInPlugin("com.github.Kaden5480.poy-tweaks-of-yore", "Tweaks of Yore", PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin {
        /**
         * <summary>
         * Executes when the plugin is being loaded.
         * </summary>
         */
        public void Awake() {
            // Entities
            config.entities.disableCabinGoat = Config.Bind(
                "Entities", "disableCabinGoat", false,
                "Whether to disable the goat on the alps cabin"
            );
            config.entities.disableEagles = Config.Bind(
                "Entities", "disableEagles", false,
                "Whether to disable eagles once they have all been collected"
            );
            config.entities.disableSwans = Config.Bind(
                "Entities", "disableSwans", false,
                "Whether to disable swans at the castle"
            );
            config.entities.lowerMarasArchSeagullVolume = Config.Bind(
                "Entities", "lowerMarasArchSeagullVolume", false,
                "Whether to lower the volume of seagulls at Mara's Arch"
            );

            // Inventory
            config.inventory.disableBeltRopeDetach = Config.Bind(
                "Inventory", "disableBeltRopeDetach", false,
                "Whether to disable detaching ropes by looking at your belt and pressing the interact bind"
            );

            // UI
            config.ui.disableCruxNotifications = Config.Bind(
                "UI", "disableCruxNotifications", false,
                "Whether to disable crux notifications"
            );
            config.ui.disableSubtitles = Config.Bind(
                "UI", "disableSubtitles", false,
                "Whether to disable subtitles"
            );

            // Misc
            config.misc.skipCleaningItems = Config.Bind(
                "Misc", "skipCleaningItems", false,
                "Whether to skip cleaning items after collecting them"
            );

            // == Patching ==
            // Entities
            Harmony.CreateAndPatchAll(typeof(Patches.Entities.DisableCabinGoat));
            Harmony.CreateAndPatchAll(typeof(Patches.Entities.DisableEagles));
            Harmony.CreateAndPatchAll(typeof(Patches.Entities.DisableSwans));
            Harmony.CreateAndPatchAll(typeof(Patches.Entities.LowerMarasArchSeagullVolume));

            // Inventory
            Harmony.CreateAndPatchAll(typeof(Patches.Inv.DisableBeltRopeDetach));

            // UI
            Harmony.CreateAndPatchAll(typeof(Patches.UI.DisableCruxNotifications));
            Harmony.CreateAndPatchAll(typeof(Patches.UI.DisableSubtitlesNPCClimber));
            Harmony.CreateAndPatchAll(typeof(Patches.UI.DisableSubtitlesNPCSystem));

            // Misc
            Harmony.CreateAndPatchAll(typeof(Patches.Misc.SkipCleaningItems));
        }


#elif MELONLOADER
using MelonLoader;
using MelonLoader.Utils;

[assembly: MelonInfo(typeof(TweaksOfYore.Plugin), "Tweaks of Yore", PluginInfo.PLUGIN_VERSION, "Kaden5480")]
[assembly: MelonGame("TraipseWare", "Peaks of Yore")]

namespace TweaksOfYore {
    public class Plugin: MelonMod {
        public override void OnInitializeMelon() {
            string filePath = $"{MelonEnvironment.UserDataDirectory}/com.github.Kaden5480.poy-tweaks-of-yore.cfg";

            // Entities
            MelonPreferences_Category entities = MelonPreferences.CreateCategory("TweaksOfYore_Entities");
            entities.SetFilePath(filePath);

            config.entities.disableCabinGoat = entities.CreateEntry<bool>("disableCabinGoat", false);
            config.entities.disableEagles = entities.CreateEntry<bool>("disableEagles", false);
            config.entities.disableSwans = entities.CreateEntry<bool>("disableSwans", false);
            config.entities.lowerMarasArchSeagullVolume = entities.CreateEntry<bool>("lowerMarasArchSeagullVolume", false);

            // Inventory
            MelonPreferences_Category inventory = MelonPreferences.CreateCategory("TweaksOfYore_Inventory");
            inventory.SetFilePath(filePath);

            config.inventory.disableBeltRopeDetach = inventory.CreateEntry<bool>("disableBeltRopeDetach", false);

            // UI
            MelonPreferences_Category ui = MelonPreferences.CreateCategory("TweaksOfYore_UI");
            ui.SetFilePath(filePath);

            config.ui.disableCruxNotifications = ui.CreateEntry<bool>("disableCruxNotifications", false);
            config.ui.disableSubtitles = ui.CreateEntry<bool>("disableSubtitles", false);

            // Misc
            MelonPreferences_Category misc = MelonPreferences.CreateCategory("TweaksOfYore_Misc");
            misc.SetFilePath(filePath);

            config.misc.skipCleaningItems = misc.CreateEntry<bool>("skipCleaningItems", false);
        }

#endif
        public static TweaksOfYore.Config.Cfg config = new TweaksOfYore.Config.Cfg();

    }
}
