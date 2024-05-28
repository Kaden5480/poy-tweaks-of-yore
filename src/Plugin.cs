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
            Harmony.CreateAndPatchAll(typeof(PatchSkipItemCleaning));
            Harmony.CreateAndPatchAll(typeof(PatchRopeDetach));
        }


#elif MELONLOADER
using MelonLoader;

[assembly: MelonInfo(typeof(TweaksOfYore.Plugin), "Tweaks of Yore", "0.1.0", "Kaden5480")]
[assembly: MelonGame("TraipseWare", "Peaks of Yore")]

namespace TweaksOfYore {
    public class Plugin: MelonMod {

#endif

        /**
         * <summary>
         * Patches items, so you no longer have to clean them.
         * </summary>
         */
        [HarmonyPatch(typeof(ArtefactOnPeak), "SaveGrabbedItem")]
        static class PatchSkipItemCleaning {
            static IEnumerable<CodeInstruction> Transpiler(
                IEnumerable<CodeInstruction> insts
            ) {
            }
        }

        /**
         * <summary>
         * Patches rope detaching, so you no longer detach
         * from looking at a rope.
         * </summary>
         */
        [HarmonyPatch(typeof(), "")]
        static class PatchRopeDetach {
            static bool Prefix() {
                return true;
            }
        }
    }
}
