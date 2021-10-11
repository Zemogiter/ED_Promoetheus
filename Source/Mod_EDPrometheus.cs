using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Settings
{
    internal class Mod_EDPrometheus : Mod
    {
        public static ModSettings_EDPrometheus Settings;

        public Mod_EDPrometheus(ModContentPack content)
        {
            Mod_EDPrometheus.Settings = (ModSettings_EDPrometheus)this.GetSettings<ModSettings_EDPrometheus>();
        }

        public virtual string SettingsCategory()
        {
            return "ED-Prometheus";
        }

        public virtual void DoSettingsWindowContents(Rect inRect)
        {
            Mod_EDPrometheus.Settings.DoSettingsWindowContents(inRect);
        }
    }
}
