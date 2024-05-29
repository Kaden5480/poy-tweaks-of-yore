using UnityEngine;


#if BEPINEX
using BepInEx;

namespace TweaksOfYore {
    [BepInPlugin("com.github.Kaden5480.poy-tweaks-of-yore", "Tweaks of Yore", PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin {
        /**
         * <summary>
         * Executes when the plugin is being loaded.
         * </summary>
         */
        public void Awake() {
            Harmony.CreateAndPatchAll(typeof(Patches.PatchSkipItemCleaning));
            Harmony.CreateAndPatchAll(typeof(Patches.SkipPeakShowText));
            Harmony.CreateAndPatchAll(typeof(Patches.SkipPeakAllowAlwaysCoroutine));
            Harmony.CreateAndPatchAll(typeof(Patches.SkipPeakAllowAlwaysJournal));
        }


#elif MELONLOADER
using MelonLoader;

[assembly: MelonInfo(typeof(TweaksOfYore.Plugin), "Tweaks of Yore", "0.1.0", "Kaden5480")]
[assembly: MelonGame("TraipseWare", "Peaks of Yore")]

namespace TweaksOfYore {
    public class Plugin: MelonMod {

#endif

    }
}
