using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Quest.Dialog
{
    internal class Dialog_0_Generic : Window
    {
        private string m_Text;
        private Vector2 m_Position;

        public Dialog_0_Generic(string title, string text)
        {
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
        }

        private void InitialWindowContents(Rect canvas)
        {
            Text.Font = (GameFont)1;
            Widgets.TextAreaScrollable(GenUI.TopPartPixels(canvas, ((Rect)canvas).height - (float)((Vector2)CloseButSize).y), this.m_Text, ref this.m_Position, true);
            Text.Font = (GameFont)2;
        }
    }
}