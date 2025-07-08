using RimWorld;
using SettingsHelper;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Verse;

namespace FasterBiosculpterPod
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class HotSwappableAttribute : Attribute
    {
    }

    [HotSwappable]
    public class FasterBiosculpterPod : Mod
    {
#if DEBUG
        private readonly string _modVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
#else
        private readonly string _modVersion = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
#endif
        public static Settings settings;
        private Vector2 scrollPosition;

        public static string glitterworldMedicineLabel;
        public static string biosculptingResearchLabel;
        public static string bioregenerationResearchLabel;
        public static string steelLabel;
        public static string plasteelLabel;
        public static string componentLabel;
        public static string advancedComponentLabel;
        public static string uraniumLabel;
        public static string workToBuildLabel;

        public static Dictionary<String, TechLevel> techLevels;

        public FasterBiosculpterPod(ModContentPack content) : base(content)
        {
            settings = GetSettings<Settings>();
        }

        public override void DoSettingsWindowContents(Rect canvas)
        {
            /*
             * For reference, the canvas height is 584 and the canvas width is 864
             */
            DrawScrollView(canvas);
            DrawFooter(canvas);
            base.DoSettingsWindowContents(canvas);
        }

        private void DrawFooter(Rect canvas)
        {
            Rect footerRect = canvas.BottomPartPixels(115f);
            footerRect.height = 47f;
            Listing_Standard lineListing = new Listing_Standard();
            lineListing.Begin(footerRect);
            lineListing.AddHorizontalLine(ListingStandardHelper.Gap);
            lineListing.End();
            footerRect.y += 12f;
            Listing_Standard footerListing = new Listing_Standard();
            footerListing.ColumnWidth = ((canvas.width - 30) / 2) - 2;
            footerListing.Begin(footerRect);

            footerListing.AddLabeledCheckbox("Inglix.Use_Quadrums".Translate(), ref settings.UseQuadrumsForDuration);
            footerListing.NewColumn();
            footerListing.AddLabeledCheckbox("Inglix.Use_Hours".Translate(), ref settings.UseHoursForDuration);
            footerListing.End();
            footerRect.y += 46f;
            footerRect.width = (canvas.width * 0.3f);
            if (Widgets.ButtonText(footerRect, "Inglix.Apply_Custom_Values".Translate()))
            {
                SettingsUtils.ApplySettings(settings);
            }
            footerRect.x += canvas.width * 0.35f;
            if (Widgets.ButtonText(footerRect, "Inglix.Apply_Recommended_Values".Translate()))
            {
                SettingsUtils.ApplyRecommendedSettings(settings);
            }
            footerRect.x += canvas.width * 0.35f;
            if (Widgets.ButtonText(footerRect, "Inglix.Apply_Vanilla_Values".Translate()))
            {
                SettingsUtils.ApplyVanillaSettings(settings);
            }
            footerRect.y += 47f;
            footerRect.x = 0f;
            footerRect.width = canvas.width;
            GUI.contentColor = Color.gray;
            Widgets.Label(footerRect, "Inglix.Installed_Version".Translate() + ": " + _modVersion);
            GUI.contentColor = Color.white;
        }

        private void DrawScrollView(Rect canvas)
        {
            Rect outRect = canvas.TopPartPixels(Constants.OuterRectHeight);
            Rect rect = new Rect(0f, 0f, outRect.width - Constants.InnerRectWidthOffset, Constants.InnerRectHeight);
            Widgets.BeginScrollView(outRect, ref scrollPosition, rect, true);
            Listing_Standard listing = new Listing_Standard();
            listing.maxOneColumn = true;
            listing.Begin(rect);

            DrawMedicCycleSettings(listing);
            DrawBioregenerationCycleSettings(listing);
            DrawAgeReversalCycleSettings(listing);
            DrawPleasureCycleSettings(listing);
            DrawMiscellaneousSettings(listing);
            DrawBuildingCostSettings(listing);
            DrawResearchCostSettings(listing);
            DrawResearchRequirementSettings(listing);

            listing.End();
            Widgets.EndScrollView();
        }

        private static void EnforceResearchPrerequisites()
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

        private static void DrawResearchRequirementSettings(Listing_Standard listing)
        {
            /* Research Tech Levels & Misc Options */
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
            EnforceResearchPrerequisites();
        }

        private static void DrawResearchCostSettings(Listing_Standard listing)
        {
            listing.AddLabelLine("Inglix.Research_Cost".Translate());
            listing.AddLabeledSlider(null, ref settings.ResearchBaseCostBiosculpting, 0, 15000f, biosculptingResearchLabel.CapitalizeFirst(), null, 100f, true, settings.ResearchBaseCostBiosculpting.ToString(), Constants.LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.ResearchBaseCostBioregeneration, 0, 15000f, bioregenerationResearchLabel.CapitalizeFirst(), null, 100f, true, settings.ResearchBaseCostBioregeneration.ToString(), Constants.LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
        }

        private static void DrawBuildingCostSettings(Listing_Standard listing)
        {
            listing.AddLabelLine("Inglix.Building_Cost".Translate());
            listing.AddLabeledCheckbox("Enable Building Cost Settings", ref settings.EnableBuildCostSettings);
            listing.AddLabeledSlider(null, ref settings.SteelCost, 0, 1000f, steelLabel.ToTitleCase(), null, 1f, true, settings.SteelCost + " " + steelLabel, Constants.LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.ComponentIndustrialCost, 0, 100f, componentLabel.ToTitleCase(), null, 1f, true, settings.ComponentIndustrialCost + " " + componentLabel, Constants.LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.PlasteelCost, 0, 1000f, plasteelLabel.ToTitleCase(), null, 1f, true, settings.PlasteelCost + " " + plasteelLabel, Constants.LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.ComponentSpacerCost, 0, 100f, advancedComponentLabel.ToTitleCase(), null, 1f, true, settings.ComponentSpacerCost + " " + advancedComponentLabel, Constants.LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.UraniumCost, 0, 100f, uraniumLabel.ToTitleCase(), null, 1f, true, settings.UraniumCost + " " + uraniumLabel, Constants.LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.WorkToBuild, 0, 600000f, workToBuildLabel.ToTitleCase(), null, 60f, true, Mathf.CeilToInt(settings.WorkToBuild / 60).ToString() + " " + workToBuildLabel, Constants.LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
        }

        private static void DrawMiscellaneousSettings(Listing_Standard listing)
        {
            listing.AddLabelLine("Inglix.Miscellaneous_Options".Translate());
            listing.AddLabeledSlider(null, ref settings.NutritionRequired, 0f, 60f, "Inglix.Nutrition_Required".Translate(), null, Constants.NutritionRequiredIncrement, true, settings.NutritionRequired.ToString() + " " + "Nutrition".Translate().ToLower(), Constants.LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.BiotuningDurationDays, 0, 840f, "Inglix.Biotuning_Duration".Translate(), null, 1f, true, SettingsUtils.ConvertDaysToTicks(settings.BiotuningDurationDays).ToStringTicksToPeriodVeryVerbose(settings.UseQuadrumsForDuration, settings.UseHoursForDuration), Constants.LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.BiotunedCycleSpeedFactor, 0, 10f, "Inglix.Biotuned_Speed".Translate(), null, 0.05f, true, settings.BiotunedCycleSpeedFactor.ToString("P0"), Constants.LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.PowerConsumption, 0f, 10000f, "Inglix.Power_Consumption".Translate(), null, 25f, true, settings.PowerConsumption.ToString() + " W", Constants.LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.StandbyConsumption, 0f, 10000f, "Inglix.Standby_Consumption".Translate(), null, 25f, true, settings.StandbyConsumption.ToString() + " W", Constants.LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
        }

        private static void DrawPleasureCycleSettings(Listing_Standard listing)
        {
            listing.AddLabelLine("Inglix.Pleasure_Cycle".Translate());
            DrawCycleDurationSlider(listing, ref settings.PleasureCycleDays);
            listing.AddLabeledSlider(null, ref settings.PleasureCycleMoodDays, 0f, 60f, "Inglix.Mood_Duration".Translate(), null, Constants.CycleDurationIncrement, true, SettingsUtils.ConvertDaysToTicks(settings.PleasureCycleMoodDays).ToStringTicksToPeriodVeryVerbose(settings.UseQuadrumsForDuration, settings.UseHoursForDuration), Constants.LeftPartPct);
            listing.AddLabeledSlider(null, ref settings.PleasureCycleMoodEffect, 0f, 100f, "Inglix.Mood_Effect".Translate(), null, 1f, true, "+" + settings.PleasureCycleMoodEffect.ToString() + " " + "Mood".Translate().ToLower(), Constants.LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
        }

        private static void DrawAgeReversalCycleSettings(Listing_Standard listing)
        {
            listing.AddLabelLine("Inglix.Age_Reversal_Cycle".Translate());
            DrawCycleDurationSlider(listing, ref settings.AgeReversalCycleDays);
            listing.AddLabeledSlider(null, ref settings.AgeReversalDays, 0, 840f, "Inglix.Age_Reversed".Translate(), null, 1f, true, SettingsUtils.ConvertDaysToTicks(settings.AgeReversalDays).ToStringTicksToPeriodVeryVerbose(settings.UseQuadrumsForDuration, settings.UseHoursForDuration), Constants.LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
        }

        private static void DrawBioregenerationCycleSettings(Listing_Standard listing)
        {
            listing.AddLabelLine("Inglix.Bioregeneration_Cycle".Translate());
            DrawCycleDurationSlider(listing, ref settings.BioregenerationCycleDays);
            listing.AddLabeledSlider(null, ref settings.BioregenerationCycleMedicineUltratech, 0f, 20f, "Inglix.MedicineUltratech_Required".Translate(), null, 1f, true, settings.BioregenerationCycleMedicineUltratech.ToString() + " " + glitterworldMedicineLabel, Constants.LeftPartPct);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
        }

        private static void DrawMedicCycleSettings(Listing_Standard listing)
        {
            listing.AddLabelLine("Inglix.Medic_Cycle".Translate());
            DrawCycleDurationSlider(listing, ref settings.MedicCycleDays);
            listing.AddHorizontalLine(ListingStandardHelper.Gap);
        }

        private static void DrawCycleDurationSlider(Listing_Standard listing, ref float cycleDuration)
        {
            listing.AddLabeledSlider(null, ref cycleDuration, Constants.CycleDurationMin, Constants.CycleDurationMax, "Inglix.Cycle_Duration".Translate(), null, Constants.CycleDurationIncrement, true, SettingsUtils.ConvertDaysToTicks(cycleDuration).ToStringTicksToPeriodVeryVerbose(settings.UseQuadrumsForDuration, settings.UseHoursForDuration), Constants.LeftPartPct);
        }

        public override string SettingsCategory()
        {
            return "Inglix.Faster_Biosculpter_Pod".Translate();
        }
    }
}