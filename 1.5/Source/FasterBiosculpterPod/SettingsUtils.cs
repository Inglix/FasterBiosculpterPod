using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace FasterBiosculpterPod
{
    [StaticConstructorOnStartup]
    internal static class SettingsUtils
    {
        
        static SettingsUtils()
        {
            FasterBiosculpterPod.glitterworldMedicineLabel = DefDatabase<ThingDef>.GetNamed("MedicineUltratech")?.label ?? "glitterworld medicine";
            FasterBiosculpterPod.biosculptingResearchLabel = DefDatabase<ResearchProjectDef>.GetNamed("Biosculpting")?.label ?? "biosculpting";
            FasterBiosculpterPod.bioregenerationResearchLabel = DefDatabase<ResearchProjectDef>.GetNamed("Bioregeneration")?.label ?? "bioregeneration";
            FasterBiosculpterPod.steelLabel = DefDatabase<ThingDef>.GetNamed("Steel")?.label ?? "steel";
            FasterBiosculpterPod.plasteelLabel = DefDatabase<ThingDef>.GetNamed("Plasteel")?.label ?? "plasteel";
            FasterBiosculpterPod.componentLabel = DefDatabase<ThingDef>.GetNamed("ComponentIndustrial")?.label ?? "component";
            FasterBiosculpterPod.advancedComponentLabel = DefDatabase<ThingDef>.GetNamed("ComponentSpacer")?.label ?? "advanced component";
            FasterBiosculpterPod.uraniumLabel = DefDatabase<ThingDef>.GetNamed("Uranium")?.label ?? "uranium";
            FasterBiosculpterPod.workToBuildLabel = DefDatabase<StatDef>.GetNamed("WorkToBuild")?.label ?? "work to build";

            FasterBiosculpterPod.techLevels = new Dictionary<string, TechLevel>
            {
                { "TechLevel_Industrial".Translate().CapitalizeFirst(), TechLevel.Industrial },
                { "TechLevel_Spacer".Translate().CapitalizeFirst(), TechLevel.Spacer },
                { "TechLevel_Ultra".Translate().CapitalizeFirst(), TechLevel.Ultra }
            };

            DeepCopyCostList(DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).costList, out FasterBiosculpterPod.settings.InitialCostList);
            FasterBiosculpterPod.settings.InitialWorkToBuild = DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).statBases.Find(x => x.stat == StatDefOf.WorkToBuild).value;
            ApplySettings(FasterBiosculpterPod.settings);
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

        internal static void ApplyRecommendedSettings(Settings settings)
        {
            settings.MedicCycleDays = Constants.RecommendedMedicCycleDays;

            settings.BioregenerationCycleDays = Constants.RecommendedBioregenerationCycleDays;
            settings.BioregenerationCycleMedicineUltratech = Constants.RecommendedBioregenerationCycleMedicineUltratech;

            settings.AgeReversalCycleDays = Constants.RecommendedAgeReversalCycleDays;
            settings.AgeReversalDays = Constants.RecommendedAgeReversalTicks / Constants.TicksPerDay;

            settings.PleasureCycleDays = Constants.RecommendedPleasureCycleDays;
            settings.PleasureCycleMoodDays = Constants.RecommendedPleasureCycleMoodDays;
            settings.PleasureCycleMoodEffect = Constants.RecommendedPleasureCycleMoodEffect;

            settings.NutritionRequired = Constants.RecommendedNutritionRequired;

            settings.BiotuningDurationDays = Constants.RecommendedBiotuningDurationTicks / Constants.TicksPerDay;
            settings.BiotunedCycleSpeedFactor = Constants.RecommendedBiotunedCycleSpeedFactor;

            settings.PowerConsumption = Constants.RecommendedPowerConsumption;
            settings.StandbyConsumption = Constants.RecommendedStandbyConsumption;

            settings.EnableBuildCostSettings = false;
            settings.SteelCost = Constants.RecommendedSteelCost;
            settings.ComponentIndustrialCost = Constants.RecommendedComponentIndustrialCost;
            settings.PlasteelCost = Constants.RecommendedPlasteelCost;
            settings.ComponentSpacerCost = Constants.RecommendedComponentSpacerCost;
            settings.UraniumCost = Constants.RecommendedUraniumCost;
            settings.WorkToBuild = Constants.RecommendedWorkToBuild;

            settings.ResearchBaseCostBiosculpting = Constants.RecommendedResearchBaseCostBiosculpting;
            settings.ResearchBaseCostBioregeneration = Constants.RecommendedResearchBaseCostBioregeneration;

            settings.ResearchLevelBiosculpting = Constants.RecommendedResearchLevelBiosculpting;
            settings.ResearchLevelBioregeneration = Constants.RecommendedResearchLevelBioregeneration;

            settings.ResearchBiosculptingHiTechResearchBench = Constants.RecommendedResearchBiosculptingHiTechResearchBench;
            settings.ResearchBiosculptingMultiAnalyzer = Constants.RecommendedResearchBiosculptingMultiAnalyzer;
            settings.ResearchBioregenerationHiTechResearchBench = Constants.RecommendedResearchBioregenerationHiTechResearchBench;
            settings.ResearchBioregenerationMultiAnalyzer = Constants.RecommendedResearchBioregenerationMultiAnalyzer;

            ApplySettings(settings);
        }

        internal static void ApplyVanillaSettings(Settings settings) 
        {
            settings.MedicCycleDays = Constants.VanillaMedicCycleDays;

            settings.BioregenerationCycleDays = Constants.VanillaBioregenerationCycleDays;
            settings.BioregenerationCycleMedicineUltratech = Constants.VanillaBioregenerationCycleMedicineUltratech;

            settings.AgeReversalCycleDays = Constants.VanillaAgeReversalCycleDays;
            settings.AgeReversalDays = Constants.VanillaAgeReversalTicks / Constants.TicksPerDay;

            settings.PleasureCycleDays = Constants.VanillaPleasureCycleDays;
            settings.PleasureCycleMoodDays = Constants.VanillaPleasureCycleMoodDays;
            settings.PleasureCycleMoodEffect = Constants.VanillaPleasureCycleMoodEffect;

            settings.NutritionRequired = Constants.VanillaNutritionRequired;

            settings.BiotuningDurationDays = Constants.VanillaBiotuningDurationTicks / Constants.TicksPerDay;
            settings.BiotunedCycleSpeedFactor = Constants.VanillaBiotunedCycleSpeedFactor;

            settings.PowerConsumption = Constants.VanillaPowerConsumption;
            settings.StandbyConsumption = Constants.VanillaStandbyConsumption;

            settings.EnableBuildCostSettings = false;
            settings.SteelCost = Constants.VanillaSteelCost;
            settings.ComponentIndustrialCost = Constants.VanillaComponentIndustrialCost;
            settings.PlasteelCost = Constants.VanillaPlasteelCost;
            settings.ComponentSpacerCost = Constants.VanillaComponentSpacerCost;
            settings.UraniumCost = Constants.VanillaUraniumCost;
            settings.WorkToBuild = Constants.VanillaWorkToBuild;

            settings.ResearchBaseCostBiosculpting = Constants.VanillaResearchBaseCostBiosculpting;
            settings.ResearchBaseCostBioregeneration = Constants.VanillaResearchBaseCostBioregeneration;

            settings.ResearchLevelBiosculpting = Constants.VanillaResearchLevelBiosculpting;
            settings.ResearchLevelBioregeneration = Constants.VanillaResearchLevelBioregeneration;

            settings.ResearchBiosculptingHiTechResearchBench = Constants.VanillaResearchBiosculptingHiTechResearchBench;
            settings.ResearchBiosculptingMultiAnalyzer = Constants.VanillaResearchBiosculptingMultiAnalyzer;
            settings.ResearchBioregenerationHiTechResearchBench = Constants.VanillaResearchBioregenerationHiTechResearchBench;
            settings.ResearchBioregenerationMultiAnalyzer = Constants.VanillaResearchBioregenerationMultiAnalyzer;

            ApplySettings(settings);
        }

        internal static void ApplySettings(Settings settings)
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
                Type harmonyClassType = typeof(HarmonyPatches.CycleCompletedTranspiler);
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

            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_HealingCycle) && x.compClass == typeof(CompBiosculpterPod_MedicCycle)) as CompProperties_BiosculpterPod_HealingCycle).durationDays = settings.MedicCycleDays;


            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_HealingCycle) && x.compClass == typeof(CompBiosculpterPod_RegenerationCycle)) as CompProperties_BiosculpterPod_HealingCycle).durationDays = settings.BioregenerationCycleDays;

            List<ThingDefCountClass> extraRequiredIngredients = new List<ThingDefCountClass>();
            if (settings.BioregenerationCycleMedicineUltratech > 0f)
            {
                ThingDefCountClass ultratechMedicine = new ThingDefCountClass(ThingDefOf.MedicineUltratech, (int)settings.BioregenerationCycleMedicineUltratech);
                extraRequiredIngredients.Add(ultratechMedicine);
            }
            (DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod_HealingCycle) && x.compClass == typeof(CompBiosculpterPod_RegenerationCycle)) as CompProperties_BiosculpterPod_HealingCycle).extraRequiredIngredients = extraRequiredIngredients;

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
                RestoreInitialCostList(settings);
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

        private static void RestoreInitialCostList(Settings settings)
        {
            DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).costList.Clear();
            DeepCopyCostList(settings.InitialCostList, out DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).costList);
            DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).statBases.Find(x => x.stat == StatDefOf.WorkToBuild).value = settings.InitialWorkToBuild;
        }

        public static void DeepCopyCostList(List<ThingDefCountClass> originalList, out List<ThingDefCountClass> newList)
        {
            newList = new List<ThingDefCountClass>();
            foreach (ThingDefCountClass oldCost in originalList)
            {
                ThingDefCountClass newCost = new ThingDefCountClass(oldCost.thingDef, oldCost.count);
                newList.Add(newCost);
            }
        }

        public static int ConvertDaysToTicks(float days)
        {
            return ((int)(days * 60000f));
        }
    }
}