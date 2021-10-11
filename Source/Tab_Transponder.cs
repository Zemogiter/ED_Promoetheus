using EnhancedDevelopment.Prometheus.Core;
using RimWorld;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Quest
{
    internal class Tab_Transponder : ITab
    {
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
                return false;
            }
        }

        public Tab_Transponder()
        {
            ((InspectTabBase)this).size = )WinSize;
            ((InspectTabBase)this).labelKey = "Transponder";
        }

        protected virtual void FillTab()
        {
            Rect rect = GenUI.ContractedBy(new Rect(0.0f, 0.0f, (float)Tab_Transponder.WinSize.x, (float)Tab_Transponder.WinSize.y), 10f);
            Listing_Standard listingStandard = new Listing_Standard();
            ((Listing)listingStandard).ColumnWidth = ((Rect) rect).width;
            ((Listing)listingStandard).Begin(rect);
            ((Listing)listingStandard).GapLine(12f);
            listingStandard.Label(GameComponent_Prometheus_Quest.GetSingleLineResourceStatus(), -1f, (string)null);
            ((Listing)listingStandard).Gap(12f);
            ((Listing)listingStandard).End();
        }

        static Tab_Transponder()
        {
            Tab_Transponder.WinSize = new Vector2(500f, 400f);
        }
    }
}