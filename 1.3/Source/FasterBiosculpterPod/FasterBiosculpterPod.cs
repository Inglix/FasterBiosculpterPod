using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using Verse;

namespace FasterBiosculpterPod
{
    public class FasterBiosculpterPod_Settings : ModSettings
    {
        public const int VanillaBiotuningDurationTicks = 3600000;
        public const int RecommendedBiotuningDurationTicks = 900000;

        public const int VanillaAgeReversalTicks = 3600000;
        public const int RecommendedAgeReversalTicks = 3600000;

        public const float VanillaAgeReversalCycleDays = 10f;
        public const float RecommendedAgeReversalCycleDays = 2.5f;
        public const float VanillaHealingCycleDays = 10f;
        public const float RecommendedHealingCycleDays = 2.5f;
        public const float VanillaPleasureCycleDays = 4f;
        public const float RecommendedPleasureCycleDays = 1f;

        public const float VanillaAgeReversalCycleNutrition = 10f;
        public const float RecommendedAgeReversalCycleNutrition = 2.5f;
        public const float VanillaHealingCycleNutrition = 10f;
        public const float RecommendedHealingCycleNutrition = 2.5f;
        public const float VanillaPleasureCycleNutrition = 5f;
        public const float RecommendedPleasureCycleNutrition = 1.25f;

        public const float VanillaPowerConsumption = 150f;
        public const float RecommendedPowerConsumption = 600f;

        public const string BiosculpterPodDescription = "An immersive pod equipped with a biosculpting fluid injector and attached control system. It can perform a variety of biological alterations including age reversal and pleasure-giving. Each pod becomes biotuned to its user, and cannot be used by anyone else. Biotuning resets after {0} days. Believers in transhumanism believe biosculpter pods are of critical importance in their quest to transcend normal human physical limitations.";
        public const string BiosculpterPodDescriptionNoBiotuning = "An immersive pod equipped with a biosculpting fluid injector and attached control system. It can perform a variety of biological alterations including age reversal and pleasure-giving. Believers in transhumanism believe biosculpter pods are of critical importance in their quest to transcend normal human physical limitations.";

        public int BiotuningDurationTicks = RecommendedBiotuningDurationTicks;

        public int AgeReversalTicks = RecommendedAgeReversalTicks;

        public float AgeReversalCycleDays = RecommendedAgeReversalCycleDays;
        public float HealingCycleDays = RecommendedHealingCycleDays;
        public float PleasureCycleDays = RecommendedPleasureCycleDays;

        public float AgeReversalCycleNutrition = RecommendedAgeReversalCycleNutrition;
        public float HealingCycleNutrition = RecommendedHealingCycleNutrition;
        public float PleasureCycleNutrition = RecommendedPleasureCycleNutrition;

        public float PowerConsumption = RecommendedPowerConsumption;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref BiotuningDurationTicks, "biotuningDurationTicks", RecommendedBiotuningDurationTicks);

            Scribe_Values.Look(ref AgeReversalTicks, "ageReversalTicks", RecommendedAgeReversalTicks);

            Scribe_Values.Look(ref AgeReversalCycleDays, "ageReversalCycleDays", RecommendedAgeReversalCycleDays);
            Scribe_Values.Look(ref HealingCycleDays, "healingCycleDays", RecommendedHealingCycleDays);
            Scribe_Values.Look(ref PleasureCycleDays, "pleasureCycleDays", RecommendedPleasureCycleDays);

            Scribe_Values.Look(ref AgeReversalCycleNutrition, "ageReversalCycleNutrition", RecommendedAgeReversalCycleNutrition);
            Scribe_Values.Look(ref HealingCycleNutrition, "healingCycleNutrition", RecommendedHealingCycleNutrition);
            Scribe_Values.Look(ref PleasureCycleNutrition, "pleasureCycleNutrition", RecommendedPleasureCycleNutrition);

            Scribe_Values.Look(ref PowerConsumption, "powerConsumption", RecommendedPowerConsumption);

            base.ExposeData();
        }
    }

    public class FasterBiosculpterPod_Mod : Mod
    {
        public static FasterBiosculpterPod_Settings settings;

        public FasterBiosculpterPod_Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<FasterBiosculpterPod_Settings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            float leftRectPadding = 10f;
            float rightRectPct = 0.80f;

            listingStandard.Begin(inRect);
            Rect rectLeft = listingStandard.GetRect(Text.LineHeight);
            rectLeft.y += leftRectPadding;
            Widgets.DrawLineHorizontal(0, rectLeft.y, inRect.width);
            rectLeft.y += leftRectPadding * 2;
            Widgets.Label(rectLeft, "Cycle_Durations".Translate());
            rectLeft.y += leftRectPadding + Text.LineHeight;
            Rect rectRight = rectLeft.RightPart(rightRectPct).Rounded();
            Widgets.Label(rectLeft, "Healing".Translate());
            settings.HealingCycleDays = Widgets.HorizontalSlider(rectRight, settings.HealingCycleDays, 0f, 60f, true, settings.HealingCycleDays.ToString() + " days", null, null, 0.1f);
            rectLeft.y += leftRectPadding + Text.LineHeight;
            rectRight = rectLeft.RightPart(rightRectPct).Rounded();
            Widgets.Label(rectLeft, "Age_Reversal".Translate());
            settings.AgeReversalCycleDays = Widgets.HorizontalSlider(rectRight, settings.AgeReversalCycleDays, 0f, 60f, true, settings.AgeReversalCycleDays.ToString() + " days", null, null, 0.1f);
            rectLeft.y += leftRectPadding + Text.LineHeight;
            rectRight = rectLeft.RightPart(rightRectPct).Rounded();
            Widgets.Label(rectLeft, "Pleasure".Translate());
            settings.PleasureCycleDays = Widgets.HorizontalSlider(rectRight, settings.PleasureCycleDays, 0f, 60f, true, settings.PleasureCycleDays.ToString() + " days", null, null, 0.1f);
            rectLeft.y += leftRectPadding * 2 + Text.LineHeight;
            Widgets.DrawLineHorizontal(0, rectLeft.y, inRect.width);
            rectLeft.y += leftRectPadding * 2;
            Widgets.Label(rectLeft, "Cycle_Nutrition_Requirements".Translate());
            rectLeft.y += leftRectPadding + Text.LineHeight;
            rectRight = rectLeft.RightPart(rightRectPct).Rounded();
            Widgets.Label(rectLeft, "Healing".Translate());
            settings.HealingCycleNutrition = Widgets.HorizontalSlider(rectRight, settings.HealingCycleNutrition, 0f, 60f, true, settings.HealingCycleNutrition.ToString() + " nutrition", null, null, 0.1f);
            rectLeft.y += leftRectPadding + Text.LineHeight;
            rectRight = rectLeft.RightPart(rightRectPct).Rounded();
            Widgets.Label(rectLeft, "Age_Reversal".Translate());
            settings.AgeReversalCycleNutrition = Widgets.HorizontalSlider(rectRight, settings.AgeReversalCycleNutrition, 0f, 60f, true, settings.AgeReversalCycleNutrition.ToString() + " nutrition", null, null, 0.1f);
            rectLeft.y += leftRectPadding + Text.LineHeight;
            rectRight = rectLeft.RightPart(rightRectPct).Rounded();
            Widgets.Label(rectLeft, "Pleasure".Translate());
            settings.PleasureCycleNutrition = Widgets.HorizontalSlider(rectRight, settings.PleasureCycleNutrition, 0f, 60f, true, settings.PleasureCycleNutrition.ToString() + " nutrition", null, null, 0.1f);
            rectLeft.y += leftRectPadding * 2 + Text.LineHeight;
            Widgets.DrawLineHorizontal(0, rectLeft.y, inRect.width);
            rectLeft.y += leftRectPadding * 2;
            rectRight = rectLeft.RightPart(rightRectPct).Rounded();
            Widgets.Label(rectLeft, "Biotuning_Duration".Translate());
            settings.BiotuningDurationTicks = (int)Widgets.HorizontalSlider(rectRight, settings.BiotuningDurationTicks, 0, 21600000, true, settings.BiotuningDurationTicks.ToStringTicksToDays("F0"), null, null, 60000);
            rectLeft.y += leftRectPadding * 2 + Text.LineHeight;
            Widgets.DrawLineHorizontal(0, rectLeft.y, inRect.width);
            rectLeft.y += leftRectPadding * 2;
            rectRight = rectLeft.RightPart(rightRectPct).Rounded();
            Widgets.Label(rectLeft, "Years_Reversed".Translate());
            settings.AgeReversalTicks = (int)Widgets.HorizontalSlider(rectRight, settings.AgeReversalTicks, 0, 72000000, true, settings.AgeReversalTicks.ToStringTicksToDays("F0"), null, null, 60000);
            rectLeft.y += leftRectPadding * 2 + Text.LineHeight;
            Widgets.DrawLineHorizontal(0, rectLeft.y, inRect.width);
            rectLeft.y += leftRectPadding * 2;
            rectRight = rectLeft.RightPart(rightRectPct).Rounded();
            Widgets.Label(rectLeft, "Base_Power_Consumption".Translate());
            settings.PowerConsumption = Widgets.HorizontalSlider(rectRight, settings.PowerConsumption, 0f, 900f, true, settings.PowerConsumption.ToString() + " W", null, null, 1f);
            rectLeft.y += leftRectPadding * 2 + Text.LineHeight;
            Widgets.DrawLineHorizontal(0, rectLeft.y, inRect.width);
            rectLeft.y += leftRectPadding * 2;
            rectLeft.height = 30f;
            rectLeft.width = inRect.width * 0.3f;
            if (Widgets.ButtonText(rectLeft, "Apply_Custom_Values".Translate()))
            {
                ApplySettings();
            }
            rectLeft.x += inRect.width * 0.35f;
            if (Widgets.ButtonText(rectLeft, "Apply_Recommended_Values".Translate()))
            {
                settings.HealingCycleDays = FasterBiosculpterPod_Settings.RecommendedHealingCycleDays;
                settings.AgeReversalCycleDays = FasterBiosculpterPod_Settings.RecommendedAgeReversalCycleDays;
                settings.PleasureCycleDays = FasterBiosculpterPod_Settings.RecommendedPleasureCycleDays;

                settings.HealingCycleNutrition = FasterBiosculpterPod_Settings.RecommendedHealingCycleNutrition;
                settings.AgeReversalCycleNutrition = FasterBiosculpterPod_Settings.RecommendedAgeReversalCycleNutrition;
                settings.PleasureCycleNutrition = FasterBiosculpterPod_Settings.RecommendedPleasureCycleNutrition;

                settings.BiotuningDurationTicks = FasterBiosculpterPod_Settings.RecommendedBiotuningDurationTicks;

                settings.AgeReversalTicks = FasterBiosculpterPod_Settings.RecommendedAgeReversalTicks;

                settings.PowerConsumption = FasterBiosculpterPod_Settings.RecommendedPowerConsumption;

                ApplySettings();
            }
            rectLeft.x += inRect.width * 0.35f;
            if (Widgets.ButtonText(rectLeft, "Apply_Vanilla_Values".Translate()))
            {
                settings.HealingCycleDays = FasterBiosculpterPod_Settings.VanillaHealingCycleDays;
                settings.AgeReversalCycleDays = FasterBiosculpterPod_Settings.VanillaAgeReversalCycleDays;
                settings.PleasureCycleDays = FasterBiosculpterPod_Settings.VanillaPleasureCycleDays;

                settings.HealingCycleNutrition = FasterBiosculpterPod_Settings.VanillaHealingCycleNutrition;
                settings.AgeReversalCycleNutrition = FasterBiosculpterPod_Settings.VanillaAgeReversalCycleNutrition;
                settings.PleasureCycleNutrition = FasterBiosculpterPod_Settings.VanillaPleasureCycleNutrition;

                settings.BiotuningDurationTicks = FasterBiosculpterPod_Settings.VanillaBiotuningDurationTicks;

                settings.AgeReversalTicks = FasterBiosculpterPod_Settings.VanillaAgeReversalTicks;

                settings.PowerConsumption = FasterBiosculpterPod_Settings.VanillaPowerConsumption;

                ApplySettings();
            }
            listingStandard.End();

            base.DoSettingsWindowContents(inRect);
        }

        /**
         * Hacky means of providing compatibility for mods which replace comps with custom ones instead of patching them using Harmony.
         */
        private static void UpdateFieldUsingReflection(string typeName, string fieldName, float newFieldValue)
        {
            List<CompProperties> comps = DefDatabase<ThingDef>.GetNamed("BiosculpterPod").comps;
            CompProperties compProperty = DefDatabase<ThingDef>.GetNamed("BiosculpterPod").comps.Find(x => x.GetType().FullName.Contains(typeName));
            var convertedComp = Convert.ChangeType(compProperty, compProperty.GetType());
            FieldInfo field = convertedComp.GetType().GetField(fieldName);
            field.SetValue(convertedComp, newFieldValue);
        }

        public static void ApplySettings()
        {
            Harmony harmony = new Harmony("Inglix.FasterBiosculpterPod");
            harmony.PatchAll();

            if (LoadedModManager.RunningModsListForReading.Find(mod => mod.PackageId.EqualsIgnoreCase("Troopersmith1.AgeMatters")) != null)
            {
                UpdateFieldUsingReflection("CompProperties_BiosculpterPod_AgeReversalCycle", "durationDays", settings.AgeReversalCycleDays);
                UpdateFieldUsingReflection("CompProperties_BiosculpterPod_AgeReversalCycle", "nutritionRequired", settings.AgeReversalCycleNutrition);
                Type ageReversalCycleType = Type.GetType("AgeMatters.CompBiosculpterPod_AgeReversalCycle,AgeMatters");
                Log.Warning("TYPE FOUND: " + ageReversalCycleType.FullName);
                Type harmonyClassType = typeof(TranspileCycleCompleted);
                Log.Warning("TYPE FOUND: " + harmonyClassType.FullName);
                MethodInfo transpiler = harmonyClassType.GetMethod("Transpiler", BindingFlags.Static | BindingFlags.NonPublic);
                Log.Warning("TRANSPILER: " + transpiler.Name);
                harmony.Patch(ageReversalCycleType.GetMethod("CycleCompleted"), transpiler: new HarmonyMethod(transpiler));
            }
            else
            {
                (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_AgeReversalCycle)) as CompProperties_BiosculpterPod_AgeReversalCycle).durationDays = settings.AgeReversalCycleDays;
                (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_AgeReversalCycle)) as CompProperties_BiosculpterPod_AgeReversalCycle).nutritionRequired = settings.AgeReversalCycleNutrition;
            }

            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_HealingCycle)) as CompProperties_BiosculpterPod_HealingCycle).durationDays = settings.HealingCycleDays;
            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_HealingCycle)) as CompProperties_BiosculpterPod_HealingCycle).nutritionRequired = settings.HealingCycleNutrition;

            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_PleasureCycle)) as CompProperties_BiosculpterPod_PleasureCycle).durationDays = settings.PleasureCycleDays;
            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_PleasureCycle)) as CompProperties_BiosculpterPod_PleasureCycle).nutritionRequired = settings.PleasureCycleNutrition;

            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_Power)) as CompProperties_Power).basePowerConsumption = settings.PowerConsumption;

            if (settings.BiotuningDurationTicks > 0)
                (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true)).description = string.Format(FasterBiosculpterPod_Settings.BiosculpterPodDescription, settings.BiotuningDurationTicks.ToStringTicksToDays("F0"));
            else
                (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true)).description = FasterBiosculpterPod_Settings.BiosculpterPodDescriptionNoBiotuning;
        }

        public override string SettingsCategory()
        {
            return "Faster_Biosculpter_Pod".Translate();
        }

    }
    
    [StaticConstructorOnStartup]
    public static class FasterBiosculpterPod_Patches
    {
        static FasterBiosculpterPod_Patches()
        {
            FasterBiosculpterPod_Mod.ApplySettings();
        }
    }

    [HarmonyPatch(typeof(CompBiosculpterPod), nameof(CompBiosculpterPod.TryAcceptPawn))]
    class TranspileTryAcceptPawn
    {
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionList = instructions.ToList();
            for (var i = 0; i < instructionList.Count; i++)
            {
                if (instructionList[i].opcode == OpCodes.Ldc_I4 && (Int32)instructionList[i].operand == FasterBiosculpterPod_Settings.VanillaBiotuningDurationTicks)
                {
                    instructionList[i].operand = LoadedModManager.GetMod<FasterBiosculpterPod_Mod>().GetSettings<FasterBiosculpterPod_Settings>().BiotuningDurationTicks;
                    break;
                }
            }

            return instructionList.AsEnumerable();
        }
    }

    [HarmonyPatch(typeof(CompBiosculpterPod), nameof(CompBiosculpterPod.CompTick))]
    class TranspileCompTick
    {
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionList = instructions.ToList();
            for (var i = 0; i < instructionList.Count; i++)
            {
                if (instructionList[i].opcode == OpCodes.Ldc_I4 && (Int32)instructionList[i].operand == FasterBiosculpterPod_Settings.VanillaBiotuningDurationTicks)
                {
                    instructionList[i].operand = LoadedModManager.GetMod<FasterBiosculpterPod_Mod>().GetSettings<FasterBiosculpterPod_Settings>().BiotuningDurationTicks;
                    break;
                }
            }

            return instructionList.AsEnumerable();
        }
    }

    [HarmonyPatch(typeof(CompBiosculpterPod_AgeReversalCycle), nameof(CompBiosculpterPod_AgeReversalCycle.CycleCompleted))]
    class TranspileCycleCompleted
    {
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionList = instructions.ToList();
            for (var i = 0; i < instructionList.Count; i++)
            {
                if (instructionList[i].opcode == OpCodes.Ldc_I4 && (Int32)instructionList[i].operand == FasterBiosculpterPod_Settings.VanillaAgeReversalTicks)
                {
                    instructionList[i].operand = LoadedModManager.GetMod<FasterBiosculpterPod_Mod>().GetSettings<FasterBiosculpterPod_Settings>().AgeReversalTicks;
                    break;
                }
            }

            return instructionList.AsEnumerable();
        }
    }

    [HarmonyPatch(typeof(Pawn_AgeTracker), nameof(Pawn_AgeTracker.ResetAgeReversalDemand))]
    class TranspileResetAgeReversalDemand
    {
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionList = instructions.ToList();
            for (var i = 0; i < instructionList.Count; i++)
            {
                if (instructionList[i].opcode == OpCodes.Ldc_I4_S && (SByte)instructionList[i].operand == 60)
                {
                    instructionList[i].opcode = OpCodes.Ldc_I4;
                    instructionList[i].operand = (LoadedModManager.GetMod<FasterBiosculpterPod_Mod>().GetSettings<FasterBiosculpterPod_Settings>().AgeReversalTicks / 60000);
                    break;
                }
            }

            return instructionList.AsEnumerable();
        }
    }
}
