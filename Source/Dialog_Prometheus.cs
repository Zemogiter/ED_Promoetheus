using EnhancedDevelopment.Prometheus.Core;
using System.Linq;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Quest.Dialog
{
    internal class Dialog_Prometheus : Window
    {
        private static Vector2 m_ScrollPosition;
        private int m_TabSelectionHeight;
        private int m_FooterSectionHeight;
        private Dialog_Prometheus.EnumDialogTabSelection m_CurrentTab;
        private static float viewHeight;

        public Dialog_Prometheus()
        {
            this.m_TabSelectionHeight = 30;
            this.m_FooterSectionHeight = 20;
            this.resizeable = 0;
            this.optionalTitle = "E.D.S.N Exclibur - Fabrication";
            this.m_CurrentTab = Dialog_Prometheus.EnumDialogTabSelection.Fabrication;
            this.doCloseButton = 0;
            this.doCloseX = 0;
            this.closeOnClickedOutside = 0;
            this.doCloseX = 1;
        }

        public virtual void DoWindowContents(Rect inRect)
        {
            this.DoGuiWholeWindowContents(inRect);
        }

        public virtual Vector2 InitialSize
        {
            get
            {
                return new Vector2(1024f, (float)UI.screenHeight);
            }
        }

        private void DoGuiWholeWindowContents(Rect totalCanvas)
        {
            Rect rectContentWindow1 = GenUI.TopPartPixels(totalCanvas, (float)this.m_TabSelectionHeight);
            this.DoGuiHeadder(rectContentWindow1);
            Rect rectContentWindow2;
            ((Rect) rectContentWindow2).(((Rect) totalCanvas).xMin, ((Rect) rectContentWindow1).yMax, ((Rect) totalCanvas).width, ((Rect) totalCanvas).height - (float)this.m_TabSelectionHeight - (float)this.m_FooterSectionHeight);
            if (this.m_CurrentTab == Dialog_Prometheus.EnumDialogTabSelection.SystemStatus)
                this.DoGuiSystemStatus(rectContentWindow2);
            else if (this.m_CurrentTab == Dialog_Prometheus.EnumDialogTabSelection.Fabrication)
                Dialog_Prometheus.DoGuiFabrication(rectContentWindow2, false, new IntVec3(), (Map)null);
            this.DoGuiFooter(GenUI.BottomPartPixels(totalCanvas, (float)this.m_FooterSectionHeight));
        }

        public void DoGuiHeadder(Rect rectContentWindow)
        {
            WidgetRow widgetRow = new WidgetRow(((Rect) rectContentWindow).x, ((Rect) rectContentWindow).y, (UIDirection)3, 99999f, 4f);
            if (widgetRow.ButtonText("Fabrication", (string)null, true, true))
            {
                this.optionalTitle = "E.D.S.N Exclibur - Fabrication";
                this.m_CurrentTab = Dialog_Prometheus.EnumDialogTabSelection.Fabrication;
            }
            if (widgetRow.ButtonText("System Status", (string)null, true, true))
            {
                this.optionalTitle = "E.D.S.N Exclibur - System Status";
                this.m_CurrentTab = Dialog_Prometheus.EnumDialogTabSelection.SystemStatus;
            }
            Widgets.DrawLineHorizontal(((Rect) rectContentWindow).xMin, ((Rect) rectContentWindow).yMax, ((Rect) rectContentWindow).width);
        }

        public void DoGuiFooter(Rect rectContentWindow)
        {
            Widgets.DrawLineHorizontal(((Rect) rectContentWindow).xMin, ((Rect) rectContentWindow).yMin, ((Rect) rectContentWindow).width);
            Widgets.TextArea(rectContentWindow, GameComponent_Prometheus_Quest.GetSingleLineResourceStatus(), true);
        }

        public void DoGuiSystemStatus(Rect rectContentWindow)
        {
            float num = (float)((double)(GameComponent_Prometheus.Instance.Comp_Quest.m_ShipSystems.Count<ShipSystem>() + 1) * (double)ShipSystem.m_Height + 6.0);
            GUI.color = Color.white;
            Rect rect1;
            ((Rect) rect1).((Rect) rectContentWindow).x, ((Rect) rectContentWindow).y, ((Rect) rectContentWindow).width, ((Rect) rectContentWindow).height - (float)this.m_FooterSectionHeight);
            Rect rect2;
            ((Rect) rect2).(0.0f, 0.0f, ((Rect) rect1).width - 16f, num);
            Widgets.DrawLineHorizontal(((Rect) rect1).xMin, ((Rect) rect1).yMin, ((Rect) rect1).width);
            Widgets.BeginScrollView(rect1, ref Dialog_Prometheus.m_ScrollPosition, rect2, true);
            float y = 0.0f;
            for (int index = 0; index < GameComponent_Prometheus.Instance.Comp_Quest.m_ShipSystems.Count<ShipSystem>(); ++index)
            {
                Rect rect3 = GameComponent_Prometheus.Instance.Comp_Quest.m_ShipSystems[index].DoInterface(0.0f, y, ((Rect) rect2).width, index);
                y += ((Rect) rect3).height + 6f;
                Widgets.DrawLineHorizontal(((Rect) rect2).x, y, ((Rect) rect2).width);
            }
            Widgets.EndScrollView();
        }

        public static void DoGuiFabrication(
          Rect rectContentWindow,
          bool showOnlyActiveThings,
          IntVec3 dropLocation = null,
          Map dropMap = null)
        {
            GameComponent_Prometheus.Instance.Comp_Fabrication.AddNewBuildingsUnderConstruction();
            GameComponent_Prometheus.Instance.Comp_Fabrication.DoListing(rectContentWindow, ref Dialog_Prometheus.m_ScrollPosition, ref Dialog_Prometheus.viewHeight, showOnlyActiveThings, dropLocation, dropMap);
        }

        static Dialog_Prometheus()
        {
            Dialog_Prometheus.m_ScrollPosition = new Vector2();
            Dialog_Prometheus.viewHeight = 1000f;
        }

        public enum EnumDialogTabSelection
        {
            SystemStatus,
            Fabrication,
        }
    }
}
