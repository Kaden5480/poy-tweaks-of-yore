using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;


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
            // == Config ==
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
            config.ui.displayAccurateRecords = Config.Bind(
                "UI", "displayAccurateRecords", false,
                "Whether to display accurate time records with the pocketwatch open"
            );

            // Misc
            config.misc.skipCleaningItems = Config.Bind(
                "Misc", "skipCleaningItems", false,
                "Whether to skip cleaning items after collecting them"
            );
            config.misc.disableSnowFallParticles = Config.Bind(
                "Misc", "disableSnowFallParticles", false,
                "Whether to disable snow fall particle effects"
            );
            config.misc.increaseFovRange = Config.Bind(
                "Misc", "increaseFovRange", false,
                "Whether to increase the configurable FOV range"
            );
            config.misc.reduceWelkinFog = Config.Bind(
                "Misc", "reduceWelkinFog", false,
                "Whether to reduce the fog on Welkin Pass"
            );
            config.misc.muteOnUnfocus = Config.Bind(
                "Misc", "muteOnUnfocus", false,
                "Whether to mute the game when it's no longer in focus"
            );

            // Speedrun
            config.speedrun.pocketwatch = Config.Bind(
                "Speedrun", "pocketwatch", false,
                "Only enable tweaks which are accepted in Pocketwatch% (and Pipe Only) runs"
            );
            config.speedrun.fullGame = Config.Bind(
                "Speedrun", "fullGame", false,
                "Only enable tweaks which are accepted in full game runs (Any%, 100%, All Peaks)"
            );

            // == Scene Loading ==
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;

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
            Harmony.CreateAndPatchAll(typeof(Patches.UI.DisplayAccurateRecords));

            // Misc
            Harmony.CreateAndPatchAll(typeof(Patches.Misc.SkipCleaningItems));
            Harmony.CreateAndPatchAll(typeof(Patches.Misc.IncreaseFovRange));
            Harmony.CreateAndPatchAll(typeof(Patches.Misc.ReduceWelkinFog));
        }

        /**
         * <summary>
         * Executes when this object is destroyed.
         * </summary>
         */
        public void OnDestroy() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        /**
         * <summary>
         * Executes when a scene is loaded.
         * </summary>
         * <param name="scene">The scene which loaded</param>
         * <param name="mode">The mode the scene loaded with</param>
         */
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            CommonSceneLoad(scene.buildIndex, scene.name);
        }

        /**
         * <summary>
         * Executes when a scene is unloaded.
         * </summary>
         * <param name="scene">The scene which unloaded</param>
         */
        private void OnSceneUnloaded(Scene scene) {
            CommonSceneUnload(scene.buildIndex, scene.name);
        }

        /**
         * <summary>
         * Executes every frame.
         * </summary>
         */
        public void Update() {
            CommonUpdate();
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
            config.ui.displayAccurateRecords = ui.CreateEntry<bool>("displayAccurateRecords", false);

            // Misc
            MelonPreferences_Category misc = MelonPreferences.CreateCategory("TweaksOfYore_Misc");
            misc.SetFilePath(filePath);

            config.misc.skipCleaningItems = misc.CreateEntry<bool>("skipCleaningItems", false);
            config.misc.disableSnowFallParticles = misc.CreateEntry<bool>("disableSnowFallParticles", false);
            config.misc.increaseFovRange = misc.CreateEntry<bool>("increaseFovRange", false);
            config.misc.reduceWelkinFog = misc.CreateEntry<bool>("reduceWelkinFog", false);
            config.misc.muteOnUnfocus = misc.CreateEntry<bool>("muteOnUnfocus", false);

            // Speedrun
            MelonPreferences_Category speedrun = MelonPreferences.CreateCategory("TweaksOfYore_Speedrun");
            speedrun.SetFilePath(filePath);

            config.speedrun.pocketwatch = speedrun.CreateEntry<bool>("pocketwatch", false);
            config.speedrun.fullGame = speedrun.CreateEntry<bool>("fullGame", false);
        }

        /**
         * <summary>
         * Executes when a scene is loaded.
         * </summary>
         * <param name="buildIndex">The build index of the scene which loaded</param>
         * <param name="sceneName">The name of the scene</param>
         */
        public override void OnSceneWasLoaded(int buildIndex, string sceneName) {
            CommonSceneLoad(buildIndex, sceneName);
        }

        /**
         * <summary>
         * Executes when a scene is unloaded.
         * </summary>
         * <param name="buildIndex">The build index of the scene which unloaded</param>
         * <param name="sceneName">The name of the scene</param>
         */
        public override void OnSceneWasUnloaded(int buildIndex, string sceneName) {
            CommonSceneUnload(buildIndex, sceneName);
        }

        /**
         * <summary>
         * Executes every frame.
         * </summary>
         */
        public override void OnUpdate() {
            CommonUpdate();
        }

#endif
        public static TweaksOfYore.Config.Cfg config = new TweaksOfYore.Config.Cfg();

        private void CommonSceneLoad(int buildIndex, string sceneName) {
            Patches.Misc.DisableSnowFallParticles.OnSceneLoaded();
            Patches.Misc.MuteOnUnfocus.OnSceneLoaded();
        }

        private void CommonSceneUnload(int buildIndex, string sceneName) {
            Patches.Misc.MuteOnUnfocus.OnSceneUnloaded();
        }

        private void CommonUpdate() {
            Patches.Misc.MuteOnUnfocus.Update();
        }
    }
}
