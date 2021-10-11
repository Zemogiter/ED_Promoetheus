using EnhancedDevelopment.Prometheus.Quest.Dialog;
using RimWorld;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Quest
{
    internal class Tab_Fabrication : ITab
    {
        public TargetingParameters targetingParams;
        private static readonly Vector2 WinSize;

        private Comp_EDSNTransponder SelectedCompTransponder
        {
            get
            {
                Thing thing = Find.Selector.SingleSelectedThing;
                if (thing is MinifiedThing minifiedThing)
                    thing = minifiedThing.InnerThing;
                return thing == null ? (Comp_EDSNTransponder)null : (Comp_EDSNTransponder)ThingCompUtility.TryGetComp<Comp_EDSNTransponder>(thing);
            }
        }

        public virtual bool IsVisible
        {
            get
            {
                return this.SelectedCompTransponder != null;
            }
        }

        public Tab_Fabrication()
        {
            this.targetingParams = new TargetingParameters();
            base.\u002Ector();
            ((InspectTabBase)this).size = (__Null)Tab_Fabrication.WinSize;
            ((InspectTabBase)this).labelKey = (__Null)"Fabrication";
            this.targetingParams.canTargetLocations = (__Null)1;
        }

        protected virtual void FillTab()
        {
            int num = 30;
            Vector2 winSize = Tab_Fabrication.WinSize;
            Rect rect = GenUI.ContractedBy(new Rect(0.0f, 0.0f, (float)winSize.x, (float)winSize.y), 10f);
            Rect rectContentWindow = GenUI.TopPartPixels(rect, ((Rect)ref rect).height - (float)num);
            this.DoGuiForTargetingDrop(GenUI.BottomPartPixels(rect, (float)num));
            IntVec3 dropLocation = ((Thing)this.SelectedCompTransponder.parent).Position;
            if (!object.Equals((object)this.SelectedCompTransponder.DropLocation, (object)IntVec3.Invalid))
                dropLocation = this.SelectedCompTransponder.DropLocation;
            Dialog_Prometheus.DoGuiFabrication(rectContentWindow, true, dropLocation, ((Thing)this.SelectedCompTransponder.parent).Map);
        }

        private void DoGuiForTargetingDrop(Rect targetingFooter)
        {
            if (Widgets.ButtonText(GenUI.LeftHalf(targetingFooter), "Drop Target", true, true, true))
            {
                // ISSUE: method pointer
                Find.Targeter.BeginTargeting(this.targetingParams, new Action<LocalTargetInfo>((object)this, __methodptr(\u003CDoGuiForTargetingDrop\u003Eb__8_0)), (Pawn)null, (Action)null, (Texture2D)null);
            }
            Rect rect = GenUI.ContractedBy(GenUI.LeftHalf(GenUI.RightHalf(targetingFooter)), 5f);
            if (object.Equals((object)this.SelectedCompTransponder.DropLocation, (object)IntVec3.Invalid))
                Widgets.Label(rect, "This Location");
            else
                Widgets.Label(rect, "X: " + (object)(int)this.SelectedCompTransponder.DropLocation.x + " Z: " + (object)(int)this.SelectedCompTransponder.DropLocation.z);
            if (!Widgets.ButtonText(GenUI.RightHalf(GenUI.RightHalf(targetingFooter)), "Reset", true, true, true))
                return;
            this.SelectedCompTransponder.DropLocation = IntVec3.Invalid;
        }

        static Tab_Fabrication()
        {
            Tab_Fabrication.WinSize = new Vector2(420f, 480f);
        }

        [CompilerGenerated]
        private void \u003CDoGuiForTargetingDrop\u003Eb__8_0(LocalTargetInfo target)
        {
            this.SelectedCompTransponder.DropLocation = ((LocalTargetInfo)ref target).Cell;
        }
    }
}
