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

            // Inventory
            config.inventory.disableBeltRopeDetach = Config.Bind(
                "Inventory", "disableBeltRopeDetach", false,
                "Whether to disable detaching ropes by looking at your belt and pressing the interact bind"
            );

            // == Patching ==
            // Entities
            Harmony.CreateAndPatchAll(typeof(Patches.Entities.DisableCabinGoat));
            Harmony.CreateAndPatchAll(typeof(Patches.Entities.DisableEagles));
            Harmony.CreateAndPatchAll(typeof(Patches.Entities.DisableSwans));

            // Inventory
            Harmony.CreateAndPatchAll(typeof(Patches.Inv.DisableBeltRopeDetach));
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

            // Inventory
            MelonPreferences_Category inventory = MelonPreferences.CreateCategory("TweaksOfYore_Inventory");
            inventory.SetFilePath(filePath);

            config.inventory.disableBeltRopeDetach = inventory.CreateEntry<bool>("disableBeltRopeDetach", false);
        }

#endif
        public static TweaksOfYore.Config.Cfg config = new TweaksOfYore.Config.Cfg();

    }
}
