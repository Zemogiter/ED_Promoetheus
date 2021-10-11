using EnhancedDevelopment.Prometheus.Core;
using EnhancedDevelopment.Prometheus.Power;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Quest.Dialog
{
    internal class Dialog_1_PowerRequest : Window
    {
        private string m_Text;
        private Vector2 m_Position;
        private Building ContactSource;

        public Dialog_1_PowerRequest(string title, string text, Building contactSource)
        {
            this.ContactSource = contactSource;
            this.m_Position = Vector2.zero;
            this.m_Text = text;
            this.resizeable = 0;
            this.optionalTitle = title;
            this.doCloseButton = 1;
            this.doCloseX = 0;
            this.closeOnClickedOutside = 0;
        }

        public virtual void DoWindowContents(Rect inRect)
        {
            this.InitialWindowContents(inRect);
            GameFont font = Text.Font;
            Text.Font = (GameFont)0;
            IntVec2 intVec2;
            ((IntVec2) intVec2).(150, 40);
            if (Widgets.ButtonText(new Rect(((Rect) inRect).xMax - intVec2.x, ((Rect) inRect).yMax - intVec2.z, (float)intVec2.x, (float)intVec2.z), "Request Aditional Relay, 300RU, 700P", true, false, true))
            {
                GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.ResourceUnits, -300);
                GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.Power, -700);
                Building_QuantumPowerRelay quantumPowerRelay = (Building_QuantumPowerRelay)ThingMaker.MakeThing(ThingDef.Named("QuantumPowerRelay"), (ThingDef)null);
                List<Thing> thingList = new List<Thing>();
                thingList.Add((Thing)quantumPowerRelay);
                DropPodUtility.DropThingsNear(((Thing)this.ContactSource).Position, ((Thing)this.ContactSource).Map, (IEnumerable<Thing>)thingList, 110, false, false, true);
            }
            Text.Font = font;
        }

        private void InitialWindowContents(Rect canvas)
        {
            Text.Font = (GameFont)1;
            Widgets.TextAreaScrollable(GenUI.TopPartPixels(canvas, ((Rect)canvas).height - (float)((Vector2)CloseButSize).y), this.m_Text, ref this.m_Position, true);
            Text.Font = (GameFont)2;
        }
    }
}
