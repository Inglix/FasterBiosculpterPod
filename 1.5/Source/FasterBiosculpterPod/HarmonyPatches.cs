using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using Verse;

namespace FasterBiosculpterPod
{
    internal class HarmonyPatches
    {
        [HarmonyPatch]
        class NutritionRequiredTranspiler
        {
            [HarmonyTargetMethods]
            static IEnumerable<MethodBase> FindMethods()
            {
                yield return AccessTools.Method(typeof(CompBiosculpterPod), "get_RequiredNutritionRemaining");
                yield return AccessTools.Method(typeof(CompBiosculpterPod), "CompInspectStringExtra");
                yield return AccessTools.Method(typeof(CompBiosculpterPod), "<CompGetGizmosExtra>b__91_9");
                yield return AccessTools.Method(typeof(CompBiosculpterPod).GetNestedType("<CompGetGizmosExtra>d__91", BindingFlags.NonPublic), "MoveNext"); // Disables the dev gizmo "fill nutrition and cycle requirements" when configured amount of nutrition is loaded
                yield return AccessTools.Method(typeof(CompBiosculpterPod), "LiquifyNutrition");
            }

            [HarmonyTranspiler]
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                List<CodeInstruction> instructionList = instructions.ToList();
                for (var i = 0; i < instructionList.Count; i++)
                {
                    if (instructionList[i].opcode == OpCodes.Ldc_R4 && (Single)instructionList[i].operand == Constants.VanillaNutritionRequired)
                    {
                        instructionList[i].operand = LoadedModManager.GetMod<FasterBiosculpterPod>().GetSettings<Settings>().NutritionRequired;
                        break;
                    }
                }

                return instructionList.AsEnumerable();
            }
        }

        [HarmonyPatch(typeof(CompBiosculpterPod), nameof(CompBiosculpterPod.SetBiotuned))]
        class SetBiotunedTranspiler
        {
            [HarmonyTranspiler]
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                List<CodeInstruction> instructionList = instructions.ToList();
                for (var i = 0; i < instructionList.Count; i++)
                {
                    if (instructionList[i].opcode == OpCodes.Ldc_I4 && (Int32)instructionList[i].operand == Constants.VanillaBiotuningDurationTicks)
                    {
                        instructionList[i].operand = SettingsUtils.ConvertDaysToTicks(LoadedModManager.GetMod<FasterBiosculpterPod>().GetSettings<Settings>().BiotuningDurationDays);
                        break;
                    }
                }

                return instructionList.AsEnumerable();
            }
        }

        [HarmonyPatch(typeof(CompBiosculpterPod), nameof(CompBiosculpterPod.CompTick))]
        class CompTickTranspiler
        {
            [HarmonyTranspiler]
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                List<CodeInstruction> instructionList = instructions.ToList();
                for (var i = 0; i < instructionList.Count; i++)
                {
                    if (instructionList[i].opcode == OpCodes.Ldc_I4 && (Int32)instructionList[i].operand == Constants.VanillaBiotuningDurationTicks)
                    {
                        instructionList[i].operand = SettingsUtils.ConvertDaysToTicks(LoadedModManager.GetMod<FasterBiosculpterPod>().GetSettings<Settings>().BiotuningDurationDays);
                        break;
                    }
                }

                return instructionList.AsEnumerable();
            }
        }

        [HarmonyPatch(typeof(CompBiosculpterPod_AgeReversalCycle), nameof(CompBiosculpterPod_AgeReversalCycle.CycleCompleted))]
        public class CycleCompletedTranspiler
        {
            [HarmonyTranspiler]
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                List<CodeInstruction> instructionList = instructions.ToList();
                for (var i = 0; i < instructionList.Count; i++)
                {
                    if (instructionList[i].opcode == OpCodes.Ldc_R4 && (Single)instructionList[i].operand == (Single)Constants.VanillaAgeReversalTicks)
                    {
                        instructionList[i].operand = (Single)SettingsUtils.ConvertDaysToTicks(LoadedModManager.GetMod<FasterBiosculpterPod>().GetSettings<Settings>().AgeReversalDays);
                        break;
                    }
                }

                return instructionList.AsEnumerable();
            }
        }

        [HarmonyPatch(typeof(CompBiosculpterPod_AgeReversalCycle), nameof(CompBiosculpterPod_AgeReversalCycle.Description))]
        class DescriptionTranspiler
        {
            [HarmonyTranspiler]
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                List<CodeInstruction> instructionList = instructions.ToList();
                for (var i = 0; i < instructionList.Count; i++)
                {
                    if (instructionList[i].opcode == OpCodes.Ldc_I4 && (Int32)instructionList[i].operand == Constants.VanillaAgeReversalTicks)
                    {
                        instructionList[i].operand = SettingsUtils.ConvertDaysToTicks(LoadedModManager.GetMod<FasterBiosculpterPod>().GetSettings<Settings>().AgeReversalDays);
                        break;
                    }
                }

                return instructionList.AsEnumerable();
            }
        }

        [HarmonyPatch(typeof(Pawn_AgeTracker), nameof(Pawn_AgeTracker.ResetAgeReversalDemand))]
        class ResetAgeReversalDemandTranspiler
        {
            [HarmonyTranspiler]
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                List<CodeInstruction> instructionList = instructions.ToList();
                for (var i = 0; i < instructionList.Count; i++)
                {
                    if (instructionList[i].opcode == OpCodes.Ldc_I4_S && (SByte)instructionList[i].operand == Constants.VanillaAgeReversalDays)
                    {
                        instructionList[i].opcode = OpCodes.Ldc_I4;
                        instructionList[i].operand = (int)LoadedModManager.GetMod<FasterBiosculpterPod>().GetSettings<Settings>().AgeReversalDays;
                        break;
                    }
                }

                return instructionList.AsEnumerable();
            }
        }
    }
}