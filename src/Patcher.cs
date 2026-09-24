using System;

using HarmonyLib;

namespace TweaksOfYore {
    internal static class Patcher {
        private static Logger logger = new Logger(typeof(Patcher));

        /**
         * <summary>
         * Applies a patch and logs a completion message.
         * </summary>
         * <param name="patch">The patch to apply</param>
         */
        internal static void Patch(Type patch) {
            Harmony.CreateAndPatchAll(patch);
            logger.LogDebug($"Applied patch: {patch}");
        }

        /**
         * <summary>
         * Applies all patches.
         * </summary>
         */
        internal static void Awake() {
            Patch(typeof(Patches.Entities));
            Patch(typeof(Patches.Inventory));
            Patch(typeof(Patches.Misc));
            Patch(typeof(Patches.UI));
        }

        /**
         * <summary>
         * Runs patches on each frame.
         * </summary>
         */
        internal static void Update() {
            Patches.Misc.MuteOnUnfocus.Update();
        }

        /**
         * <summary>
         * Runs patches on scene loads.
         * </summary>
         */
        internal static void SceneLoad() {
            Patches.Misc.DisableSnowFallParticles();
            Patches.Misc.MuteOnUnfocus.SceneLoad();
        }

        /**
         * <summary>
         * Runs patches on scene unloads.
         * </summary>
         */
        internal static void SceneUnload() {
            Patches.Misc.MuteOnUnfocus.SceneUnload();
        }
    }
}
