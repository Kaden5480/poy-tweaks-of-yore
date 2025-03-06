using System;

using HarmonyLib;
using TimeAttackCategories = TimeAttackSetter.TimeAttackCategories;

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

    /**
     * <summary>
     * Allows displaying more accurate time records
     * with the pocketwatch open.
     * </summary>
     */
    [HarmonyPatch(typeof(TimeAttack), "CheckRecords")]
    static class DisplayAccurateRecords {
        private static TimeAttackCategories GetCategory(TimeAttack timeAttack) {
            StamperPeakSummit stamper = timeAttack.summitStamper;
            TimeAttackSetter setter = timeAttack.scoreSetter;

            int index = 0;

            if (stamper.isCategory2) {
                index = 1;
            }
            else if (stamper.isCategory3) {
                index = 2;
            }
            else if (stamper.isCategory4) {
                index = 3;
            }
            else if (stamper.isAlps1) {
                index = 4;
            }
            else if (stamper.isAlps2) {
                index = 5;
            }
            else if (stamper.isAlps3) {
                index = 6;
            }

            return setter.timeAttackCategory[index];
        }

        private static bool IsBigPeak(TimeAttack timeAttack) {
            if (timeAttack.summitStamper.isCategory4) {
                return true;
            }

            return timeAttack.summitStamper.isAlps3 && !timeAttack.isAlps3ShortMap;
        }

        static void Postfix(TimeAttack __instance) {
            TimeAttackCategories category = GetCategory(__instance);
            float time = category.playerPrefTimes[__instance.peakNumber];

            TimeSpan span = TimeSpan.FromSeconds(time);
            float other = span.Seconds + (time - ((int) time));

            string timeString = $"{span.Minutes:00}:{other:00.0000000}";


            if (IsBigPeak(__instance) == true) {
                timeString = $"{span.Hours:00}:{timeString}";
            }

            __instance.recordTimeText.text = timeString;
        }
    }
}
