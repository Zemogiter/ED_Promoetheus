using EnhancedDevelopment.Prometheus.Core;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Fabrication
{
    internal class ThingForDeployment
    {
        public string defName;
        public string label;
        public int TotalNeededWork = 100;
        public int TotalNeededResources = 100;
        public int TotalNeededPower = 100;
        public bool PreventConstruction = false;
        public int WorkRemaining = 100;
        public bool ConstructionInProgress = false;
        public int UnitsAvalable = 0;
        public int UnitsRequestedAditional = 0;

        public bool ShouldBeShown(bool showOnlyActiveThings) => !(this.PreventConstruction | showOnlyActiveThings) || (this.ConstructionInProgress || this.UnitsAvalable >= 1 || this.UnitsRequestedAditional >= 1);

		[System.Obsolete]
		public void ExposeData()
        {
            Log.Message("Expose: " + this.defName, false);
            // ISSUE: cast to a reference type
            Scribe_Values.Look<int>((M0 &) this.WorkRemaining, this.defName + "_WorkRemaining", (M0)0, false);
            // ISSUE: cast to a reference type
            Scribe_Values.Look<bool>((M0 &) this.ConstructionInProgress, this.defName + "_ConstructionInProgress", (M0)0, false);
            // ISSUE: cast to a reference type
            Scribe_Values.Look<int>((M0 &) this.UnitsAvalable, this.defName + "_UnitsAvalable", (M0)0, false);
            // ISSUE: cast to a reference type
            Scribe_Values.Look<int>((M0 &) this.UnitsRequestedAditional, this.defName + "_UnitsRequestedAditional", (M0)0, false);
        }

        public ThingForDeployment(string defName, string label)
        {
            this.defName = defName;
            this.label = label;
        }

        public void InitiateDrop(IntVec3 dropLocation, Map dropMap)
        {
            if (this.UnitsAvalable < 1)
                return;
            Thing thing = ThingMaker.MakeThing(ThingDef.Named(this.defName), (ThingDef)null);
            MinifiedThing minifiedThing = MinifyUtility.MakeMinified(thing);
            List<Thing> thingList = new List<Thing>();
            if (minifiedThing != null)
                thingList.Add((Thing)minifiedThing);
            else
                thingList.Add(thing);
            --this.UnitsAvalable;
            DropPodUtility.DropThingsNear(dropLocation, dropMap, (IEnumerable<Thing>)thingList, 110, false, false, true);
        }

        public Rect DoInterface(
          float x,
          float y,
          float width,
          int index,
          IntVec3 dropLocation = null,
          Map dropMap = null)
        {
            Rect rect;
            // ISSUE: explicit constructor call
            ((Rect)ref rect).\u002Ector(x, y, width, 100f);
            this.DoInterface_Column1(GenUI.LeftHalf(rect), dropLocation, dropMap);
            this.DoInterface_Column2(GenUI.RightHalf(rect), dropLocation, dropMap);
            return rect;
        }

        private void DoInterface_Column1(Rect _RectTotal, IntVec3 dropLocation = null, Map dropMap = null)
        {
            Rect rect1 = GenUI.TopHalf(_RectTotal);
            Rect rect2 = GenUI.BottomHalf(_RectTotal);
            Rect rect3 = GenUI.TopHalf(rect1);
            if (this.ConstructionInProgress)
                Widgets.TextArea(rect3, this.label + " - In Progress", true);
            else
                Widgets.TextArea(rect3, this.label, true);
            Widgets.TextArea(GenUI.BottomHalf(rect1), "Work: " + this.WorkRemaining.ToString() + " / " + this.TotalNeededWork.ToString(), true);
            Widgets.TextArea(GenUI.TopHalf(rect2), "RU:" + (object)this.TotalNeededResources + " Power: " + (object)this.TotalNeededPower, true);
            Rect rect4 = GenUI.BottomHalf(rect2);
            Widgets.TextArea(GenUI.LeftHalf(rect4), "Avalable: " + this.UnitsAvalable.ToString() + " Requested: " + this.UnitsRequestedAditional.ToString(), true);
            if (dropMap != null && this.UnitsAvalable >= 1 && Widgets.ButtonText(GenUI.RightHalf(GenUI.RightHalf(rect4)), "Deploy", true, false, true))
                this.InitiateDrop(dropLocation, dropMap);
            if (Widgets.ButtonText(GenUI.LeftHalf(GenUI.LeftHalf(GenUI.RightHalf(rect4))), "-", true, false, true))
            {
                if (this.UnitsRequestedAditional > 0)
                    this.UnitsRequestedAditional -= GenUI.CurrentAdjustmentMultiplier();
                if (this.UnitsRequestedAditional < 0)
                    this.UnitsRequestedAditional = 0;
            }
            if (this.PreventConstruction || !Widgets.ButtonText(GenUI.RightHalf(GenUI.LeftHalf(GenUI.RightHalf(rect4))), "+", true, false, true))
                return;
            this.UnitsRequestedAditional += GenUI.CurrentAdjustmentMultiplier();
        }

        private void DoInterface_Column2(Rect _RectTotal, IntVec3 dropLocation = null, Map dropMap = null)
        {
            string description = (string)((Def)ThingDef.Named(this.defName)).description;
            Widgets.TextArea(_RectTotal, description, true);
        }

        public void AfterCompletion()
        {
            if (!(this.defName == "NanoMaterial"))
                return;
            GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.NanoMaterials, this.UnitsAvalable);
            this.UnitsAvalable = 0;
        }
    }
}
