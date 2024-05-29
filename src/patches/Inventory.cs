using HarmonyLib;

namespace TweaksOfYore.Patches {
    /**
     * <summary>
     * Disables using the pocket watch without using the keybind.
     * </summary>
     */
    [HarmonyPatch(typeof(Inventory), "FetchPocketWatch")]
    static class DisableInteractPocketWatch {
        static bool Prefix(bool usedPocketWatchFromBelt) {
            if (usedPocketWatchFromBelt == true) {
                return false;
            }

            return true;
        }
    }

    /**
     * <summary>
     * Disables detaching from rope without using the keybind.
     * </summary>
     */
    [HarmonyPatch(typeof(RopeAnchor), "AllowDetachFromRope")]
    static class DisableInteractRope {
        static bool Prefix() {
            return false;
        }
    }
}
