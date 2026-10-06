using System;

using HarmonyLib;
using UnityEngine;

using TimeAttackCategories = TimeAttackSetter.TimeAttackCategories;

namespace TweaksOfYore.Patches {
    internal static class UI {
        /**
         * <summary>
         * Always display the pocketwatch tutorials.
         * </summary>
         */
        [HarmonyPostfix]
        [HarmonyPatch(typeof(TimeAttack), "Update")]
        private static void AlwaysDisplayTATutorial(TimeAttack __instance) {
            // It doesn't seem easy to tell if the normal tutorial coroutines
            // are running, so not resetting the canvas groups back to
            // an expected state when this patch is disabled may
            // cause them to stay visible (provided this patch
            // was disabled while the pocketwatch was open)
            // I'm fine with leaving this issue in for now, since
            // it's not game breaking and should be resolved by
            // re-enabling this patch or restarting the level
            if (Config.alwaysDisplayTATutorial.Value == false) {
                return;
            }

            CanvasGroup recordDisplayTut = __instance.recordDisplayTut;
            CanvasGroup scoreboardDisplayTut = __instance.scoreboardDisplayTut;

            float target = (__instance.isOpenNow) ? 1f : 0f;

            recordDisplayTut.alpha = Mathf.MoveTowards(
                recordDisplayTut.alpha, target, 3f * Time.deltaTime
            );

            scoreboardDisplayTut.alpha = Mathf.MoveTowards(
                scoreboardDisplayTut.alpha, target, 3f * Time.deltaTime
            );
        }

        /**
         * <summary>
         * Disables the crux notifications.
         * </summary>
         */
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Crux), "EnterCrux")]
        [HarmonyPatch(MethodType.Enumerator)]
        private static bool DisableCruxNotifications() {
            if (Config.fullGame.Value == false
                && Config.disableCruxNotifications.Value == true
            ) {
                return false;
            }

            return true;
        }

        /**
         * <summary>
         * Disables subtitles
         * </summary>
         */
        [HarmonyPostfix]
        [HarmonyPatch(typeof(NPC_Climber), "LateUpdate")]
        private static void DisableSubtitlesNPCClimber(NPC_Climber __instance) {
            if (Config.fullGame.Value == true
                || Config.disableSubtitles.Value == false
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

        /**
         * <summary>
         * Disables subtitles
         * </summary>
         */
        [HarmonyPostfix]
        [HarmonyPatch(typeof(NPCSystem), "Update")]
        private static void DisableSubtitlesNPCSystem(NPCSystem __instance) {
            if (Config.fullGame.Value == true
                || Config.disableSubtitles.Value == false
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

        /**
         * <summary>
         * Allows displaying more accurate time records
         * with the pocketwatch open.
         * </summary>
         */
        static class AccurateRecords {
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

            public static void UpdateRecord(TimeAttack timeAttack) {
                if (timeAttack == null || timeAttack.recordTimeText == null) {
                    return;
                }

                if (Config.displayAccurateRecords.Value == false) {
                    return;
                }

                TimeAttackCategories category = GetCategory(timeAttack);
                float time = category.playerPrefTimes[timeAttack.peakNumber];

                TimeSpan span = TimeSpan.FromSeconds(time);
                float other = span.Seconds + (time - ((int) time));

                string timeString = $"{span.Minutes:00}:{other:00.0000000}";


                if (IsBigPeak(timeAttack) == true) {
                    timeString = $"{span.Hours:00}:{timeString}";
                }

                timeAttack.recordTimeText.text = timeString;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TimeAttack), "CheckRecords")]
        private static void DisplayAccurateRecordsCheck(TimeAttack __instance) {
            AccurateRecords.UpdateRecord(__instance);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TimeAttack), "BringUpScore")]
        [HarmonyPatch(MethodType.Enumerator)]
        private static void DisplayAccurateRecordsScore() {
            AccurateRecords.UpdateRecord(
                GameObject.FindObjectOfType<TimeAttack>()
            );
        }

        /**
         * <summary>
         * Displays explicit keybinds instead of vague ones
         * in the time attack tutorials.
         * </summary>
         */
        [HarmonyPostfix]
        [HarmonyPatch(typeof(TimeAttack), "Start")]
        private static void DisplayExplicitTAKeybinds() {
            if (Config.displayExplicitTAKeybinds.Value == false) {
                return;
            }

            string leftArm = Util.GetKeybindByName("Arm Left");
            string rightArm = Util.GetKeybindByName("Arm Right");
            string interact = Util.GetKeybindByName("Interact");

            if (Cache.pocketwatchTutText != null) {
                Cache.pocketwatchTutText.text
                    = $"Press \"{interact}\" to toggle Time Attack on or off.";
            }

            if (Cache.recordTutText != null) {
                Cache.recordTutText.text
                    = $"When Pocket Watch is open, press \"{rightArm}\""
                    + " to toggle the record display next to the timer.";
            }

            if (Cache.scoreboardTutText != null) {
                Cache.scoreboardTutText.text
                    = $"While Pocket Watch is open, press \"{leftArm}\""
                    + " to toggle only opening the scoreboard if a record"
                    + " is set at summit.";
            }

            // The size of the scoreboard tutorial should also
            // be copied to the record tutorial, for consistent sizing.
            if (Cache.scoreboardTutBg != null && Cache.recordTutBg != null) {
                Cache.recordTutBg.sizeDelta
                    = Cache.scoreboardTutBg.sizeDelta;

                Cache.recordTutBg.anchoredPosition
                    = Cache.scoreboardTutBg.anchoredPosition;
            }
        }
    }
}
