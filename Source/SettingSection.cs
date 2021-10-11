using UnityEngine;

namespace EnhancedDevelopment.Prometheus.Settings
{
    internal abstract class SettingSection
    {
        public abstract string Name();

        public abstract void DoSettingsWindowContents(Rect canvas);

        public abstract void ExposeData();

        protected SettingSection()
        {
            base.\u002Ector();
        }
    }
}
