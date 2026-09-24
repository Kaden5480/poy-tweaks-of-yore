using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TweaksOfYore.Patches {
    internal static class Entities {
        /**
         * <summary>
         * Disables the cabin goat object.
         * </summary>
         */
        [HarmonyPrefix]
        [HarmonyPatch(typeof(CabinGoatReact), "StartEvent")]
        private static void DisableCabinGoat(CabinGoatReact __instance) {
            if (Config.disableCabinGoat.Value == false) {
                return;
            }

            __instance.gameObject.SetActive(false);
        }

        /**
         * <summary>
         * Disables the eagle game objects if they have been collected.
         * </summary>
         */
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Mermaid), "LoadMermaidStuff")]
        private static void DisableEagles(Mermaid __instance) {
            if (__instance.eagleParentObj == null) {
                return;
            }

            if (Config.disableEagles.Value == false) {
                return;
            }

            for (int i = 1; i <= 5; i++) {
                if ($"Eagle_{i}".Equals(__instance.eagleParentObj.name) == false) {
                    continue;
                }

                if (PlayerPrefs.HasKey($"Eagle{i}") == false) {
                    continue;
                }

                __instance.eagleParentObj.SetActive(false);
            }
        }

        /**
         * <summary>
         * Disables swans at the castle.
         * </summary>
         */
        [HarmonyPostfix]
        [HarmonyPatch(typeof(BirdFlock), "Awake")]
        private static void DisableSwans(BirdFlock __instance) {
            if (Config.disableSwans.Value == false) {
                return;
            }

            if (__instance.isSwan == false) {
                return;
            }

            __instance.gameObject.SetActive(false);
        }

        /**
         * <summary>
         * Lower the volume for seagulls at Mara's Arch.
         * </summary>
         */
        private static float defaultVolume = 0.85f;
        private static float volumeInject = 0f;

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Bird), "BirdGetHitSound")]
        private static void MarasArchPrefix() {
            if (Config.lowerMarasArchSeagullVolume.Value == true
                && "Alps_3_SeaArch".Equals(SceneManager.GetActiveScene().name)
            ) {
                volumeInject = 0.3f;
            }
            else {
                volumeInject = defaultVolume;
            }
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Bird), "BirdGetHitSound")]
        static IEnumerable<CodeInstruction> Transpiler(
            IEnumerable<CodeInstruction> insts
        ) {
            FieldInfo volumeInjectInfo = AccessTools.Field(
                typeof(Entities),
                nameof(volumeInject)
            );

            int times = 0;

            foreach (CodeInstruction inst in insts) {
                if (inst.LoadsConstant(defaultVolume) == true) {
                    if (times == 1) {
                        inst.opcode = OpCodes.Ldsfld;
                        inst.operand = volumeInjectInfo;
                    }

                    times++;
                }

                yield return inst;
            }
        }
    }
}
