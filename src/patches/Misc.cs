using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

using HarmonyLib;
using UnityEngine;

namespace TweaksOfYore.Patches {
    internal static class Misc {
        /**
         * <summary>
         * Skips cleaning items after they have been collected.
         * </summary>
         */
        [HarmonyPostfix]
        [HarmonyPatch(typeof(ArtefactOnPeak), "SaveGrabbedItem")]
        private static void SkipCleaningItems() {
            if (Config.fullGame.Value == true
                || Config.skipCleaningItems.Value == false
            ) {
                return;
            }

            foreach (FieldInfo info in AccessTools.GetDeclaredFields(typeof(GameManager))) {
                string name = info.Name;

                if (name.StartsWith("alps_statue_") == false
                    && name.StartsWith("artefact_") == false
                    ) {
                    continue;
                }

                if (name.EndsWith("IsDirty") == false
                    && name.EndsWith("IsDirty_pt1") == false
                    && name.EndsWith("IsDirty_pt2") == false
                ) {
                    continue;
                }

                info.SetValue(GameManager.control, false);
            }

            GameManager.control.Save();
        }

        /**
         * <summary>
         * Disables snow fall particle effects.
         * </summary>
         */
        internal static void DisableSnowFallParticles() {
            if (Config.fullGame.Value == true
                || Config.disableSnowFallParticles.Value == false
            ) {
                return;
            }

            foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>()) {
                if (obj.name.StartsWith("SnowHeapFallSmokeParticle") == false) {
                    continue;
                }

                if (obj.GetComponent<RandomParticlePlay>() == false) {
                    continue;
                }

                obj.SetActive(false);
            }
        }

        /**
         * <summary>
         * Increases the range of configurable FOVs.
         * </summary>
         */
        [HarmonyPostfix]
        [HarmonyPatch(typeof(GraphicsOptions), "Awake")]
        private static void IncreaseFOVRange(GraphicsOptions __instance) {
            float defaultMin = __instance.fovSlider.minValue;
            float defaultMax = __instance.fovSlider.maxValue;

            if (Config.fullGame.Value == true
                || Config.pocketwatch.Value == true
                || Config.increaseFovRange.Value == false
            ) {
                __instance.fovSlider.value = Mathf.Min(
                    defaultMax, Mathf.Max(
                    __instance.fovSlider.value, defaultMin
                ));
                return;
            }

            __instance.fovSlider.minValue = 0f;
            __instance.fovSlider.maxValue = 180f;
        }

        /**
         * <summary>
         * Reduces the fog effect on Welkin Pass.
         * </summary>
         */
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PPFogDistance), "Start")]
        static void ReduceWelkinFog(PPFogDistance __instance) {
            if (Config.fullGame.Value == true
                || Config.pocketwatch.Value == true
                || Config.reduceWelkinFog.Value == false
            ) {
                return;
            }

            // Only run on welkin pass
            if ("Alps2_5_WelkinPass".Equals(Cache.scene.name) == false) {
                return;
            }

            AccessTools.Field(typeof(PPFogDistance), "globalDensity_original")
                .SetValue(__instance, 0.5f);
        }

        /**
         * <summary>
         * Mutes when the game is no longer focused.
         * </summary>
         */
        internal static class MuteOnUnfocus {
            private static float oldLevel = -80f;
            private static bool isMuted = false;

            private static void Mute() {
                if (isMuted == true) {
                    return;
                }

                Cache.mixer.GetFloat("MasterVolume", out oldLevel);
                Cache.mixer.SetFloat("MasterVolume", -80f);
                isMuted = true;
            }

            private static void Unmute() {
                if (isMuted == false) {
                    return;
                }

                Cache.mixer.SetFloat("MasterVolume", oldLevel);
                isMuted = false;
            }

            internal static void Update() {
                if (Config.muteOnUnfocus.Value == false
                    || Cache.mixer == null
                ) {
                    return;
                }

                if (Application.isFocused == false) {
                    Mute();
                }
                else {
                    Unmute();
                }
            }
        }

        /**
         * <summary>
         * Disables random exhales.
         * </summary>
         */
        [HarmonyPrefix]
        [HarmonyPatch(typeof(RandomExhalePebble), "RandomExhale")]
        [HarmonyPatch(typeof(RandomExhalePebble), "RandomExhaleFromPitchClimbing")]
        private static bool DisableExhale() {
            if (Config.disableExhale.Value == false
                || Config.fullGame.Value == true
            ) {
                return true;
            }

            return false;
        }

        /**
         * <summary>
         * Extends the distance which you can reach the "return to cabin" bag from.
         * </summary>
         */
        private static float leavePeakSceneDefault = 1.35f;
        private static float leavePeakSceneInject = 0f;

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(LeavePeakScene), "Update")]
        private static IEnumerable<CodeInstruction> ExtendLeavePeakScene(
            IEnumerable<CodeInstruction> insts
        ) {
            FieldInfo leavePeakSceneInjectInfo = AccessTools.Field(
                typeof(Misc), nameof(leavePeakSceneInject)
            );

            IEnumerable<CodeInstruction> result = Helper.Replace(insts,
                new[] {
                    new CodeInstruction(OpCodes.Ldc_R4, leavePeakSceneDefault),
                    new CodeInstruction(OpCodes.Ldarg_0),
                },
                new[] {
                    new CodeInstruction(OpCodes.Ldsfld, leavePeakSceneInjectInfo),
                    new CodeInstruction(OpCodes.Ldarg_0),
                }
            );

            foreach (CodeInstruction inst in result) {
                Plugin.LogDebug(Helper.InstToString(inst));
                yield return inst;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(LeavePeakScene), "Update")]
        private static void ExtendLeavePeakUpdate() {
            if (Config.fullGame.Value == true
                || Config.extendReturnToCabinBag.Value == false
            ) {
                leavePeakSceneInject = leavePeakSceneDefault;
                return;
            }

            leavePeakSceneInject = 3f;
        }
    }
}
