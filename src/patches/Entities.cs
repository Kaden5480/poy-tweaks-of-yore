using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TweaksOfYore.Patches.Entities {
    /**
     * <summary>
     * Disables the cabin goat object.
     * </summary>
     */
    [HarmonyPatch(typeof(CabinGoatReact), "StartEvent")]
    static class DisableCabinGoat {
        static void Prefix(CabinGoatReact __instance) {
            if (Plugin.config.entities.disableCabinGoat.Value == false) {
                return;
            }

            __instance.gameObject.SetActive(false);
        }
    }

    /**
     * <summary>
     * Disables the eagle game objects if they have been collected.
     * </summary>
     */
    [HarmonyPatch(typeof(Mermaid), "LoadMermaidStuff")]
    static class DisableEagles {
        static void Postfix(Mermaid __instance) {
            if (__instance.eagleParentObj == null) {
                return;
            }

            if (Plugin.config.entities.disableEagles.Value == false) {
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
    }

    /**
     * <summary>
     * Disables swans at the castle.
     * </summary>
     */
    [HarmonyPatch(typeof(BirdFlock), "Awake")]
    static class DisableSwans {
        static void Postfix(BirdFlock __instance) {
            if (Plugin.config.entities.disableSwans.Value == false) {
                return;
            }

            if (__instance.isSwan == false) {
                return;
            }

            __instance.gameObject.SetActive(false);
        }
    }

    /**
     * <summary>
     * Lower the volume for seagulls at Mara's Arch.
     * </summary>
     */
    [HarmonyPatch(typeof(Bird), "BirdGetHitSound")]
    static class LowerMarasArchSeagullVolume {
        static float defaultVolume = 0.85f;

        public static float volumeInject;

        static void Prefix() {
            if (Plugin.config.entities.lowerMarasArchSeagullVolume.Value == true
                && "Alps_3_SeaArch".Equals(SceneManager.GetActiveScene().name)
            ) {
                volumeInject = 0.3f;
            }
            else {
                volumeInject = defaultVolume;
            }
        }

        static IEnumerable<CodeInstruction> Transpiler(
            IEnumerable<CodeInstruction> insts
        ) {
            int times = 0;
            FieldInfo volumeInjectInfo = typeof(LowerMarasArchSeagullVolume).GetField(
                nameof(volumeInject),
                BindingFlags.Public | BindingFlags.Static
            );

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
