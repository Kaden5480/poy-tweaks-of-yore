using HarmonyLib;

namespace TweaksOfYore.Patches.UI {
    /**
     * <summary>
     * Disables the crux notifications.
     * </summary>
     */
    [HarmonyPatch(typeof(Crux), "EnterCrux")]
    [HarmonyPatch(MethodType.Enumerator)]
    static class DisableCruxNotifications {
        static bool Prefix() {
            if (Plugin.config.speedrun.fullGame.Value == false
                && Plugin.config.ui.disableCruxNotifications.Value == true
            ) {
                return false;
            }

            return true;
        }
    }

    /**
     * <summary>
     * Disables subtitles
     * </summary>
     */
    [HarmonyPatch(typeof(NPC_Climber), "LateUpdate")]
    static class DisableSubtitlesNPCClimber {
        static void Postfix(NPC_Climber __instance) {
            if (Plugin.config.speedrun.fullGame.Value == true
                || Plugin.config.ui.disableSubtitles.Value == false
            ) {
                return;
            }

            if (__instance.dialogueBackground == null) {
                return;
            }

            if ("INTERACT TO ACCEPT".Equals(__instance.dialogueText.text)) {
                __instance.dialogueBackground.gameObject.SetActive(true);
            }
            else {
                __instance.dialogueBackground.gameObject.SetActive(false);
            }
        }
    }

    /**
     * <summary>
     * Disables subtitles
     * </summary>
     */
    [HarmonyPatch(typeof(NPCSystem), "Update")]
    static class DisableSubtitlesNPCSystem {
        static void Postfix(NPCSystem __instance) {
            if (Plugin.config.speedrun.fullGame.Value == true
                || Plugin.config.ui.disableSubtitles.Value == false
            ) {
                return;
            }

            if (__instance.dialogueBackground == null) {
                return;
            }


            if ("TALK".Equals(__instance.dialogueText.text)) {
                __instance.dialogueBackground.gameObject.SetActive(true);
            }
            else {
                __instance.dialogueBackground.gameObject.SetActive(false);
            }
        }
    }
}
