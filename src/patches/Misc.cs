using System.Reflection;

using HarmonyLib;

namespace TweaksOfYore.Patches.Misc {
    /**
     * <summary>
     * Skips cleaning items after they have been collected.
     * </summary>
     */
    [HarmonyPatch(typeof(ArtefactOnPeak), "SaveGrabbedItem")]
    static class SkipCleaningItems {
        static void Postfix() {
            if (Plugin.config.speedrun.fullGame.Value == true
                || Plugin.config.misc.skipCleaningItems.Value == false
            ) {
                return;
            }

            foreach (FieldInfo info in AccessTools.GetDeclaredFields(typeof(GameManager))) {
                string name = info.Name;

                if (name.StartsWith("alps_statue_") == false
                    && name.StartsWith("artefact_") == false
                ) {
                    continue;
                }

                if (name.EndsWith("IsDirty") == false
                    && name.EndsWith("IsDirty_pt1") == false
                    && name.EndsWith("IsDirty_pt2") == false
                ) {
                    continue;
                }

                info.SetValue(GameManager.control, false);
            }

            GameManager.control.Save();
        }
    }
}
