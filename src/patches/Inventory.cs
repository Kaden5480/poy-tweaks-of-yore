using System.Reflection;

using HarmonyLib;
using UnityEngine;

using TimeAttackCategories = TimeAttackSetter.TimeAttackCategories;

namespace TweaksOfYore.Patches.Inv {
    /**
     * <summary>
     * Disables detaching ropes by looking down at the belt.
     * </summary>
     */
    [HarmonyPatch(typeof(RopeAnchor), "AllowDetachFromRope")]
    static class DisableBeltRopeDetach {
        static bool Prefix() {
            if (Plugin.config.speedrun.fullGame.Value == false
                && Plugin.config.speedrun.pocketwatch.Value == false
                && Plugin.config.inventory.disableBeltRopeDetach.Value == true
            ) {
                return false;
            }

            return true;
        }
    }

    /**
     * <summary>
     * Disables the clipboard from appearing when your time isn't a pb.
     * </summary>
     */
    [HarmonyPatch(typeof(TimeAttack), "BringUpScore")]
    [HarmonyPatch(MethodType.Enumerator)]
    static class DisableNonPbClipboard {
        private static Climbing climbing = null;
        private static TimeAttack timeAttack = null;

        private static TimeAttackCategories GetCategory() {
            StamperPeakSummit stamper = timeAttack.summitStamper;
            int category = 0;

            if (stamper.isCategory2) {
                category = 1;
            }
            else if (stamper.isCategory3) {
                category = 2;
            }
            else if (stamper.isCategory4) {
                category = 3;
            }
            else if (stamper.isAlps1) {
                category = 4;
            }
            else if (stamper.isAlps2) {
                category = 5;
            }
            else if (stamper.isAlps3) {
                category = 6;
            }

            return timeAttack.scoreSetter.timeAttackCategory[category];
        }

        private static bool IsPB() {
            TimeAttackCategories category = GetCategory();
            int peak = timeAttack.peakNumber;

            if (category.playerPrefTimes.Length < 1
                || category.playerPrefHolds.Length < 1
                || category.playerPrefRopes.Length < 1
            ) {
                return true;
            }

            if (timeAttack.timer < category.bestOverallTimeOnScore[peak]
                || category.bestOverallTimeOnScore[peak] == 0f
                || timeAttack.holdsMade < category.bestOverallHoldOnScore[peak]
                || timeAttack.ropesUsed < category.bestOverallRopesOnScore[peak]
                || category.bestOverallRopesOnScore[peak] != 0
            ) {
                return true;
            }

            return false;
        }

        public static void OnSceneLoaded() {
            climbing = GameObject.FindObjectOfType<Climbing>();
            timeAttack = GameObject.FindObjectOfType<TimeAttack>();
        }

        static bool Prefix() {
            if (Plugin.config.inventory.disableNonPbClipboard.Value == false
                || Plugin.config.speedrun.fullGame.Value == true
                || timeAttack == null
                || climbing == null
            ) {
                return true;
            }

            if (IsPB() == true) {
                return true;
            }

            climbing.summitingPeak = false;

            TimeAttack.receivingScore = false;
            TimeAttack.startedToReceiveScore = false;

            Helper.SetField<TimeAttack, bool>(timeAttack, "aboutToReceiveScoreRemoveRope", false);
            Helper.SetField<TimeAttack, bool>(timeAttack, "aboutToReceiveScore", false);
            Helper.SetField<TimeAttack, float>(timeAttack, "groundedAtSummitTimer", 0f);
            Helper.SetField<TimeAttack, bool>(timeAttack, "summited", false);

            timeAttack.keepTimeattackOpen = true;
            timeAttack.timeAttackActivated = false;
            timeAttack.watchActivated = false;
            timeAttack.watchReady = false;

            timeAttack.pocketwatchSound.volume = 0.12f;
            timeAttack.pocketwatchSound.clip = timeAttack.s_reachSummit;
            timeAttack.pocketwatchSound.Play();

            return false;
        }
    }
}
