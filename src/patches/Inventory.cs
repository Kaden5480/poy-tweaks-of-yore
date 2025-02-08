using System.Reflection;

using HarmonyLib;
using UnityEngine;

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
                && Plugin.config.inventory.disableBeltRopeDetach.Value == true
            ) {
                return false;
            }

            return true;
        }
    }
}
