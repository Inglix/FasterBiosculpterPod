using RimWorld;
using System.Collections.Generic;
using Verse;

namespace FasterBiosculpterPod
{
    [HotSwappable]
    public class Settings : ModSettings
    {
        public float MedicCycleDays = Constants.VanillaMedicCycleDays;
        public float BioregenerationCycleDays = Constants.VanillaBioregenerationCycleDays;
        public float BioregenerationCycleMedicineUltratech = Constants.VanillaBioregenerationCycleMedicineUltratech;
        public float AgeReversalCycleDays = Constants.VanillaAgeReversalCycleDays;
        public float AgeReversalTicks = Constants.VanillaAgeReversalTicks;
        public float AgeReversalDays = Constants.VanillaAgeReversalTicks / Constants.TicksPerDay;
        public float PleasureCycleDays = Constants.VanillaPleasureCycleDays;
        public float PleasureCycleMoodDays = Constants.VanillaPleasureCycleMoodDays;
        public float PleasureCycleMoodEffect = Constants.VanillaPleasureCycleMoodEffect;

        public float NutritionRequired = Constants.VanillaNutritionRequired;

        public float BiotuningDurationTicks = Constants.VanillaBiotuningDurationTicks;
        public float BiotuningDurationDays = Constants.VanillaBiotuningDurationTicks / Constants.TicksPerDay;
        public float BiotunedCycleSpeedFactor = Constants.VanillaBiotunedCycleSpeedFactor;

        public float PowerConsumption = Constants.VanillaPowerConsumption;
        public float StandbyConsumption = Constants.VanillaStandbyConsumption;

        public bool EnableBuildCostSettings = false;
        public List<ThingDefCountClass> InitialCostList;
        public float InitialWorkToBuild;
        public float SteelCost = Constants.VanillaSteelCost;
        public float ComponentIndustrialCost = Constants.VanillaComponentIndustrialCost;
        public float PlasteelCost = Constants.VanillaPlasteelCost;
        public float ComponentSpacerCost = Constants.VanillaComponentSpacerCost;
        public float UraniumCost = Constants.VanillaUraniumCost;
        public float WorkToBuild = Constants.VanillaWorkToBuild;

        public float ResearchBaseCostBiosculpting = Constants.VanillaResearchBaseCostBiosculpting;
        public float ResearchBaseCostBioregeneration = Constants.VanillaResearchBaseCostBioregeneration;
        public TechLevel ResearchLevelBiosculpting = Constants.VanillaResearchLevelBiosculpting;
        public TechLevel ResearchLevelBioregeneration = Constants.VanillaResearchLevelBioregeneration;
        public bool ResearchBiosculptingHiTechResearchBench = Constants.VanillaResearchBiosculptingHiTechResearchBench;
        public bool ResearchBiosculptingMultiAnalyzer = Constants.VanillaResearchBiosculptingMultiAnalyzer;
        public bool ResearchBioregenerationHiTechResearchBench = Constants.VanillaResearchBioregenerationHiTechResearchBench;
        public bool ResearchBioregenerationMultiAnalyzer = Constants.VanillaResearchBioregenerationMultiAnalyzer;

        public bool UseQuadrumsForDuration = true;
        public bool UseHoursForDuration = true;
        
        public override void ExposeData()
        {
            Scribe_Values.Look(ref MedicCycleDays, "medicCycleDays", Constants.VanillaMedicCycleDays);

            Scribe_Values.Look(ref BioregenerationCycleDays, "bioregenerationCycleDays", Constants.VanillaBioregenerationCycleDays);
            Scribe_Values.Look(ref BioregenerationCycleMedicineUltratech, "bioregenerationCycleMedicineUltratech", Constants.VanillaBioregenerationCycleMedicineUltratech);

            Scribe_Values.Look(ref AgeReversalCycleDays, "ageReversalCycleDays", Constants.VanillaAgeReversalCycleDays);
            Scribe_Values.Look(ref AgeReversalTicks, "ageReversalTicks", Constants.VanillaAgeReversalTicks); // Deprecated
            Scribe_Values.Look(ref AgeReversalDays, "ageReversalDays", AgeReversalTicks / Constants.TicksPerDay);

            Scribe_Values.Look(ref PleasureCycleDays, "pleasureCycleDays", Constants.VanillaPleasureCycleDays);
            Scribe_Values.Look(ref PleasureCycleMoodDays, "pleasureCycleMoodDays", Constants.VanillaPleasureCycleMoodDays);
            Scribe_Values.Look(ref PleasureCycleMoodEffect, "pleasureCycleMoodEffect", Constants.VanillaPleasureCycleMoodEffect);

            Scribe_Values.Look(ref NutritionRequired, "nutritionRequired", Constants.VanillaNutritionRequired);

            Scribe_Values.Look(ref BiotuningDurationTicks, "biotuningDurationTicks", Constants.VanillaBiotuningDurationTicks); // Deprecated
            Scribe_Values.Look(ref BiotuningDurationDays, "biotuningDurationDays", BiotuningDurationTicks / Constants.TicksPerDay);
            Scribe_Values.Look(ref BiotunedCycleSpeedFactor, "biotunedCycleSpeedFactor", Constants.VanillaBiotunedCycleSpeedFactor);

            Scribe_Values.Look(ref PowerConsumption, "powerConsumption", Constants.VanillaPowerConsumption);
            Scribe_Values.Look(ref StandbyConsumption, "standbyConsumption", Constants.VanillaStandbyConsumption);

            Scribe_Values.Look(ref EnableBuildCostSettings, "enableBuildCostSettings", false);
            Scribe_Values.Look(ref SteelCost, "steelCost", Constants.VanillaSteelCost);
            Scribe_Values.Look(ref ComponentIndustrialCost, "componentIndustrialCost", Constants.VanillaComponentIndustrialCost);
            Scribe_Values.Look(ref PlasteelCost, "plasteelCost", Constants.VanillaPlasteelCost);
            Scribe_Values.Look(ref ComponentSpacerCost, "componentSpacerCost", Constants.VanillaComponentSpacerCost);
            Scribe_Values.Look(ref UraniumCost, "uraniumCost", Constants.VanillaUraniumCost);
            Scribe_Values.Look(ref WorkToBuild, "workToBuild", Constants.VanillaWorkToBuild);

            Scribe_Values.Look(ref ResearchBaseCostBiosculpting, "researchBaseCostBiosculpting", Constants.VanillaResearchBaseCostBiosculpting);
            Scribe_Values.Look(ref ResearchBaseCostBioregeneration, "researchBaseCostBioregeneration", Constants.VanillaResearchBaseCostBioregeneration);

            Scribe_Values.Look(ref ResearchLevelBiosculpting, "researchLevelBiosculpting", Constants.VanillaResearchLevelBiosculpting);
            Scribe_Values.Look(ref ResearchLevelBioregeneration, "researchLevelBioregeneration", Constants.VanillaResearchLevelBioregeneration);

            Scribe_Values.Look(ref ResearchBiosculptingHiTechResearchBench, "researchBiosculptingHiTechResearchBench", Constants.VanillaResearchBiosculptingHiTechResearchBench);
            Scribe_Values.Look(ref ResearchBiosculptingMultiAnalyzer, "researchBiosculptingMultiAnalyzer", Constants.VanillaResearchBiosculptingMultiAnalyzer);
            Scribe_Values.Look(ref ResearchBioregenerationHiTechResearchBench, "researchBioregenerationHiTechResearchBench", Constants.VanillaResearchBioregenerationHiTechResearchBench);
            Scribe_Values.Look(ref ResearchBioregenerationMultiAnalyzer, "researchBioregenerationMultiAnalyzer", Constants.VanillaResearchBioregenerationMultiAnalyzer);

            Scribe_Values.Look(ref UseQuadrumsForDuration, "useQuadrumsForDuration", true);
            Scribe_Values.Look(ref UseHoursForDuration, "useHoursForDuration", true);

            base.ExposeData();
        }
    }
}
