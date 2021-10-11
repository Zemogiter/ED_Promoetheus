using EnhancedDevelopment.Prometheus.Core;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Quest
{
    internal abstract class ShipSystem
    {
        public static float m_Height;
        public int CurrentLevel;
        private Vector2 m_Position;

        public abstract string Name();

        public virtual string TechnicalName()
        {
            return this.Name().Replace(" ", "");
        }

        public virtual int NanoMaterialNeededForUpgrade()
        {
            return 5 * (this.CurrentLevel + 1);
        }

        public abstract int GetMaxLevel();

        public virtual void ApplyRequiredResearchUnlocks()
        {
        }

        public bool CanUpgradeLevel()
        {
            return !this.IsAtMaxLevel() && this.IsEnoughNanoMaterialsToUpgrade();
        }

        public bool IsAtMaxLevel()
        {
            return this.CurrentLevel >= this.GetMaxLevel();
        }

        private bool IsEnoughNanoMaterialsToUpgrade()
        {
            return GameComponent_Prometheus.Instance.Comp_Quest.ResourceGetReserveStatus(GameComponent_Prometheus_Quest.EnumResourceType.NanoMaterials) >= this.NanoMaterialNeededForUpgrade();
        }

        public virtual string GetDescriptionText()
        {
            return Translator.Translate("SystemDescription_" + this.TechnicalName());
        }

        public void ExposeData()
        {
            // ISSUE: cast to a reference type
            Scribe_Values.Look<int>((M0 &) ref this.CurrentLevel, "ShipSystem_" + this.TechnicalName() + "_CurrentLevel", (M0)0, false);
        }

        public Rect DoInterface(float x, float y, float width, int index)
        {
            Rect rect1;
            ((Rect)rect1).(x, y, width - 20f, ShipSystem.m_Height);
            Rect rect2 = GenUI.LeftHalf(rect1);
            Widgets.TextArea(GenUI.TopHalf(GenUI.TopHalf(rect2)), this.Name() + " Status", true);
            Widgets.TextArea(GenUI.BottomHalf(GenUI.TopHalf(rect2)), "System Status Level: " + this.CurrentLevel.ToString() + " / " + this.GetMaxLevel().ToString(), true);
            Widgets.TextArea(GenUI.TopHalf(GenUI.BottomHalf(rect2)), "Nano Materials Needed for Next Level:" + this.NanoMaterialNeededForUpgrade().ToString(), true);
            Rect rect3 = GenUI.LeftHalf(GenUI.BottomHalf(GenUI.BottomHalf(rect2)));
            if (this.CanUpgradeLevel())
            {
                if (Widgets.ButtonText(rect3, "Upgrade Level", true, false, true))
                    this.TryUpgradeLevel();
            }
            else if (this.IsAtMaxLevel())
                Widgets.ButtonText(rect3, "MAX Level", true, false, true);
            else if (!this.IsEnoughNanoMaterialsToUpgrade())
                Widgets.ButtonText(rect3, "Low Nano Materials", true, false, true);
            else if (!Widgets.ButtonText(rect3, "Upgrade DISABLED", true, false, true))
                ;
            Rect rect4 = GenUI.RightHalf(rect1);
            Text.Font = (GameFont)1;
            Widgets.TextAreaScrollable(rect4, this.GetDescriptionText(), ref this.m_Position, true);
            return rect1;
        }

        public void TryUpgradeLevel()
        {
            GameComponent_Prometheus.Instance.Comp_Quest.ResourceRequestReserve(GameComponent_Prometheus_Quest.EnumResourceType.NanoMaterials, this.NanoMaterialNeededForUpgrade());
            ++this.CurrentLevel;
            GameComponent_Prometheus.Instance.Comp_Quest.UpdateAllResearch();
        }

        public void UpgradeTo(int level)
        {
            if (this.CurrentLevel >= level)
                return;
            this.CurrentLevel = level;
        }

        protected ShipSystem()
        {
            this.CurrentLevel = 0;
            this.m_Position = Vector2.zero;
        }

        static ShipSystem()
        {
            ShipSystem.m_Height = 200f;
        }
    }
}
