using EnhancedDevelopment.Prometheus.Fabrication;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Core
{
    internal class GameComponent_Prometheus_Fabrication : GameComponent_BaseClass
    {
        public List<EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment> ThingForDeployment = new List<EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment>();

		[Obsolete]
		public override void ExposeData()
        {
            this.AddNewBuildingsUnderConstruction();
            this.ThingForDeployment.ForEach((Action<EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment>)(x => x.ExposeData()));
        }

		public override int GetTickInterval()
		{
			return 30;
		}

		public override void TickOnInterval()
        {
            EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment thingForDeployment1 = (EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment)GenCollection.FirstOrFallback<EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment>((IEnumerable<M0>)this.ThingForDeployment, (Func<M0, bool>)(t => t.ConstructionInProgress), (M0)null);
            if (thingForDeployment1 != null)
            {
                --thingForDeployment1.WorkRemaining;
                if (thingForDeployment1.WorkRemaining > 0)
                    return;
                thingForDeployment1.ConstructionInProgress = false;
                ++thingForDeployment1.UnitsAvalable;
                thingForDeployment1.WorkRemaining = thingForDeployment1.TotalNeededWork;
                thingForDeployment1.AfterCompletion();
            }
            else if (GenCollection.Any<EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment>(this.ThingForDeployment, (Predicate<M0>)(b => b.UnitsRequestedAditional >= 1)))
            {
                EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment thingForDeployment2 = (EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment)GenCollection.RandomElement<EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment>((IEnumerable<M0>)this.ThingForDeployment.Where<EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment>((Func<EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment, bool>)(b => b.UnitsRequestedAditional >= 1)));
                if (thingForDeployment2 != null && GameComponent_Prometheus.Instance.Comp_Quest.ResourceGetReserveStatus(GameComponent_Prometheus_Quest.EnumResourceType.Power) >= thingForDeployment2.TotalNeededPower && GameComponent_Prometheus.Instance.Comp_Quest.ResourceGetReserveStatus(GameComponent_Prometheus_Quest.EnumResourceType.ResourceUnits) >= thingForDeployment2.TotalNeededResources)
                {
                    thingForDeployment2.ConstructionInProgress = true;
                    --thingForDeployment2.UnitsRequestedAditional;
                    thingForDeployment2.WorkRemaining = thingForDeployment2.TotalNeededWork;
                    GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.ResourceUnits, -thingForDeployment2.TotalNeededResources);
                    GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.Power, -thingForDeployment2.TotalNeededPower);
                }
            }
        }

        public void DoListing(
          Rect rect,
          ref Vector2 scrollPosition,
          ref float viewHeight,
          bool showOnlyActiveThings,
          IntVec3 dropLocation = null,
          Map dropMap = null)
        {
            List<EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment> list = this.ThingForDeployment.Where<EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment>((Func<EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment, bool>)(t => t.ShouldBeShown(showOnlyActiveThings))).ToList<EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment>();
            Rect rect1;
			// ISSUE: explicit constructor call
			((Rect)rect1).((Rect)rect).xMin, ((Rect)rect).yMin, ((Rect)rect).width, ((Rect)rect).height));
            Rect rect2;
            // ISSUE: explicit constructor call
            ((Rect)rect2).(0.0f, 0.0f, ((Rect)rect1).width - 16f, viewHeight);
            Widgets.BeginScrollView(rect1, ref scrollPosition, rect2, true);
            float y = 0.0f;
            for (int index = 0; index < list.Count; ++index)
            {
                Rect rect3 = list[index].DoInterface(0.0f, y, ((Rect)rect2).width, index, dropLocation, dropMap);
                y += ((Rect)rect3).height + 6f;
                Widgets.DrawLineHorizontal(((Rect)rect2).x, y, ((Rect)rect2).width);
            }
            if (((int)Event.current.type) == 8)
                viewHeight = y + 60f;
            Widgets.EndScrollView();
        }

        public void AddNewBuildingsUnderConstruction()
        {
            DefDatabase<ThingDef>.AllDefs.ToList<ThingDef>().ForEach((Action<ThingDef>)(x =>
            {
                CompProperties_Fabricated compProperties = (CompProperties_Fabricated)x.GetCompProperties<CompProperties_Fabricated>();
                if (compProperties == null || ((BuildableDef)x).researchPrerequisites != null && !((IEnumerable<ResearchProjectDef>)((BuildableDef)x).researchPrerequisites).All<ResearchProjectDef>((Func<ResearchProjectDef, bool>)(r => r.IsFinished || string.Equals((string)((Def)r).defName, "Research_ED_Prometheus_Quest_Unlock"))) || GenCollection.Any<EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment>((List<M0>)this.ThingForDeployment, (Predicate<M0>)(b => string.Equals(b.defName, (string)((Def)x).defName))))
                    return;
                this.ThingForDeployment.Add(new EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment((string)((Def)x).defName, (string)((Def)x).label)
                {
                    WorkRemaining = compProperties.RequiredWork,
                    TotalNeededWork = compProperties.RequiredWork,
                    PreventConstruction = compProperties.PreventConstruction,
                    TotalNeededPower = compProperties.RequiredPower,
                    TotalNeededResources = compProperties.RequiredMaterials
                });
            }));
            this.ThingForDeployment.OrderBy<EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment, string>((Func<EnhancedDevelopment.Prometheus.Fabrication.ThingForDeployment, string>)(x => x.label));
        }
    }
}
