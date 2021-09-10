using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using Verse;
using SettingsHelper;

namespace FasterBiosculpterPod
{
    public class FasterBiosculpterPod_Settings : ModSettings
    {
        public const float VanillaMedicCycleDays = 6f;
        public const float VanillaMedicCycleNutrition = 5f;
        public const float RecommendedMedicCycleDays = 1.5f;
        public const float RecommendedMedicCycleNutrition = 1.3f;

        public const float VanillaBioregenerationCycleDays = 25f;
        public const float VanillaBioregenerationCycleNutrition = 30f;
        public const float VanillaBioregenerationCycleMedicineUltratech = 2f;
        public const float RecommendedBioregenerationCycleDays = 6.3f;
        public const float RecommendedBioregenerationCycleNutrition = 7.5f;
        public const float RecommendedBioregenerationCycleMedicineUltratech = 1f;

        public const float VanillaAgeReversalCycleDays = 8f;
        public const float VanillaAgeReversalCycleNutrition = 5f;
        public const int VanillaAgeReversalTicks = 3600000;
        public const float RecommendedAgeReversalCycleDays = 2f;
        public const float RecommendedAgeReversalCycleNutrition = 1.3f;
        public const int RecommendedAgeReversalTicks = 3600000;

        public const float VanillaPleasureCycleDays = 4f;
        public const float VanillaPleasureCycleNutrition = 5f;
        public const float RecommendedPleasureCycleDays = 1f;
        public const float RecommendedPleasureCycleNutrition = 1.3f;

        public const int VanillaBiotuningDurationTicks = 3600000;
        public const int RecommendedBiotuningDurationTicks = 900000;

        public const float VanillaPowerConsumption = 150f;
        public const float RecommendedPowerConsumption = 600f;

        public const string BiosculpterPodDescription = "An immersive pod equipped with a biosculpting fluid injector and attached control system. It can perform a variety of biological alterations including age reversal and pleasure-giving. Each pod becomes biotuned to its user, and cannot be used by anyone else. Biotuning resets after {0} days. Believers in transhumanism believe biosculpter pods are of critical importance in their quest to transcend normal human physical limitations.";
        public const string BiosculpterPodDescriptionNoBiotuning = "An immersive pod equipped with a biosculpting fluid injector and attached control system. It can perform a variety of biological alterations including age reversal and pleasure-giving. Believers in transhumanism believe biosculpter pods are of critical importance in their quest to transcend normal human physical limitations.";

        public float BiotuningDurationTicks = RecommendedBiotuningDurationTicks;

        public float AgeReversalTicks = RecommendedAgeReversalTicks;

        public float MedicCycleDays = RecommendedMedicCycleDays;
        public float MedicCycleNutrition = RecommendedMedicCycleNutrition;
        public float BioregenerationCycleDays = RecommendedBioregenerationCycleDays;
        public float BioregenerationCycleNutrition = RecommendedBioregenerationCycleNutrition;
        public float BioregenerationCycleMedicineUltratech = RecommendedBioregenerationCycleMedicineUltratech;
        public float AgeReversalCycleDays = RecommendedAgeReversalCycleDays;
        public float AgeReversalCycleNutrition = RecommendedAgeReversalCycleNutrition;
        public float PleasureCycleDays = RecommendedPleasureCycleDays;
        public float PleasureCycleNutrition = RecommendedPleasureCycleNutrition;

        public float PowerConsumption = RecommendedPowerConsumption;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref MedicCycleDays, "medicCycleDays", RecommendedMedicCycleDays);
            Scribe_Values.Look(ref MedicCycleNutrition, "medicCycleNutrition", RecommendedMedicCycleNutrition);

            Scribe_Values.Look(ref BioregenerationCycleDays, "bioregenerationCycleDays", RecommendedBioregenerationCycleDays);
            Scribe_Values.Look(ref BioregenerationCycleNutrition, "bioregenerationCycleNutrition", RecommendedBioregenerationCycleNutrition);
            Scribe_Values.Look(ref BioregenerationCycleMedicineUltratech, "bioregenerationCycleMedicineUltratech", RecommendedBioregenerationCycleMedicineUltratech);

            Scribe_Values.Look(ref AgeReversalCycleDays, "ageReversalCycleDays", RecommendedAgeReversalCycleDays);
            Scribe_Values.Look(ref AgeReversalCycleNutrition, "ageReversalCycleNutrition", RecommendedAgeReversalCycleNutrition);
            Scribe_Values.Look(ref AgeReversalTicks, "ageReversalTicks", RecommendedAgeReversalTicks);

            Scribe_Values.Look(ref PleasureCycleDays, "pleasureCycleDays", RecommendedPleasureCycleDays);
            Scribe_Values.Look(ref PleasureCycleNutrition, "pleasureCycleNutrition", RecommendedPleasureCycleNutrition);

            Scribe_Values.Look(ref BiotuningDurationTicks, "biotuningDurationTicks", RecommendedBiotuningDurationTicks);

            Scribe_Values.Look(ref PowerConsumption, "powerConsumption", RecommendedPowerConsumption);

            base.ExposeData();
        }
    }

    public class FasterBiosculpterPod_Mod : Mod
    {
        public static FasterBiosculpterPod_Settings settings;
        private Vector2 scrollPosition;

        public FasterBiosculpterPod_Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<FasterBiosculpterPod_Settings>();
        }

        public override void DoSettingsWindowContents(Rect canvas)
        {
            const float LeftPartPct = 0.2f;

            Rect outRect = canvas.TopPart(0.9f);
            Rect rect = new Rect(0f, 0f, outRect.width - 18f, 682.5f);
            Widgets.BeginScrollView(outRect, ref scrollPosition, rect, true);
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(rect);

            listing.AddLabelLine("Medic_Cycle".Translate());
            listing.AddLabeledSlider("Cycle_Duration".Translate(), ref settings.MedicCycleDays, 0f, 60f, null, null, 0.1f, true, settings.MedicCycleDays.ToString() + "_days".Translate(), LeftPartPct);
            listing.AddLabeledSlider("Nutrition_Required".Translate(), ref settings.MedicCycleNutrition, 0f, 60f, null, null, 0.1f, true, settings.MedicCycleNutrition.ToString() + "_nutrition".Translate(), LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
            listing.AddLabelLine("Bioregeneration_Cycle".Translate());
            listing.AddLabeledSlider("Cycle_Duration".Translate(), ref settings.BioregenerationCycleDays, 0f, 60f, null, null, 0.1f, true, settings.BioregenerationCycleDays.ToString() + "_days".Translate(), LeftPartPct);
            listing.AddLabeledSlider("Nutrition_Required".Translate(), ref settings.BioregenerationCycleNutrition, 0f, 60f, null, null, 0.1f, true, settings.BioregenerationCycleNutrition.ToString() + "_nutrition".Translate(), LeftPartPct);
            listing.AddLabeledSlider("MedicineUltratech_Required".Translate(), ref settings.BioregenerationCycleMedicineUltratech, 0f, 10f, null, null, 1f, true, settings.BioregenerationCycleMedicineUltratech.ToString() + "_medicine_ultratech".Translate(), LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
            listing.AddLabelLine("Age_Reversal_Cycle".Translate());
            listing.AddLabeledSlider("Cycle_Duration".Translate(), ref settings.AgeReversalCycleDays, 0f, 60f, null, null, 0.1f, true, settings.AgeReversalCycleDays.ToString() + "_days".Translate(), LeftPartPct);
            listing.AddLabeledSlider("Nutrition_Required".Translate(), ref settings.AgeReversalCycleNutrition, 0f, 60f, null, null, 0.1f, true, settings.AgeReversalCycleNutrition.ToString() + "_nutrition".Translate(), LeftPartPct);
            listing.AddLabeledSlider("Age_Reversal".Translate(), ref settings.AgeReversalTicks, 0, 72000000, null, null, 60000f, true, ((int)settings.AgeReversalTicks).ToStringTicksToDays("F0"), LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
            listing.AddLabelLine("Pleasure_Cycle".Translate());
            listing.AddLabeledSlider("Cycle_Duration".Translate(), ref settings.PleasureCycleDays, 0f, 60f, null, null, 0.1f, true, settings.PleasureCycleDays.ToString() + "_days".Translate(), LeftPartPct);
            listing.AddLabeledSlider("Nutrition_Required".Translate(), ref settings.PleasureCycleNutrition, 0f, 60f, null, null, 0.1f, true, settings.PleasureCycleNutrition.ToString() + "_nutrition".Translate(), LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
            listing.AddLabeledSlider("Biotuning_Duration".Translate(), ref settings.BiotuningDurationTicks, 0, 72000000, null, null, 60000f, true, ((int)settings.BiotuningDurationTicks).ToStringTicksToDays("F0"), LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
            listing.AddLabeledSlider("Power_Consumption".Translate(), ref settings.PowerConsumption, 0f, 10000f, null, null, 25f, true, settings.PowerConsumption.ToString() + "_W".Translate(), LeftPartPct);
            listing.End();
            Widgets.EndScrollView();

            Rect buttonsRect = canvas.BottomPart(0.075f).LeftPart(0.3f);
            buttonsRect.height = canvas.height * 0.05f;
            if (Widgets.ButtonText(buttonsRect, "Apply_Custom_Values".Translate()))
            {
                ApplySettings();
            }
            buttonsRect.x += canvas.width * 0.35f;
            if (Widgets.ButtonText(buttonsRect, "Apply_Recommended_Values".Translate()))
            {
                settings.MedicCycleDays = FasterBiosculpterPod_Settings.RecommendedMedicCycleDays;
                settings.MedicCycleNutrition = FasterBiosculpterPod_Settings.RecommendedMedicCycleNutrition;

                settings.BioregenerationCycleDays = FasterBiosculpterPod_Settings.RecommendedBioregenerationCycleDays;
                settings.BioregenerationCycleNutrition = FasterBiosculpterPod_Settings.RecommendedBioregenerationCycleNutrition;
                settings.BioregenerationCycleMedicineUltratech = FasterBiosculpterPod_Settings.RecommendedBioregenerationCycleMedicineUltratech;

                settings.AgeReversalCycleDays = FasterBiosculpterPod_Settings.RecommendedAgeReversalCycleDays;
                settings.AgeReversalCycleNutrition = FasterBiosculpterPod_Settings.RecommendedAgeReversalCycleNutrition;
                settings.AgeReversalTicks = FasterBiosculpterPod_Settings.RecommendedAgeReversalTicks;

                settings.PleasureCycleDays = FasterBiosculpterPod_Settings.RecommendedPleasureCycleDays;
                settings.PleasureCycleNutrition = FasterBiosculpterPod_Settings.RecommendedPleasureCycleNutrition;

                settings.BiotuningDurationTicks = FasterBiosculpterPod_Settings.RecommendedBiotuningDurationTicks;

                settings.PowerConsumption = FasterBiosculpterPod_Settings.RecommendedPowerConsumption;

                ApplySettings();
            }
            buttonsRect.x += canvas.width * 0.35f;
            if (Widgets.ButtonText(buttonsRect, "Apply_Vanilla_Values".Translate()))
            {
                settings.MedicCycleDays = FasterBiosculpterPod_Settings.VanillaMedicCycleDays;
                settings.MedicCycleNutrition = FasterBiosculpterPod_Settings.VanillaMedicCycleNutrition;

                settings.BioregenerationCycleDays = FasterBiosculpterPod_Settings.VanillaBioregenerationCycleDays;
                settings.BioregenerationCycleNutrition = FasterBiosculpterPod_Settings.VanillaBioregenerationCycleNutrition;
                settings.BioregenerationCycleMedicineUltratech = FasterBiosculpterPod_Settings.VanillaBioregenerationCycleMedicineUltratech;

                settings.AgeReversalCycleDays = FasterBiosculpterPod_Settings.VanillaAgeReversalCycleDays;
                settings.AgeReversalCycleNutrition = FasterBiosculpterPod_Settings.VanillaAgeReversalCycleNutrition;
                settings.AgeReversalTicks = FasterBiosculpterPod_Settings.VanillaAgeReversalTicks;

                settings.PleasureCycleDays = FasterBiosculpterPod_Settings.VanillaPleasureCycleDays;
                settings.PleasureCycleNutrition = FasterBiosculpterPod_Settings.VanillaPleasureCycleNutrition;

                settings.BiotuningDurationTicks = FasterBiosculpterPod_Settings.VanillaBiotuningDurationTicks;

                settings.PowerConsumption = FasterBiosculpterPod_Settings.VanillaPowerConsumption;

                ApplySettings();
            }

            base.DoSettingsWindowContents(canvas);
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

            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_HealingCycle) && x.compClass == typeof(CompBiosculpterPod_MedicCycle)) as CompProperties_BiosculpterPod_HealingCycle).durationDays = settings.MedicCycleDays;
            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_HealingCycle) && x.compClass == typeof(CompBiosculpterPod_MedicCycle)) as CompProperties_BiosculpterPod_HealingCycle).nutritionRequired = settings.MedicCycleNutrition;


            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_HealingCycle) && x.compClass == typeof(CompBiosculpterPod_RegenerationCycle)) as CompProperties_BiosculpterPod_HealingCycle).durationDays = settings.BioregenerationCycleDays;
            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_HealingCycle) && x.compClass == typeof(CompBiosculpterPod_RegenerationCycle)) as CompProperties_BiosculpterPod_HealingCycle).nutritionRequired = settings.BioregenerationCycleNutrition;

            List<ThingDefCountClass> requiredExtraIngredients = new List<ThingDefCountClass>();
            if (settings.BioregenerationCycleMedicineUltratech > 0f)
            {
                ThingDefCountClass ultratechMedicine = new ThingDefCountClass(ThingDefOf.MedicineUltratech, (int)settings.BioregenerationCycleMedicineUltratech);
                requiredExtraIngredients.Add(ultratechMedicine);
            }
            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_HealingCycle) && x.compClass == typeof(CompBiosculpterPod_RegenerationCycle)) as CompProperties_BiosculpterPod_HealingCycle).extraRequiredIngredients = requiredExtraIngredients;

            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_PleasureCycle)) as CompProperties_BiosculpterPod_PleasureCycle).durationDays = settings.PleasureCycleDays;
            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_PleasureCycle)) as CompProperties_BiosculpterPod_PleasureCycle).nutritionRequired = settings.PleasureCycleNutrition;

            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_Power)) as CompProperties_Power).basePowerConsumption = settings.PowerConsumption;

            if (settings.BiotuningDurationTicks > 0)
                (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true)).description = string.Format(FasterBiosculpterPod_Settings.BiosculpterPodDescription, ((int)settings.BiotuningDurationTicks).ToStringTicksToDays("F0"));
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
                    instructionList[i].operand = (int)LoadedModManager.GetMod<FasterBiosculpterPod_Mod>().GetSettings<FasterBiosculpterPod_Settings>().BiotuningDurationTicks;
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
                    instructionList[i].operand = (int)LoadedModManager.GetMod<FasterBiosculpterPod_Mod>().GetSettings<FasterBiosculpterPod_Settings>().BiotuningDurationTicks;
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
                    instructionList[i].operand = (int)LoadedModManager.GetMod<FasterBiosculpterPod_Mod>().GetSettings<FasterBiosculpterPod_Settings>().AgeReversalTicks;
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
                    instructionList[i].operand = ((int)LoadedModManager.GetMod<FasterBiosculpterPod_Mod>().GetSettings<FasterBiosculpterPod_Settings>().AgeReversalTicks / 60000);
                    break;
                }
            }

            return instructionList.AsEnumerable();
        }
    }
}
