using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using SettingsHelper;

namespace FasterBiosculpterPod
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class HotSwappableAttribute : Attribute
    {
    }

    [HotSwappable]
    public class FasterBiosculpterPod_Settings : ModSettings
    {
        public const float VanillaMedicCycleDays = 6f;
        public const float RecommendedMedicCycleDays = 3f;

        public const float VanillaBioregenerationCycleDays = 25f;
        public const float VanillaBioregenerationCycleMedicineUltratech = 2f;
        public const float RecommendedBioregenerationCycleDays = 12.5f;
        public const float RecommendedBioregenerationCycleMedicineUltratech = VanillaBioregenerationCycleMedicineUltratech;

        public const float VanillaAgeReversalCycleDays = 8f;
        public const int VanillaAgeReversalTicks = 3600000;
        public const float RecommendedAgeReversalCycleDays = 4f;
        public const int RecommendedAgeReversalTicks = 3600000;

        public const float VanillaPleasureCycleDays = 4f;
        public const float VanillaPleasureCycleMoodDays = 12f;
        public const float VanillaPleasureCycleMoodEffect = 15f;
        public const float RecommendedPleasureCycleDays = 2f;
        public const float RecommendedPleasureCycleMoodDays = VanillaPleasureCycleMoodDays;
        public const float RecommendedPleasureCycleMoodEffect = VanillaPleasureCycleMoodEffect;

        public const float VanillaNutritionRequired = 5f;
        public const float RecommendedNutritionRequired = VanillaNutritionRequired;

        public const int VanillaBiotuningDurationTicks = 4800000;
        public const int RecommendedBiotuningDurationTicks = VanillaBiotuningDurationTicks;
        public const float VanillaBiotunedCycleSpeedFactor = 1.25f;
        public const float RecommendedBiotunedCycleSpeedFactor = VanillaBiotunedCycleSpeedFactor;

        public const float VanillaPowerConsumption = 200f;
        public const float RecommendedPowerConsumption = 800f;
        public const float VanillaStandbyConsumption = 50f;
        public const float RecommendedStandbyConsumption = 0f;

        public const float VanillaSteelCost = 120f;
        public const float VanillaComponentIndustrialCost = 4f;
        public const float VanillaPlasteelCost = 0f;
        public const float VanillaComponentSpacerCost = 0f;
        public const float VanillaUraniumCost = 0f;
        public const float VanillaWorkToBuild = 28000f;
        public const float RecommendedSteelCost = VanillaSteelCost;
        public const float RecommendedComponentIndustrialCost = VanillaComponentIndustrialCost;
        public const float RecommendedPlasteelCost = VanillaPlasteelCost;
        public const float RecommendedComponentSpacerCost = VanillaComponentSpacerCost;
        public const float RecommendedUraniumCost = VanillaUraniumCost;
        public const float RecommendedWorkToBuild = VanillaWorkToBuild;

        public const float VanillaResearchBaseCostBiosculpting = 1500f;
        public const float VanillaResearchBaseCostBioregeneration = 4000f;
        public const TechLevel VanillaResearchLevelBiosculpting = TechLevel.Industrial;
        public const TechLevel VanillaResearchLevelBioregeneration = TechLevel.Industrial;
        public const bool VanillaResearchBiosculptingHiTechResearchBench = true;
        public const bool VanillaResearchBiosculptingMultiAnalyzer = false;
        public const bool VanillaResearchBioregenerationHiTechResearchBench = true;
        public const bool VanillaResearchBioregenerationMultiAnalyzer = true;
        public const float RecommendedResearchBaseCostBiosculpting = VanillaResearchBaseCostBiosculpting;
        public const float RecommendedResearchBaseCostBioregeneration = VanillaResearchBaseCostBioregeneration;
        public const TechLevel RecommendedResearchLevelBiosculpting = VanillaResearchLevelBiosculpting;
        public const TechLevel RecommendedResearchLevelBioregeneration = VanillaResearchLevelBioregeneration;
        public const bool RecommendedResearchBiosculptingHiTechResearchBench = true;
        public const bool RecommendedResearchBiosculptingMultiAnalyzer = false;
        public const bool RecommendedResearchBioregenerationHiTechResearchBench = true;
        public const bool RecommendedResearchBioregenerationMultiAnalyzer = true;

        public float MedicCycleDays = VanillaMedicCycleDays;
        public float BioregenerationCycleDays = VanillaBioregenerationCycleDays;
        public float BioregenerationCycleMedicineUltratech = VanillaBioregenerationCycleMedicineUltratech;
        public float AgeReversalCycleDays = VanillaAgeReversalCycleDays;
        public float AgeReversalTicks = VanillaAgeReversalTicks;
        public float AgeReversalDays = VanillaAgeReversalTicks / 60000;
        public float PleasureCycleDays = VanillaPleasureCycleDays;
        public float PleasureCycleMoodDays = VanillaPleasureCycleMoodDays;
        public float PleasureCycleMoodEffect = VanillaPleasureCycleMoodEffect;

        public float NutritionRequired = VanillaNutritionRequired;

        public float BiotuningDurationTicks = VanillaBiotuningDurationTicks;
        public float BiotuningDurationDays = VanillaBiotuningDurationTicks / 60000;
        public float BiotunedCycleSpeedFactor = VanillaBiotunedCycleSpeedFactor;

        public float PowerConsumption = VanillaPowerConsumption;
        public float StandbyConsumption = VanillaStandbyConsumption;

        public bool EnableBuildCostSettings = false;
        public List<ThingDefCountClass> InitialCostList;
        public float InitialWorkToBuild;
        public float SteelCost = VanillaSteelCost;
        public float ComponentIndustrialCost = VanillaComponentIndustrialCost;
        public float PlasteelCost = VanillaPlasteelCost;
        public float ComponentSpacerCost = VanillaComponentSpacerCost;
        public float UraniumCost = VanillaUraniumCost;
        public float WorkToBuild = VanillaWorkToBuild;

        public float ResearchBaseCostBiosculpting = VanillaResearchBaseCostBiosculpting;
        public float ResearchBaseCostBioregeneration = VanillaResearchBaseCostBioregeneration;
        public TechLevel ResearchLevelBiosculpting = VanillaResearchLevelBiosculpting;
        public TechLevel ResearchLevelBioregeneration = VanillaResearchLevelBioregeneration;
        public bool ResearchBiosculptingHiTechResearchBench = VanillaResearchBiosculptingHiTechResearchBench;
        public bool ResearchBiosculptingMultiAnalyzer = VanillaResearchBiosculptingMultiAnalyzer;
        public bool ResearchBioregenerationHiTechResearchBench = VanillaResearchBioregenerationHiTechResearchBench;
        public bool ResearchBioregenerationMultiAnalyzer = VanillaResearchBioregenerationMultiAnalyzer;

        public bool UseQuadrumsForDuration = true;
        public bool UseHoursForDuration = true;
        
        public override void ExposeData()
        {
            Scribe_Values.Look(ref MedicCycleDays, "medicCycleDays", VanillaMedicCycleDays);

            Scribe_Values.Look(ref BioregenerationCycleDays, "bioregenerationCycleDays", VanillaBioregenerationCycleDays);
            Scribe_Values.Look(ref BioregenerationCycleMedicineUltratech, "bioregenerationCycleMedicineUltratech", VanillaBioregenerationCycleMedicineUltratech);

            Scribe_Values.Look(ref AgeReversalCycleDays, "ageReversalCycleDays", VanillaAgeReversalCycleDays);
            Scribe_Values.Look(ref AgeReversalTicks, "ageReversalTicks", VanillaAgeReversalTicks); // Deprecated
            Scribe_Values.Look(ref AgeReversalDays, "ageReversalDays", AgeReversalTicks / 60000);

            Scribe_Values.Look(ref PleasureCycleDays, "pleasureCycleDays", VanillaPleasureCycleDays);
            Scribe_Values.Look(ref PleasureCycleMoodDays, "pleasureCycleMoodDays", VanillaPleasureCycleMoodDays);
            Scribe_Values.Look(ref PleasureCycleMoodEffect, "pleasureCycleMoodEffect", VanillaPleasureCycleMoodEffect);

            Scribe_Values.Look(ref NutritionRequired, "nutritionRequired", VanillaNutritionRequired);

            Scribe_Values.Look(ref BiotuningDurationTicks, "biotuningDurationTicks", VanillaBiotuningDurationTicks); // Deprecated
            Scribe_Values.Look(ref BiotuningDurationDays, "biotuningDurationDays", BiotuningDurationTicks / 60000);
            Scribe_Values.Look(ref BiotunedCycleSpeedFactor, "biotunedCycleSpeedFactor", VanillaBiotunedCycleSpeedFactor);

            Scribe_Values.Look(ref PowerConsumption, "powerConsumption", VanillaPowerConsumption);
            Scribe_Values.Look(ref StandbyConsumption, "standbyConsumption", VanillaStandbyConsumption);

            Scribe_Values.Look(ref EnableBuildCostSettings, "enableBuildCostSettings", false);
            Scribe_Values.Look(ref SteelCost, "steelCost", VanillaSteelCost);
            Scribe_Values.Look(ref ComponentIndustrialCost, "componentIndustrialCost", VanillaComponentIndustrialCost);
            Scribe_Values.Look(ref PlasteelCost, "plasteelCost", VanillaPlasteelCost);
            Scribe_Values.Look(ref ComponentSpacerCost, "componentSpacerCost", VanillaComponentSpacerCost);
            Scribe_Values.Look(ref UraniumCost, "uraniumCost", VanillaUraniumCost);
            Scribe_Values.Look(ref WorkToBuild, "workToBuild", VanillaWorkToBuild);

            Scribe_Values.Look(ref ResearchBaseCostBiosculpting, "researchBaseCostBiosculpting", VanillaResearchBaseCostBiosculpting);
            Scribe_Values.Look(ref ResearchBaseCostBioregeneration, "researchBaseCostBioregeneration", VanillaResearchBaseCostBioregeneration);

            Scribe_Values.Look(ref ResearchLevelBiosculpting, "researchLevelBiosculpting", VanillaResearchLevelBiosculpting);
            Scribe_Values.Look(ref ResearchLevelBioregeneration, "researchLevelBioregeneration", VanillaResearchLevelBioregeneration);

            Scribe_Values.Look(ref ResearchBiosculptingHiTechResearchBench, "researchBiosculptingHiTechResearchBench", VanillaResearchBiosculptingHiTechResearchBench);
            Scribe_Values.Look(ref ResearchBiosculptingMultiAnalyzer, "researchBiosculptingMultiAnalyzer", VanillaResearchBiosculptingMultiAnalyzer);
            Scribe_Values.Look(ref ResearchBioregenerationHiTechResearchBench, "researchBioregenerationHiTechResearchBench", VanillaResearchBioregenerationHiTechResearchBench);
            Scribe_Values.Look(ref ResearchBioregenerationMultiAnalyzer, "researchBioregenerationMultiAnalyzer", VanillaResearchBioregenerationMultiAnalyzer);

            Scribe_Values.Look(ref UseQuadrumsForDuration, "useQuadrumsForDuration", true);
            Scribe_Values.Look(ref UseHoursForDuration, "useHoursForDuration", true);

            base.ExposeData();
        }
    }

    [HotSwappable]
    public class FasterBiosculpterPod_Mod : Mod
    {
#if DEBUG
        private readonly string modVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
#else
        private readonly string modVersion = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
#endif
        public static FasterBiosculpterPod_Settings settings;
        private Vector2 scrollPosition;

        public FasterBiosculpterPod_Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<FasterBiosculpterPod_Settings>();
        }

        public override void DoSettingsWindowContents(Rect canvas)
        {
            /*
             * For reference, the canvas height is 584 and the canvas width is 864
             */
            const float LeftPartPct = 0.0f; // Let the slider take up the entire width of the settings window
            const float CycleDurationIncrement = 0.1f; // Increment cycle durations by 0.1 day increments (2.4 hours)
            const float NutritionRequiredIncrement = 0.1f; // Increment nutrition required by 0.1 nutrition increments
            string glitterworldMedicineName = DefDatabase<ThingDef>.GetNamed("MedicineUltratech").label;
            string biosculptingResearchName = DefDatabase<ResearchProjectDef>.GetNamed("Biosculpting").label;
            string bioregenerationResearchName = DefDatabase<ResearchProjectDef>.GetNamed("Bioregeneration").label;

            string steel = DefDatabase<ThingDef>.GetNamed("Steel").label;
            string plasteel = DefDatabase<ThingDef>.GetNamed("Plasteel").label;
            string component = DefDatabase<ThingDef>.GetNamed("ComponentIndustrial").label;
            string advancedComponent = DefDatabase<ThingDef>.GetNamed("ComponentSpacer").label;
            string uranium = DefDatabase<ThingDef>.GetNamed("Uranium").label;
            string workToBuild = DefDatabase<StatDef>.GetNamed("WorkToBuild").label;

            Dictionary<String, TechLevel> techLevels = new Dictionary<string, TechLevel>();
            techLevels.Add("TechLevel_Industrial".Translate().CapitalizeFirst(), TechLevel.Industrial);
            techLevels.Add("TechLevel_Spacer".Translate().CapitalizeFirst(), TechLevel.Spacer);
            techLevels.Add("TechLevel_Ultra".Translate().CapitalizeFirst(), TechLevel.Ultra);
    
            Rect outRect = canvas.TopPartPixels(472f);
            Rect rect = new Rect(0f, 0f, outRect.width - 33f, 1365.0f);
            Widgets.BeginScrollView(outRect, ref scrollPosition, rect, true);
            Listing_Standard listing = new Listing_Standard();
            listing.maxOneColumn = true;
            listing.Begin(rect);
            listing.AddLabelLine("Inglix.Medic_Cycle".Translate());
            listing.AddLabeledSlider(null, ref settings.MedicCycleDays, 0f, 60f, "Inglix.Cycle_Duration".Translate(), null, CycleDurationIncrement, true, ConvertDaysToTicks(settings.MedicCycleDays).ToStringTicksToPeriodVeryVerbose(settings.UseQuadrumsForDuration, settings.UseHoursForDuration), LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
            listing.AddLabelLine("Inglix.Bioregeneration_Cycle".Translate());
            listing.AddLabeledSlider(null, ref settings.BioregenerationCycleDays, 0f, 60f, "Inglix.Cycle_Duration".Translate(), null, CycleDurationIncrement, true, ConvertDaysToTicks(settings.BioregenerationCycleDays).ToStringTicksToPeriodVeryVerbose(settings.UseQuadrumsForDuration, settings.UseHoursForDuration), LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.BioregenerationCycleMedicineUltratech, 0f, 20f, "Inglix.MedicineUltratech_Required".Translate(), null, 1f, true, settings.BioregenerationCycleMedicineUltratech.ToString() + " " + glitterworldMedicineName, LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
            listing.AddLabelLine("Inglix.Age_Reversal_Cycle".Translate());
            listing.AddLabeledSlider(null, ref settings.AgeReversalCycleDays, 0f, 60f, "Inglix.Cycle_Duration".Translate(), null, CycleDurationIncrement, true, ConvertDaysToTicks(settings.AgeReversalCycleDays).ToStringTicksToPeriodVeryVerbose(settings.UseQuadrumsForDuration, settings.UseHoursForDuration), LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.AgeReversalDays, 0, 840f, "Inglix.Age_Reversed".Translate(), null, 1f, true, ConvertDaysToTicks(settings.AgeReversalDays).ToStringTicksToPeriodVeryVerbose(settings.UseQuadrumsForDuration, settings.UseHoursForDuration), LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
            listing.AddLabelLine("Inglix.Pleasure_Cycle".Translate());
            listing.AddLabeledSlider(null, ref settings.PleasureCycleDays, 0f, 60f, "Inglix.Cycle_Duration".Translate(), null, CycleDurationIncrement, true, ConvertDaysToTicks(settings.PleasureCycleDays).ToStringTicksToPeriodVeryVerbose(settings.UseQuadrumsForDuration, settings.UseHoursForDuration), LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.PleasureCycleMoodDays, 0f, 60f, "Inglix.Mood_Duration".Translate(), null, CycleDurationIncrement, true, ConvertDaysToTicks(settings.PleasureCycleMoodDays).ToStringTicksToPeriodVeryVerbose(settings.UseQuadrumsForDuration, settings.UseHoursForDuration), LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.PleasureCycleMoodEffect, 0f, 100f, "Inglix.Mood_Effect".Translate(), null, 1f, true, "+" + settings.PleasureCycleMoodEffect.ToString() + " " + "Mood".Translate().ToLower(), LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
            listing.AddLabelLine("Inglix.Miscellaneous_Options".Translate());
            listing.AddLabeledSlider(null, ref settings.NutritionRequired, 0f, 60f, "Inglix.Nutrition_Required".Translate(), null, NutritionRequiredIncrement, true, settings.NutritionRequired.ToString() + " " + "Nutrition".Translate().ToLower(), LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.BiotuningDurationDays, 0, 840f, "Inglix.Biotuning_Duration".Translate(), null, 1f, true, ConvertDaysToTicks(settings.BiotuningDurationDays).ToStringTicksToPeriodVeryVerbose(settings.UseQuadrumsForDuration, settings.UseHoursForDuration), LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.BiotunedCycleSpeedFactor, 0, 10f, "Inglix.Biotuned_Speed".Translate(), null, 0.05f, true, settings.BiotunedCycleSpeedFactor.ToString("P0"), LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.PowerConsumption, 0f, 10000f, "Inglix.Power_Consumption".Translate(), null, 25f, true, settings.PowerConsumption.ToString() + " W", LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.StandbyConsumption, 0f, 10000f, "Inglix.Standby_Consumption".Translate(), null, 25f, true, settings.StandbyConsumption.ToString() + " W", LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
            listing.AddLabelLine("Inglix.Building_Cost".Translate());
            listing.AddLabeledCheckbox("Enable Building Cost Settings", ref settings.EnableBuildCostSettings);
            listing.AddLabeledSlider(null, ref settings.SteelCost, 0, 1000f, steel.ToTitleCase(), null, 1f, true, settings.SteelCost + " " + steel, LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.ComponentIndustrialCost, 0, 100f, component.ToTitleCase(), null, 1f, true, settings.ComponentIndustrialCost + " " + component, LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.PlasteelCost, 0, 1000f, plasteel.ToTitleCase(), null, 1f, true, settings.PlasteelCost + " " + plasteel, LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.ComponentSpacerCost, 0, 100f, advancedComponent.ToTitleCase(), null, 1f, true, settings.ComponentSpacerCost + " " + advancedComponent, LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.UraniumCost, 0, 100f, uranium.ToTitleCase(), null, 1f, true, settings.UraniumCost + " " + uranium, LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.WorkToBuild, 0, 600000f, workToBuild.ToTitleCase(), null, 60f, true, Mathf.CeilToInt(settings.WorkToBuild / 60).ToString() + " " + workToBuild, LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
            listing.AddLabelLine("Inglix.Research_Cost".Translate());
            listing.AddLabeledSlider(null, ref settings.ResearchBaseCostBiosculpting, 0, 15000f, biosculptingResearchName.CapitalizeFirst(), null, 100f, true, settings.ResearchBaseCostBiosculpting.ToString(), LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.ResearchBaseCostBioregeneration, 0, 15000f, bioregenerationResearchName.CapitalizeFirst(), null, 100f, true, settings.ResearchBaseCostBioregeneration.ToString(), LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);

            Rect rect2 = listing.GetRect(210f);
            Listing_Standard listing2 = new Listing_Standard();
            listing2.ColumnWidth = ((rect2.width - 17) / 2);
            listing2.Begin(rect2);
            listing2.AddLabeledRadioList<TechLevel>("Inglix.Biosculpting_Research".Translate(), techLevels, ref settings.ResearchLevelBiosculpting);
            GUI.contentColor = settings.ResearchLevelBiosculpting > TechLevel.Industrial ? Color.grey : Color.white;
            listing2.AddLabeledCheckbox("Inglix.Require_HiTechBench".Translate(), ref settings.ResearchBiosculptingHiTechResearchBench);
            GUI.contentColor = settings.ResearchBiosculptingHiTechResearchBench && settings.ResearchLevelBiosculpting < TechLevel.Ultra ? Color.white : Color.gray;
            listing2.AddLabeledCheckbox("Inglix.Require_MultiAnalyzer".Translate(), ref settings.ResearchBiosculptingMultiAnalyzer);
            GUI.contentColor = Color.white;
            listing2.NewColumn();
            listing2.AddLabeledRadioList<TechLevel>("Inglix.Bioregeneration_Research".Translate(), techLevels, ref settings.ResearchLevelBioregeneration);
            GUI.contentColor = settings.ResearchLevelBioregeneration > TechLevel.Industrial ? Color.grey : Color.white;
            listing2.AddLabeledCheckbox("Inglix.Require_HiTechBench".Translate(), ref settings.ResearchBioregenerationHiTechResearchBench);
            GUI.contentColor = settings.ResearchBiosculptingHiTechResearchBench && settings.ResearchLevelBioregeneration < TechLevel.Ultra ? Color.white : Color.gray;
            listing2.AddLabeledCheckbox("Inglix.Require_MultiAnalyzer".Translate(), ref settings.ResearchBioregenerationMultiAnalyzer);
            GUI.contentColor = Color.white;
            listing2.End();

            // Enforce logical inheritence of requirements
            {
                if (settings.ResearchLevelBiosculpting > settings.ResearchLevelBioregeneration)
                {
                    settings.ResearchLevelBioregeneration = settings.ResearchLevelBiosculpting;
                }
                if (settings.ResearchBiosculptingHiTechResearchBench)
                {
                    settings.ResearchBioregenerationHiTechResearchBench = true;
                }
                if (settings.ResearchBiosculptingMultiAnalyzer)
                {
                    settings.ResearchBioregenerationMultiAnalyzer = true;
                }
                if (!settings.ResearchBiosculptingHiTechResearchBench)
                {
                    settings.ResearchBiosculptingMultiAnalyzer = false;
                }
                if (!settings.ResearchBioregenerationHiTechResearchBench)
                {
                    settings.ResearchBioregenerationMultiAnalyzer = false;
                }
                if (settings.ResearchLevelBiosculpting > TechLevel.Industrial)
                {
                    settings.ResearchBiosculptingHiTechResearchBench = true;
                }
                if (settings.ResearchLevelBioregeneration > TechLevel.Industrial)
                {
                    settings.ResearchBioregenerationHiTechResearchBench = true;
                }
                if (settings.ResearchLevelBiosculpting > TechLevel.Spacer)
                {
                    settings.ResearchBiosculptingMultiAnalyzer = true;
                }
                if (settings.ResearchLevelBioregeneration > TechLevel.Spacer)
                {
                    settings.ResearchBioregenerationMultiAnalyzer = true;
                }
            }

            listing.End();
            Widgets.EndScrollView();

            Rect buttonsRect = canvas.BottomPartPixels(115f);
            buttonsRect.height = 47f;
            Listing_Standard lineListing = new Listing_Standard();
            lineListing.Begin(buttonsRect);
            lineListing.AddHorizontalLine(ListingStandardHelper.Gap);
            lineListing.End();
            buttonsRect.y += 12f;
            Listing_Standard footerListing = new Listing_Standard();
            footerListing.ColumnWidth = ((canvas.width - 30) / 2) - 2;
            footerListing.Begin(buttonsRect);
            
            footerListing.AddLabeledCheckbox("Inglix.Use_Quadrums".Translate(), ref settings.UseQuadrumsForDuration);
            footerListing.NewColumn();
            footerListing.AddLabeledCheckbox("Inglix.Use_Hours".Translate(), ref settings.UseHoursForDuration);
            footerListing.End();
            buttonsRect.y += 46f;
            buttonsRect.width = (canvas.width * 0.3f);
            if (Widgets.ButtonText(buttonsRect, "Inglix.Apply_Custom_Values".Translate()))
            {
                ApplySettings();
            }
            buttonsRect.x += canvas.width * 0.35f;
            if (Widgets.ButtonText(buttonsRect, "Inglix.Apply_Recommended_Values".Translate()))
            {
                settings.MedicCycleDays = FasterBiosculpterPod_Settings.RecommendedMedicCycleDays;

                settings.BioregenerationCycleDays = FasterBiosculpterPod_Settings.RecommendedBioregenerationCycleDays;
                settings.BioregenerationCycleMedicineUltratech = FasterBiosculpterPod_Settings.RecommendedBioregenerationCycleMedicineUltratech;

                settings.AgeReversalCycleDays = FasterBiosculpterPod_Settings.RecommendedAgeReversalCycleDays;
                settings.AgeReversalDays = FasterBiosculpterPod_Settings.RecommendedAgeReversalTicks / 60000;

                settings.PleasureCycleDays = FasterBiosculpterPod_Settings.RecommendedPleasureCycleDays;
                settings.PleasureCycleMoodDays = FasterBiosculpterPod_Settings.RecommendedPleasureCycleMoodDays;
                settings.PleasureCycleMoodEffect = FasterBiosculpterPod_Settings.RecommendedPleasureCycleMoodEffect;

                settings.NutritionRequired = FasterBiosculpterPod_Settings.RecommendedNutritionRequired;

                settings.BiotuningDurationDays = FasterBiosculpterPod_Settings.RecommendedBiotuningDurationTicks / 60000;
                settings.BiotunedCycleSpeedFactor = FasterBiosculpterPod_Settings.RecommendedBiotunedCycleSpeedFactor;

                settings.PowerConsumption = FasterBiosculpterPod_Settings.RecommendedPowerConsumption;
                settings.StandbyConsumption = FasterBiosculpterPod_Settings.RecommendedStandbyConsumption;

                settings.EnableBuildCostSettings = false;
                settings.SteelCost = FasterBiosculpterPod_Settings.RecommendedSteelCost;
                settings.ComponentIndustrialCost = FasterBiosculpterPod_Settings.RecommendedComponentIndustrialCost;
                settings.PlasteelCost = FasterBiosculpterPod_Settings.RecommendedPlasteelCost;
                settings.ComponentSpacerCost = FasterBiosculpterPod_Settings.RecommendedComponentSpacerCost;
                settings.UraniumCost = FasterBiosculpterPod_Settings.RecommendedUraniumCost;
                settings.WorkToBuild = FasterBiosculpterPod_Settings.RecommendedWorkToBuild;

                settings.ResearchBaseCostBiosculpting = FasterBiosculpterPod_Settings.RecommendedResearchBaseCostBiosculpting;
                settings.ResearchBaseCostBioregeneration = FasterBiosculpterPod_Settings.RecommendedResearchBaseCostBioregeneration;

                settings.ResearchLevelBiosculpting = FasterBiosculpterPod_Settings.RecommendedResearchLevelBiosculpting;
                settings.ResearchLevelBioregeneration = FasterBiosculpterPod_Settings.RecommendedResearchLevelBioregeneration;

                settings.ResearchBiosculptingHiTechResearchBench = FasterBiosculpterPod_Settings.RecommendedResearchBiosculptingHiTechResearchBench;
                settings.ResearchBiosculptingMultiAnalyzer = FasterBiosculpterPod_Settings.RecommendedResearchBiosculptingMultiAnalyzer;
                settings.ResearchBioregenerationHiTechResearchBench = FasterBiosculpterPod_Settings.RecommendedResearchBioregenerationHiTechResearchBench;
                settings.ResearchBioregenerationMultiAnalyzer = FasterBiosculpterPod_Settings.RecommendedResearchBioregenerationMultiAnalyzer;

                ApplySettings();
            }
            buttonsRect.x += canvas.width * 0.35f;
            if (Widgets.ButtonText(buttonsRect, "Inglix.Apply_Vanilla_Values".Translate()))
            {
                settings.MedicCycleDays = FasterBiosculpterPod_Settings.VanillaMedicCycleDays;

                settings.BioregenerationCycleDays = FasterBiosculpterPod_Settings.VanillaBioregenerationCycleDays;
                settings.BioregenerationCycleMedicineUltratech = FasterBiosculpterPod_Settings.VanillaBioregenerationCycleMedicineUltratech;

                settings.AgeReversalCycleDays = FasterBiosculpterPod_Settings.VanillaAgeReversalCycleDays;
                settings.AgeReversalDays = FasterBiosculpterPod_Settings.VanillaAgeReversalTicks / 60000;

                settings.PleasureCycleDays = FasterBiosculpterPod_Settings.VanillaPleasureCycleDays;
                settings.PleasureCycleMoodDays = FasterBiosculpterPod_Settings.VanillaPleasureCycleMoodDays;
                settings.PleasureCycleMoodEffect = FasterBiosculpterPod_Settings.VanillaPleasureCycleMoodEffect;

                settings.NutritionRequired = FasterBiosculpterPod_Settings.VanillaNutritionRequired;

                settings.BiotuningDurationDays = FasterBiosculpterPod_Settings.VanillaBiotuningDurationTicks / 60000;
                settings.BiotunedCycleSpeedFactor = FasterBiosculpterPod_Settings.VanillaBiotunedCycleSpeedFactor;

                settings.PowerConsumption = FasterBiosculpterPod_Settings.VanillaPowerConsumption;
                settings.StandbyConsumption = FasterBiosculpterPod_Settings.VanillaStandbyConsumption;

                settings.EnableBuildCostSettings = false;
                settings.SteelCost = FasterBiosculpterPod_Settings.VanillaSteelCost;
                settings.ComponentIndustrialCost = FasterBiosculpterPod_Settings.VanillaComponentIndustrialCost;
                settings.PlasteelCost = FasterBiosculpterPod_Settings.VanillaPlasteelCost;
                settings.ComponentSpacerCost = FasterBiosculpterPod_Settings.VanillaComponentSpacerCost;
                settings.UraniumCost = FasterBiosculpterPod_Settings.VanillaUraniumCost;
                settings.WorkToBuild = FasterBiosculpterPod_Settings.VanillaWorkToBuild;

                settings.ResearchBaseCostBiosculpting = FasterBiosculpterPod_Settings.VanillaResearchBaseCostBiosculpting;
                settings.ResearchBaseCostBioregeneration = FasterBiosculpterPod_Settings.VanillaResearchBaseCostBioregeneration;

                settings.ResearchLevelBiosculpting = FasterBiosculpterPod_Settings.VanillaResearchLevelBiosculpting;
                settings.ResearchLevelBioregeneration = FasterBiosculpterPod_Settings.VanillaResearchLevelBioregeneration;

                settings.ResearchBiosculptingHiTechResearchBench = FasterBiosculpterPod_Settings.VanillaResearchBiosculptingHiTechResearchBench;
                settings.ResearchBiosculptingMultiAnalyzer = FasterBiosculpterPod_Settings.VanillaResearchBiosculptingMultiAnalyzer;
                settings.ResearchBioregenerationHiTechResearchBench = FasterBiosculpterPod_Settings.VanillaResearchBioregenerationHiTechResearchBench;
                settings.ResearchBioregenerationMultiAnalyzer = FasterBiosculpterPod_Settings.VanillaResearchBioregenerationMultiAnalyzer;

                ApplySettings();
            }
            buttonsRect.y += 47f;
            buttonsRect.x = 0f;
            buttonsRect.width = canvas.width;
            GUI.contentColor = Color.gray;
            Widgets.Label(buttonsRect, "Inglix.Installed_Version".Translate() + ": " + modVersion);
            GUI.contentColor = Color.white;

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
            harmony.UnpatchAll("Inglix.FasterBiosculpterPod");
            harmony.PatchAll();
            if (LoadedModManager.RunningModsListForReading.Find(mod => mod.PackageId.EqualsIgnoreCase("Troopersmith1.AgeMatters")) != null)
            {
                Log.Warning("Age Matters mod adds a custom version of CompProperties_BiosculpterPod_AgeReversalCycle instead of patching the original. In order to apply settings for the age reversal cycle, the durationDays field must be updated using reflection, and a transpiler must be run against their custom CycleCompleted method.");
                UpdateFieldUsingReflection("CompProperties_BiosculpterPod_AgeReversalCycle", "durationDays", settings.AgeReversalCycleDays);
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
                // This is now handled by CompProperties_BiosculpterPod_AgeReversalCycle.Description; see new transpiler TranspileDescription
                //(DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_AgeReversalCycle)) as CompProperties_BiosculpterPod_AgeReversalCycle).description = "Reverse " + ConvertDaysToTicks(settings.AgeReversalDays).ToStringTicksToPeriodVeryVerbose(settings.UseQuadrumsForDuration, settings.UseHoursForDuration) + " of aging.";
            }


            List<ThingDefCountClass> extraRequiredIngredients = new List<ThingDefCountClass>();
            if (LoadedModManager.RunningModsListForReading.Find(mod => mod.PackageId.EqualsIgnoreCase("sambucher.selectivebioregeneration")) != null)
            {
                (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_HealingCycle) && typeof(Selective_Bioregeneration.CompBiosculpterPod_TargetedMedicCycle).IsAssignableFrom(x.compClass)) as CompProperties_BiosculpterPod_HealingCycle).durationDays = settings.MedicCycleDays;
                (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_HealingCycle) && typeof(Selective_Bioregeneration.CompBiosculpterPod_TargetedRegenerationCycle).IsAssignableFrom(x.compClass)) as CompProperties_BiosculpterPod_HealingCycle).durationDays = settings.BioregenerationCycleDays;
                if (settings.BioregenerationCycleMedicineUltratech > 0f)
                {
                    ThingDefCountClass ultratechMedicine = new ThingDefCountClass(ThingDefOf.MedicineUltratech, (int)settings.BioregenerationCycleMedicineUltratech);
                    extraRequiredIngredients.Add(ultratechMedicine);
                }
                (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_HealingCycle) && typeof(Selective_Bioregeneration.CompBiosculpterPod_TargetedRegenerationCycle).IsAssignableFrom(x.compClass)) as CompProperties_BiosculpterPod_HealingCycle).extraRequiredIngredients = extraRequiredIngredients;
            }
            else
            {
                (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_HealingCycle) && typeof(CompBiosculpterPod_MedicCycle).IsAssignableFrom(x.compClass)) as CompProperties_BiosculpterPod_HealingCycle).durationDays = settings.MedicCycleDays;
                (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_HealingCycle) && typeof(CompBiosculpterPod_RegenerationCycle).IsAssignableFrom(x.compClass)) as CompProperties_BiosculpterPod_HealingCycle).durationDays = settings.BioregenerationCycleDays;
                if (settings.BioregenerationCycleMedicineUltratech > 0f)
                {
                    ThingDefCountClass ultratechMedicine = new ThingDefCountClass(ThingDefOf.MedicineUltratech, (int)settings.BioregenerationCycleMedicineUltratech);
                    extraRequiredIngredients.Add(ultratechMedicine);
                }
                (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_HealingCycle) && typeof(CompBiosculpterPod_RegenerationCycle).IsAssignableFrom(x.compClass)) as CompProperties_BiosculpterPod_HealingCycle).extraRequiredIngredients = extraRequiredIngredients;
            }

            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_PleasureCycle)) as CompProperties_BiosculpterPod_PleasureCycle).durationDays = settings.PleasureCycleDays;
            DefDatabase<ThoughtDef>.GetNamed("BiosculpterPleasure", true).durationDays = settings.PleasureCycleMoodDays;
            DefDatabase<ThoughtDef>.GetNamed("BiosculpterPleasure", true).stages[0].baseMoodEffect = settings.PleasureCycleMoodEffect;

            CompProperties_Power power = (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_Power)) as CompProperties_Power);
            Type powerType = typeof(CompProperties_Power);
            FieldInfo basePowerConsumptionField = powerType.GetField("basePowerConsumption", BindingFlags.NonPublic | BindingFlags.Instance);
            basePowerConsumptionField.SetValue(power, settings.PowerConsumption);
            //(DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_Power)) as CompProperties_Power).basePowerConsumption = settings.PowerConsumption;
            
            // They replaced CompProperties_BiosculpterPod.powerConsumptionStandby with CompProperties_Power.idlePowerDraw
            //(DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod)) as CompProperties_BiosculpterPod).powerConsumptionStandby = settings.StandbyConsumption;
            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_Power)) as CompProperties_Power).idlePowerDraw = settings.StandbyConsumption;

            if (settings.BiotuningDurationDays > 0)
                (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true)).description = "Inglix.Biosculpter_Description".Translate(settings.BiotuningDurationDays);
            else
                (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true)).description = "Inglix.Biosculpter_Description_No_Biotuning".Translate();

            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod)) as CompProperties_BiosculpterPod).biotunedCycleSpeedFactor = settings.BiotunedCycleSpeedFactor;

            if (settings.EnableBuildCostSettings)
            {
                DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).costList.Clear();
                UpdateCostListItem(settings.SteelCost, ThingDefOf.Steel);
                UpdateCostListItem(settings.ComponentIndustrialCost, ThingDefOf.ComponentIndustrial);
                UpdateCostListItem(settings.PlasteelCost, ThingDefOf.Plasteel);
                UpdateCostListItem(settings.ComponentSpacerCost, ThingDefOf.ComponentSpacer);
                UpdateCostListItem(settings.UraniumCost, ThingDefOf.Uranium);
                DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).statBases.Find(x => x.stat == StatDefOf.WorkToBuild).value = settings.WorkToBuild;
            }
            else
            {
                RestoreInitialCostList();
            }

            ResearchProjectDef biosculpting = DefDatabase<ResearchProjectDef>.GetNamed("Biosculpting", true);
            biosculpting.baseCost = settings.ResearchBaseCostBiosculpting;
            biosculpting.techLevel = settings.ResearchLevelBiosculpting;
            UpdateResearchPrerequisites(settings.ResearchBiosculptingHiTechResearchBench, settings.ResearchBiosculptingMultiAnalyzer, biosculpting);
            ResearchProjectDef bioregeneration = DefDatabase<ResearchProjectDef>.GetNamed("Bioregeneration", true);
            bioregeneration.baseCost = settings.ResearchBaseCostBioregeneration;
            bioregeneration.techLevel = settings.ResearchLevelBioregeneration;
            UpdateResearchPrerequisites(settings.ResearchBioregenerationHiTechResearchBench, settings.ResearchBioregenerationMultiAnalyzer, bioregeneration);
            RegenerateResearchTree();
        }

        private static void UpdateResearchPrerequisites(bool hiTechResearchBench, bool multiAnalyzer, ResearchProjectDef researchProject)
        {
            if (hiTechResearchBench)
            {
                if (researchProject.requiredResearchBuilding == null)
                {
                    researchProject.requiredResearchBuilding = ThingDef.Named("HiTechResearchBench");
                }
            }
            else
            {
                researchProject.requiredResearchBuilding = null;
            }
            if (multiAnalyzer)
            {
                if (researchProject.requiredResearchFacilities == null)
                {
                    researchProject.requiredResearchFacilities = new List<ThingDef>();
                }
                if (!researchProject.requiredResearchFacilities.Contains(ThingDef.Named("MultiAnalyzer")))
                {
                    researchProject.requiredResearchFacilities.Add(ThingDef.Named("MultiAnalyzer"));
                }
                if (!researchProject.prerequisites.Contains(ResearchProjectDef.Named("MultiAnalyzer")))
                {
                    researchProject.prerequisites.Add(ResearchProjectDef.Named("MultiAnalyzer"));
                }
            }
            else
            {
                if (!researchProject.requiredResearchFacilities.NullOrEmpty() && researchProject.requiredResearchFacilities.Contains(ThingDef.Named("MultiAnalyzer")))
                {
                    researchProject.requiredResearchFacilities.Remove(ThingDef.Named("MultiAnalyzer"));
                }
                if (researchProject.prerequisites.Contains(ResearchProjectDef.Named("MultiAnalyzer")))
                {
                    researchProject.prerequisites.Remove(ResearchProjectDef.Named("MultiAnalyzer"));
                }
            }
        }

        private static void RegenerateResearchTree()
        {
            if (LoadedModManager.RunningModsListForReading.Find(mod => mod.PackageId.EqualsIgnoreCase("VinaLx.ResearchPalForked")) != null)
            {
                Type tree = Type.GetType("ResearchPal.Tree,ResearchTree");
                MethodInfo resetLayout = tree.GetMethod("ResetLayout");
                var task = Task.Run(() => resetLayout.Invoke(null, null));
            }
        }

        private static void UpdateCostListItem(float setting, ThingDef thingDef)
        {
            if (setting > 0f)
            {
                if (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).costList.Exists(x => x.thingDef == thingDef))
                {
                    DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).costList.Find(x => x.thingDef == thingDef).count = (int)setting;
                }
                else
                {
                    DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).costList.Add(new ThingDefCountClass(thingDef, (int)setting));
                }
            }
            else
            {
                if (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).costList.Exists(x => x.thingDef == thingDef))
                {
                    DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).costList.RemoveAll(x => x.thingDef == thingDef);
                }
            }
        }

        private static void RestoreInitialCostList()
        {
            DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).costList.Clear();
            DeepCopyCostList(settings.InitialCostList, out DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).costList);
            DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).statBases.Find(x => x.stat == StatDefOf.WorkToBuild).value = settings.InitialWorkToBuild;
        }

        public static void DeepCopyCostList(List<ThingDefCountClass> originalList, out List<ThingDefCountClass> newList)
        {
            newList = new List<ThingDefCountClass>();
            foreach(ThingDefCountClass oldCost in originalList)
            {
                ThingDefCountClass newCost = new ThingDefCountClass(oldCost.thingDef, oldCost.count);
                newList.Add(newCost);
            }
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
            FasterBiosculpterPod_Mod.DeepCopyCostList(DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).costList, out FasterBiosculpterPod_Mod.settings.InitialCostList);
            FasterBiosculpterPod_Mod.settings.InitialWorkToBuild = DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).statBases.Find(x => x.stat == StatDefOf.WorkToBuild).value;
            FasterBiosculpterPod_Mod.ApplySettings();
        }
    }

    //[HarmonyPatch(typeof(CompBiosculpterPod))]
    //[HarmonyPatch(nameof(CompBiosculpterPod.SetBiotuned))]
    //class PatchSetBiotuned
    //{
    //    static bool Prefix(CompBiosculpterPod __instance, Pawn newBiotunedTo)
    //    {
    //        FieldInfo biotunedTo = __instance.GetType().GetField("biotunedTo", BindingFlags.NonPublic | BindingFlags.Instance);
    //        FieldInfo autoAgeReversal = __instance.GetType().GetField("autoAgeReversal", BindingFlags.NonPublic | BindingFlags.Instance);
    //        FieldInfo cachedBiotunedPods = __instance.GetType().GetField("cachedBiotunedPods", BindingFlags.NonPublic | BindingFlags.Static);
    //        FieldInfo biotunedCountdownTicks = __instance.GetType().GetField("biotunedCountdownTicks", BindingFlags.NonPublic | BindingFlags.Instance);

    //        if (newBiotunedTo != biotunedTo.GetValue(__instance))
    //        {
    //            autoAgeReversal.SetValue(__instance, false);
    //        }
    //        if (biotunedTo.GetValue(__instance) != null && ((Dictionary<Pawn, List<CompBiosculpterPod>>)cachedBiotunedPods.GetValue(null)).ContainsKey((Pawn)biotunedTo.GetValue(__instance)))
    //        {
    //            //cachedBiotunedPods[biotunedTo].Remove(this);
    //            ((Dictionary<Pawn, List<CompBiosculpterPod>>)cachedBiotunedPods.GetValue(null))[(Pawn)biotunedTo.GetValue(__instance)].Remove(__instance);
    //        }
    //        if (newBiotunedTo != null && !((Dictionary<Pawn, List<CompBiosculpterPod>>)cachedBiotunedPods.GetValue(null)).ContainsKey(newBiotunedTo))
    //        {
    //            ((Dictionary<Pawn, List<CompBiosculpterPod>>)cachedBiotunedPods.GetValue(null))[newBiotunedTo] = new List<CompBiosculpterPod>();
    //        }
    //        if (newBiotunedTo != null && !((Dictionary<Pawn, List<CompBiosculpterPod>>)cachedBiotunedPods.GetValue(null))[newBiotunedTo].Contains(__instance))
    //        {
    //            ((Dictionary<Pawn, List<CompBiosculpterPod>>)cachedBiotunedPods.GetValue(null))[newBiotunedTo].Add(__instance);
    //        }
    //        if (newBiotunedTo != null && newBiotunedTo != biotunedTo.GetValue(__instance))
    //        {
    //            biotunedCountdownTicks.SetValue(__instance, FasterBiosculpterPod_Mod.ConvertDaysToTicks(FasterBiosculpterPod_Mod.settings.BiotuningDurationDays));
    //        }
    //        biotunedTo.SetValue(__instance, newBiotunedTo);

    //        return false;

    //        //if (newBiotunedTo == biotunedTo.GetValue(__instance))
    //        //{
    //        //    return false;
    //        //}
    //        //return true;
    //    }
    //}

    [HarmonyPatch]
    class TranspileNutritionRequired
    {
        [HarmonyTargetMethods]
        static IEnumerable<MethodBase> FindMethods()
        {
            yield return AccessTools.Method(typeof(CompBiosculpterPod), "get_RequiredNutritionRemaining");
            yield return AccessTools.Method(typeof(CompBiosculpterPod), "CompInspectStringExtra");
            yield return AccessTools.Method(typeof(CompBiosculpterPod), "<CompGetGizmosExtra>b__88_9");
            yield return AccessTools.Method(typeof(CompBiosculpterPod).GetNestedType("<CompGetGizmosExtra>d__88", BindingFlags.NonPublic), "MoveNext"); // Disables the dev gizmo "fill nutrition and cycle requirements" when configured amount of nutrition is loaded
            yield return AccessTools.Method(typeof(CompBiosculpterPod), "LiquifyNutrition");
        }

        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionList = instructions.ToList();
            for (var i = 0; i < instructionList.Count; i++)
            {
                if (instructionList[i].opcode == OpCodes.Ldc_R4 && (Single)instructionList[i].operand == FasterBiosculpterPod_Settings.VanillaNutritionRequired)
                {
                    instructionList[i].operand = LoadedModManager.GetMod<FasterBiosculpterPod_Mod>().GetSettings<FasterBiosculpterPod_Settings>().NutritionRequired;
                    break;
                }
            }

            return instructionList.AsEnumerable();
        }
    }

    [HarmonyPatch(typeof(CompBiosculpterPod), nameof(CompBiosculpterPod.SetBiotuned))]
    class TranspileSetBiotuned
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
                if (instructionList[i].opcode == OpCodes.Ldc_R4 && (Single)instructionList[i].operand == (Single)FasterBiosculpterPod_Settings.VanillaAgeReversalTicks)
                {
                    instructionList[i].operand = (Single)FasterBiosculpterPod_Mod.ConvertDaysToTicks(LoadedModManager.GetMod<FasterBiosculpterPod_Mod>().GetSettings<FasterBiosculpterPod_Settings>().AgeReversalDays);
                    break;
                }
            }

            return instructionList.AsEnumerable();
        }
    }

    [HarmonyPatch(typeof(CompBiosculpterPod_AgeReversalCycle), nameof(CompBiosculpterPod_AgeReversalCycle.Description))]
    class TranspileDescription
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
