using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

using HarmonyLib;

namespace TweaksOfYore.Patches {
    /**
     * <summary>
     * Patches items so you no longer have to clean them.
     * </summary>
     */
    [HarmonyPatch(typeof(ArtefactOnPeak), "SaveGrabbedItem")]
    static class SkipItemCleaning {
        static IEnumerable<CodeInstruction> Transpiler(
            IEnumerable<CodeInstruction> insts
        ) {
            CodeInstruction buf = null;

            foreach (CodeInstruction inst in insts) {
                // If an instruction is buffered, check for a store in
                // an *_IsDirty field
                if (buf != null) {
                    if (inst.opcode == OpCodes.Stfld
                            && ((FieldInfo) inst.operand).EndsWith("IsDirty")) {
                        // Is an *_IsDirty field, so store false instead
                        buf.opcode = OpCodes.Ldc_I4_0;
                    }

                    yield return buf;
                    buf = null;
                    yield return inst;

                    continue;
                }

                // If storing true, buffer the instruction
                if (inst.opcode == OpCodes.Ldc_I4_1) {
                    buf = inst;
                    continue;
                }

                // Otherwise, return the instruction normally
                yield return inst;
            }
        }
    }
}
