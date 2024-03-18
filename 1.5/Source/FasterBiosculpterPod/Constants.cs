using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FasterBiosculpterPod
{
    internal static class Constants
    {
        public const float LeftPartPct = 0.0f; // Let the slider take up the entire width of the settings window
        public const float CycleDurationMin = 0f; // Minimum cycle duration is 0 days
        public const float CycleDurationMax = 60f; // Maximum cycle duration is 60 days
        public const float CycleDurationIncrement = 0.1f; // Increment cycle duration by 0.1 day (2.4 hour) increments
        public const float NutritionRequiredIncrement = 0.1f; // Increment nutrition required by 0.1 nutrition increments
        public const float OuterRectHeight = 472f;
        public const float InnerRectWidthOffset = 33f;
        public const float InnerRectHeight = 1365.0f;
        public const float VanillaMedicCycleDays = 6f;
        public const float RecommendedMedicCycleDays = 3f;

        public const int TicksPerDay = 60000;

        public const float VanillaBioregenerationCycleDays = 25f;
        public const float VanillaBioregenerationCycleMedicineUltratech = 2f;
        public const float RecommendedBioregenerationCycleDays = 12.5f;
        public const float RecommendedBioregenerationCycleMedicineUltratech = VanillaBioregenerationCycleMedicineUltratech;

        public const float VanillaAgeReversalCycleDays = 8f;
        public const int VanillaAgeReversalTicks = 3600000;
        public const float VanillaAgeReversalDays = VanillaAgeReversalTicks / 60000;
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

    }
}
