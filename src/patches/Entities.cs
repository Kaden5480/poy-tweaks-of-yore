using HarmonyLib;
using UnityEngine;

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
            if (Plugin.config.entities.disableEagles.Value == false) {
                return;
            }

            for (int i = 1; i <= 5; i++) {
                if ($"Eagle_{i}".Equals(__instance.gameObject.name) == false) {
                    continue;
                }

                if (PlayerPrefs.HasKey($"Eagle{i}") == false) {
                    continue;
                }

                __instance.gameObject.SetActive(false);
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
}
