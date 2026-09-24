using System.Reflection;

using HarmonyLib;
using UnityEngine;

namespace TweaksOfYore.Patches {
    internal static class Inventory {
        /**
         * <summary>
         * Disables detaching ropes by looking down at the belt.
         * </summary>
         */
        [HarmonyPrefix]
        [HarmonyPatch(typeof(RopeAnchor), "AllowDetachFromRope")]
        static bool DisableBeltRopeDetach() {
            if (Config.fullGame.Value == false
                && Config.pocketwatch.Value == false
                && Config.disableBeltRopeDetach.Value == true
            ) {
                return false;
            }

            return true;
        }
    }
}
