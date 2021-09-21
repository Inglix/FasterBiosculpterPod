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
        public const float VanillaPleasureCycleMoodDays = 12f;
        public const float VanillaPleasureCycleMoodEffect = 10f;
        public const float RecommendedPleasureCycleDays = 1f;
        public const float RecommendedPleasureCycleNutrition = 1.3f;
        public const float RecommendedPleasureCycleMoodDays = 12f;
        public const float RecommendedPleasureCycleMoodEffect = 10f;

        public const int VanillaBiotuningDurationTicks = 3600000;
        public const int RecommendedBiotuningDurationTicks = 900000;

        public const float VanillaPowerConsumption = 150f;
        public const float RecommendedPowerConsumption = 600f;

        public float MedicCycleDays = RecommendedMedicCycleDays;
        public float MedicCycleNutrition = RecommendedMedicCycleNutrition;
        public float BioregenerationCycleDays = RecommendedBioregenerationCycleDays;
        public float BioregenerationCycleNutrition = RecommendedBioregenerationCycleNutrition;
        public float BioregenerationCycleMedicineUltratech = RecommendedBioregenerationCycleMedicineUltratech;
        public float AgeReversalCycleDays = RecommendedAgeReversalCycleDays;
        public float AgeReversalCycleNutrition = RecommendedAgeReversalCycleNutrition;
        public float AgeReversalTicks = RecommendedAgeReversalTicks;
        public float AgeReversalDays = RecommendedAgeReversalTicks / 60000;
        public float PleasureCycleDays = RecommendedPleasureCycleDays;
        public float PleasureCycleNutrition = RecommendedPleasureCycleNutrition;
        public float PleasureCycleMoodDays = RecommendedPleasureCycleMoodDays;
        public float PleasureCycleMoodEffect = RecommendedPleasureCycleMoodEffect;

        public float BiotuningDurationTicks = RecommendedBiotuningDurationTicks;
        public float BiotuningDurationDays = RecommendedBiotuningDurationTicks / 60000;

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
            Scribe_Values.Look(ref AgeReversalTicks, "ageReversalTicks", RecommendedAgeReversalTicks); // Deprecated
            Scribe_Values.Look(ref AgeReversalDays, "ageReversalDays", AgeReversalTicks / 60000);

            Scribe_Values.Look(ref PleasureCycleDays, "pleasureCycleDays", RecommendedPleasureCycleDays);
            Scribe_Values.Look(ref PleasureCycleNutrition, "pleasureCycleNutrition", RecommendedPleasureCycleNutrition);
            Scribe_Values.Look(ref PleasureCycleMoodDays, "pleasureCycleMoodDays", RecommendedPleasureCycleMoodDays);
            Scribe_Values.Look(ref PleasureCycleMoodEffect, "pleasureCycleMoodEffect", RecommendedPleasureCycleMoodEffect);

            Scribe_Values.Look(ref BiotuningDurationTicks, "biotuningDurationTicks", RecommendedBiotuningDurationTicks); // Deprecated
            Scribe_Values.Look(ref BiotuningDurationDays, "biotuningDurationDays", BiotuningDurationTicks / 60000);

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
            const float LeftPartPct = 0.0f; // Let the slider take up the entire width of the settings window
            const float CycleDurationIncrement = 0.1f; // Increment cycle durations by 0.1 day increments (2.4 hours)
            const float NutritionRequiredIncrement = 0.1f; // Increment nutrition required by 0.1 nutrition increments
            string glitterworldMedicineName = DefDatabase<ThingDef>.GetNamed("MedicineUltratech").label;
    
            Rect outRect = canvas.TopPart(0.9f);
            Rect rect = new Rect(0f, 0f, outRect.width - 18f, 747.5f);
            Widgets.BeginScrollView(outRect, ref scrollPosition, rect, true);
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(rect);

            listing.AddLabelLine("Inglix.Medic_Cycle".Translate());
            listing.AddLabeledSlider(null, ref settings.MedicCycleDays, 0f, 60f, "Inglix.Cycle_Duration".Translate(), null, CycleDurationIncrement, true, ConvertDaysToTicks(settings.MedicCycleDays).ToStringTicksToPeriodVeryVerbose(), LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.MedicCycleNutrition, 0f, 60f, "Inglix.Nutrition_Required".Translate(), null, NutritionRequiredIncrement, true, settings.MedicCycleNutrition.ToString() + " " + "Nutrition".Translate().ToLower(), LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
            listing.AddLabelLine("Inglix.Bioregeneration_Cycle".Translate());
            listing.AddLabeledSlider(null, ref settings.BioregenerationCycleDays, 0f, 60f, "Inglix.Cycle_Duration".Translate(), null, CycleDurationIncrement, true, ConvertDaysToTicks(settings.BioregenerationCycleDays).ToStringTicksToPeriodVeryVerbose(), LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.BioregenerationCycleNutrition, 0f, 60f, "Inglix.Nutrition_Required".Translate(), null, NutritionRequiredIncrement, true, settings.BioregenerationCycleNutrition.ToString() + " " + "Nutrition".Translate().ToLower(), LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.BioregenerationCycleMedicineUltratech, 0f, 20f, "Inglix.MedicineUltratech_Required".Translate(), null, 1f, true, settings.BioregenerationCycleMedicineUltratech.ToString() + " " + glitterworldMedicineName, LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
            listing.AddLabelLine("Inglix.Age_Reversal_Cycle".Translate());
            listing.AddLabeledSlider(null, ref settings.AgeReversalCycleDays, 0f, 60f, "Inglix.Cycle_Duration".Translate(), null, CycleDurationIncrement, true, ConvertDaysToTicks(settings.AgeReversalCycleDays).ToStringTicksToPeriodVeryVerbose(), LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.AgeReversalCycleNutrition, 0f, 60f, "Inglix.Nutrition_Required".Translate(), null, NutritionRequiredIncrement, true, settings.AgeReversalCycleNutrition.ToString() + " " + "Nutrition".Translate().ToLower(), LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.AgeReversalDays, 0, 840f, "Inglix.Age_Reversed".Translate(), null, 1f, true, ConvertDaysToTicks(settings.AgeReversalDays).ToStringTicksToPeriodVeryVerbose(), LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
            listing.AddLabelLine("Inglix.Pleasure_Cycle".Translate());
            listing.AddLabeledSlider(null, ref settings.PleasureCycleDays, 0f, 60f, "Inglix.Cycle_Duration".Translate(), null, CycleDurationIncrement, true, ConvertDaysToTicks(settings.PleasureCycleDays).ToStringTicksToPeriodVeryVerbose(), LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.PleasureCycleNutrition, 0f, 60f, "Inglix.Nutrition_Required".Translate(), null, NutritionRequiredIncrement, true, settings.PleasureCycleNutrition.ToString() + " " + "Nutrition".Translate().ToLower(), LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.PleasureCycleMoodDays, 0f, 60f, "Inglix.Mood_Duration".Translate(), null, CycleDurationIncrement, true, ConvertDaysToTicks(settings.PleasureCycleMoodDays).ToStringTicksToPeriodVeryVerbose(), LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.PleasureCycleMoodEffect, 0f, 100f, "Inglix.Mood_Effect".Translate(), null, 1f, true, "+" + settings.PleasureCycleMoodEffect.ToString() + " " + "Mood".Translate().ToLower(), LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
            listing.AddLabelLine("Inglix.Miscellaneous_Options".Translate());
            listing.AddLabeledSlider(null, ref settings.BiotuningDurationDays, 0, 840f, "Inglix.Biotuning_Duration".Translate(), null, 1f, true, ConvertDaysToTicks(settings.BiotuningDurationDays).ToStringTicksToPeriodVeryVerbose(), LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.PowerConsumption, 0f, 10000f, "Inglix.Power_Consumption".Translate(), null, 25f, true, settings.PowerConsumption.ToString() + " W", LeftPartPct);
            listing.End();
            Widgets.EndScrollView();

            Rect buttonsRect = canvas.BottomPart(0.075f).LeftPart(0.3f);
            buttonsRect.height = canvas.height * 0.05f;
            if (Widgets.ButtonText(buttonsRect, "Inglix.Apply_Custom_Values".Translate()))
            {
                ApplySettings();
            }
            buttonsRect.x += canvas.width * 0.35f;
            if (Widgets.ButtonText(buttonsRect, "Inglix.Apply_Recommended_Values".Translate()))
            {
                settings.MedicCycleDays = FasterBiosculpterPod_Settings.RecommendedMedicCycleDays;
                settings.MedicCycleNutrition = FasterBiosculpterPod_Settings.RecommendedMedicCycleNutrition;

                settings.BioregenerationCycleDays = FasterBiosculpterPod_Settings.RecommendedBioregenerationCycleDays;
                settings.BioregenerationCycleNutrition = FasterBiosculpterPod_Settings.RecommendedBioregenerationCycleNutrition;
                settings.BioregenerationCycleMedicineUltratech = FasterBiosculpterPod_Settings.RecommendedBioregenerationCycleMedicineUltratech;

                settings.AgeReversalCycleDays = FasterBiosculpterPod_Settings.RecommendedAgeReversalCycleDays;
                settings.AgeReversalCycleNutrition = FasterBiosculpterPod_Settings.RecommendedAgeReversalCycleNutrition;
                settings.AgeReversalDays = FasterBiosculpterPod_Settings.RecommendedAgeReversalTicks / 60000;

                settings.PleasureCycleDays = FasterBiosculpterPod_Settings.RecommendedPleasureCycleDays;
                settings.PleasureCycleNutrition = FasterBiosculpterPod_Settings.RecommendedPleasureCycleNutrition;
                settings.PleasureCycleMoodDays = FasterBiosculpterPod_Settings.RecommendedPleasureCycleMoodDays;
                settings.PleasureCycleMoodEffect = FasterBiosculpterPod_Settings.RecommendedPleasureCycleMoodEffect;

                settings.BiotuningDurationDays = FasterBiosculpterPod_Settings.RecommendedBiotuningDurationTicks / 60000;

                settings.PowerConsumption = FasterBiosculpterPod_Settings.RecommendedPowerConsumption;

                ApplySettings();
            }
            buttonsRect.x += canvas.width * 0.35f;
            if (Widgets.ButtonText(buttonsRect, "Inglix.Apply_Vanilla_Values".Translate()))
            {
                settings.MedicCycleDays = FasterBiosculpterPod_Settings.VanillaMedicCycleDays;
                settings.MedicCycleNutrition = FasterBiosculpterPod_Settings.VanillaMedicCycleNutrition;

                settings.BioregenerationCycleDays = FasterBiosculpterPod_Settings.VanillaBioregenerationCycleDays;
                settings.BioregenerationCycleNutrition = FasterBiosculpterPod_Settings.VanillaBioregenerationCycleNutrition;
                settings.BioregenerationCycleMedicineUltratech = FasterBiosculpterPod_Settings.VanillaBioregenerationCycleMedicineUltratech;

                settings.AgeReversalCycleDays = FasterBiosculpterPod_Settings.VanillaAgeReversalCycleDays;
                settings.AgeReversalCycleNutrition = FasterBiosculpterPod_Settings.VanillaAgeReversalCycleNutrition;
                settings.AgeReversalDays = FasterBiosculpterPod_Settings.VanillaAgeReversalTicks / 60000;

                settings.PleasureCycleDays = FasterBiosculpterPod_Settings.VanillaPleasureCycleDays;
                settings.PleasureCycleNutrition = FasterBiosculpterPod_Settings.VanillaPleasureCycleNutrition;
                settings.PleasureCycleMoodDays = FasterBiosculpterPod_Settings.VanillaPleasureCycleMoodDays;
                settings.PleasureCycleMoodEffect = FasterBiosculpterPod_Settings.VanillaPleasureCycleMoodEffect;

                settings.BiotuningDurationDays = FasterBiosculpterPod_Settings.VanillaBiotuningDurationTicks / 60000;

                settings.PowerConsumption = FasterBiosculpterPod_Settings.VanillaPowerConsumption;

                ApplySettings();
            }

            base.DoSettingsWindowContents(canvas);
        }

        public static int ConvertDaysToTicks(float days)
        {
            return ((int)(days * 60000f));
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
                Log.Warning("Age Matters mod adds a custom version of CompProperties_BiosculpterPod_AgeReversalCycle instead of patching the original. In order to apply settings for the age reversal cycle, the durationDays and nutritionRequired fields must be updated using reflection, and a transpiler must be run against their custom CycleCompleted method.");
                UpdateFieldUsingReflection("CompProperties_BiosculpterPod_AgeReversalCycle", "durationDays", settings.AgeReversalCycleDays);
                UpdateFieldUsingReflection("CompProperties_BiosculpterPod_AgeReversalCycle", "nutritionRequired", settings.AgeReversalCycleNutrition);
                Type ageReversalCycleType = Type.GetType("AgeMatters.CompBiosculpterPod_AgeReversalCycle,AgeMatters");
                Log.Message("AGE REVERSAL CYCLE TYPE FOUND: " + ageReversalCycleType.FullName);
                Type harmonyClassType = typeof(TranspileCycleCompleted);
                Log.Message("HARMONY CLASS TYPE FOUND: " + harmonyClassType.FullName);
                MethodInfo transpiler = harmonyClassType.GetMethod("Transpiler", BindingFlags.Static | BindingFlags.NonPublic);
                Log.Message("TRANSPILER FOUND: " + transpiler.Name);
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
            DefDatabase<ThoughtDef>.GetNamed("BiosculpterPleasure", true).durationDays = settings.PleasureCycleMoodDays;
            DefDatabase<ThoughtDef>.GetNamed("BiosculpterPleasure", true).stages[0].baseMoodEffect = settings.PleasureCycleMoodEffect;

            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_Power)) as CompProperties_Power).basePowerConsumption = settings.PowerConsumption;

            if (settings.BiotuningDurationDays > 0)
                (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true)).description = "Inglix.Biosculpter_Description".Translate(settings.BiotuningDurationDays);
            else
                (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true)).description = "Inglix.Biosculpter_Description_No_Biotuning".Translate();
        }

        public override string SettingsCategory()
        {
            return "Inglix.Faster_Biosculpter_Pod".Translate();
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
                    instructionList[i].operand = FasterBiosculpterPod_Mod.ConvertDaysToTicks(LoadedModManager.GetMod<FasterBiosculpterPod_Mod>().GetSettings<FasterBiosculpterPod_Settings>().BiotuningDurationDays);
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
                    instructionList[i].operand = FasterBiosculpterPod_Mod.ConvertDaysToTicks(LoadedModManager.GetMod<FasterBiosculpterPod_Mod>().GetSettings<FasterBiosculpterPod_Settings>().BiotuningDurationDays);
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
                    instructionList[i].operand = FasterBiosculpterPod_Mod.ConvertDaysToTicks(LoadedModManager.GetMod<FasterBiosculpterPod_Mod>().GetSettings<FasterBiosculpterPod_Settings>().AgeReversalDays);
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
                    instructionList[i].operand = (int)LoadedModManager.GetMod<FasterBiosculpterPod_Mod>().GetSettings<FasterBiosculpterPod_Settings>().AgeReversalDays;
                    break;
                }
            }

            return instructionList.AsEnumerable();
        }
    }
}
