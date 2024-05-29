using System.Collections.Generic;
using System.Reflection.Emit;

using HarmonyLib;

namespace TweaksOfYore {
    public class Helper {
        /**
         * <summary>
         * Compare two instructions for equivalence.
         * </summary>
         * <param name="a">The first instruction to compare</param>
         * <param name="b">The second instruction to compare</param>
         */
        public static bool InstsEqual(CodeInstruction a, CodeInstruction b) {
            // If either are null, always match
            if (a == null || b == null) {
                return true;
            }

            // Check opcodes
            if (a.opcode != b.opcode) {
                return false;
            }

            // Check null operands
            if (a.operand == null || b.operand == null) {
                return true;
            }

            // Check operand equivalence
            return a.operand.Equals(b.operand);
        }

        /**
         * <summary>
         * Find a conditional branch, and make it unconditional.
         * </summary>
         * <param name="instructions">The instructions to search in</param>
         * <param name="pattern">The pattern to search for</param>
         * <returns>The patched instructions</returns>
         */
        public static IEnumerable<CodeInstruction> BranchAlways(
            IEnumerable<CodeInstruction> instructions,
            CodeInstruction[] pattern
        ) {
            List<CodeInstruction> buffer = new List<CodeInstruction>();
            int patternIndex = 0;

            // If empty pattern, return normally
            if (pattern.Length < 1) {
                foreach (CodeInstruction instruction in instructions) {
                    yield return instruction;
                }

                yield break;
            }

            foreach (CodeInstruction instruction in instructions) {
                // If pattern matched, remove the condition and
                // make the branch unconditional
                if (patternIndex >= pattern.Length) {
                    CodeInstruction branch = buffer[patternIndex - 1];
                    branch.opcode = OpCodes.Br;

                    // Move all labels to the unconditional branch
                    foreach (CodeInstruction buffered in buffer) {
                        buffered.MoveLabelsTo(branch);
                    }

                    yield return branch;
                    yield return instruction;

                    buffer.Clear();
                    patternIndex = 0;

                    continue;
                }

                // If the pattern isn't fully matched, return
                // all buffered instructions normally
                if (InstsEqual(instruction, pattern[patternIndex]) == false) {
                    foreach (CodeInstruction buffered in buffer) {
                        yield return buffered;
                    }

                    yield return instruction;

                    buffer.Clear();
                    patternIndex = 0;

                    continue;
                }

                // Otherwise, store matching instructions
                buffer.Add(instruction);
                patternIndex++;
            }
        }

        /**
         * <summary>
         * Given a sequence of instructions, find a pattern and replace
         * it with the provided sequence.
         * </summary>
         * <param name="instructions">The instructions to search in</param>
         * <param name="pattern">The pattern to search for</param>
         * <param name="replacement">What to replace the pattern with</param>
         * <returns>The patched instructions</returns>
         */
        public static IEnumerable<CodeInstruction> Replace(
            IEnumerable<CodeInstruction> instructions,
            CodeInstruction[] pattern,
            CodeInstruction[] replacement
        ) {
            List<CodeInstruction> buffer = new List<CodeInstruction>();
            int patternIndex = 0;

            // If empty pattern, return normally
            if (pattern.Length < 1) {
                foreach (CodeInstruction instruction in instructions) {
                    yield return instruction;
                }

                yield break;
            }

            foreach (var instruction in instructions) {
                // If pattern matched, return the replacement
                if (patternIndex >= pattern.Length) {
                    // Move all labels to first instruction
                    foreach (CodeInstruction buffered in buffer) {
                        buffered.MoveLabelsTo(replacement[0]);
                    }

                    foreach (var replace in replacement) {
                        yield return replace;
                    }

                    yield return instruction;

                    patternIndex = 0;
                    buffer.Clear();

                    continue;
                }

                // If the pattern isn't fully matched, return
                // all buffered instructions normally
                if (InstsEqual(instruction, pattern[patternIndex]) == false) {
                    foreach (var buffered in buffer) {
                        yield return buffered;
                    }

                    yield return instruction;

                    patternIndex = 0;
                    buffer.Clear();

                    continue;
                }

                // Otherwise, store matching instructions
                buffer.Add(instruction);
                patternIndex++;
            }
        }
    }
}
