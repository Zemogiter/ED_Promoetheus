using EnhancedDevelopment.Prometheus.Core;
using System.Collections.Generic;
using Verse;

namespace EnhancedDevelopment.Prometheus.Quest
{
    internal static class ResearchHelper
    {
        public static void UpdateQuestStatusResearch()
        {
            if (GameComponent_Prometheus.Instance.Comp_Quest.m_QuestStatus >= 1)
                ResearchHelper.QuestUnlock("Research_ED_Prometheus_AnalyseStrangeSignal");
            if (GameComponent_Prometheus.Instance.Comp_Quest.m_QuestStatus < 4)
                return;
            GameComponent_Prometheus.Instance.Comp_Quest.ShipSystem_Fabrication.UpgradeTo(1);
        }

        public static void QuestUnlock(string researchName)
        {
            ResearchProjectDef named = DefDatabase<ResearchProjectDef>.GetNamed(researchName, true);
            if (named.requiredResearchFacilities == null)
                return;
            ((List<ThingDef>)named.requiredResearchFacilities).Clear();
        }

        public static void QuestComplete(string researchName)
        {
            ResearchHelper.QuestUnlock(researchName);
            Find.ResearchManager.FinishProject(DefDatabase<ResearchProjectDef>.GetNamed(researchName, true), false, (Pawn)null);
        }

        public static bool IsResearched(string researchName)
        {
            return DefDatabase<ResearchProjectDef>.GetNamed(researchName, true).IsFinished;
        }
    }
}
