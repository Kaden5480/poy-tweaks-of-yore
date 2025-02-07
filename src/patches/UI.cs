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
            if (Plugin.config.ui.disableCruxNotifications.Value == true) {
                return false;
            }

            return true;
        }
    }
}
