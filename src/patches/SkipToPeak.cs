using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

using HarmonyLib;

namespace TweaksOfYore.Patches {
    /**
     * <summary>
     * Patches the UI to always show the skip to next peak button.
     * </summary>
     */
    [HarmonyPatch(typeof(StamperPeakSummit), "SkipCoroutineMethod")]
    [HarmonyPatch(MethodType.Enumerator)]
    static class SkipPeakShowText {
        static IEnumerable<CodeInstruction> Transpiler(
            IEnumerable<CodeInstruction> insts
        ) {
            FieldInfo control = AccessTools.Field(
                typeof(GameManager), nameof(GameManager.control)
            );

            FieldInfo permaDeathEnabled = AccessTools.Field(
                typeof(GameManager), nameof(GameManager.permaDeathEnabled)
            );

            // Always branch to yfyd logic
            IEnumerable<CodeInstruction> replaced = Helper.BranchAlways(insts,
                new[] {
                    new CodeInstruction(OpCodes.Ldsfld, control),
                    new CodeInstruction(OpCodes.Ldfld, permaDeathEnabled),
                    new CodeInstruction(OpCodes.Brtrue, null),
                }
            );

            // Return patched instructions
            foreach (CodeInstruction replace in replaced) {
                yield return replace;
            }
        }
    }

    /**
     * <summary>
     * Always allow choosing the next peak (if it's not the end of a category).
     * </summary>
     */
    [HarmonyPatch(typeof(StamperPeakSummit), "SkipCoroutineMethod")]
    [HarmonyPatch(MethodType.Enumerator)]
    static class SkipPeakAllowAlwaysCoroutine {
        static IEnumerable<CodeInstruction> Transpiler(
            IEnumerable<CodeInstruction> insts
        ) {
            FieldInfo control = AccessTools.Field(
                typeof(GameManager), nameof(GameManager.control)
            );

            FieldInfo permaDeathEnabled = AccessTools.Field(
                typeof(GameManager), nameof(GameManager.permaDeathEnabled)
            );

            // Remove the branch to always allow skipping
            IEnumerable<CodeInstruction> replaced = Helper.Replace(insts,
                new[] {
                    new CodeInstruction(OpCodes.Ldsfld, control),
                    new CodeInstruction(OpCodes.Ldfld, permaDeathEnabled),
                    new CodeInstruction(OpCodes.Brfalse, null),
                    new CodeInstruction(OpCodes.Ldloc_1),
                },
                new[] {
                    new CodeInstruction(OpCodes.Ldloc_1),
                }
            );

            // Return patched instructions
            foreach (CodeInstruction replace in replaced) {
                yield return replace;
            }
        }
    }

    /**
     * <summary>
     * Always allow choosing the next peak (if it's not the end of a category).
     * </summary>
     */
    [HarmonyPatch(typeof(StamperPeakSummit), "StampJournal")]
    [HarmonyPatch(MethodType.Enumerator)]
    static class SkipPeakAllowAlwaysJournal {
        static IEnumerable<CodeInstruction> Transpiler(
            IEnumerable<CodeInstruction> insts
        ) {
            FieldInfo control = AccessTools.Field(
                typeof(GameManager), nameof(GameManager.control)
            );

            FieldInfo permaDeathEnabled = AccessTools.Field(
                typeof(GameManager), nameof(GameManager.permaDeathEnabled)
            );

            // Always branch to yfyd logic
            IEnumerable<CodeInstruction> replaced = Helper.BranchAlways(insts,
                new[] {
                    new CodeInstruction(OpCodes.Ldsfld, control),
                    new CodeInstruction(OpCodes.Ldfld, permaDeathEnabled),
                    new CodeInstruction(OpCodes.Brtrue, null),
                }
            );

            // Return patched instructions
            foreach (CodeInstruction replace in replaced) {
                yield return replace;
            }
        }
    }
}
