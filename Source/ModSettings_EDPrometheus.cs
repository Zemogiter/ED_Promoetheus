using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Settings
{
    internal class ModSettings_EDPrometheus : ModSettings
    {
        public List<SettingSection> m_Settings;
        public SettingSection_Shields Shields;
        public SettingSection_NanoShields NanoShields;
        public SettingSection_LaserDrill LaserDrill;
        public SettingSection_Quest Quest;
        private SettingSection m_CurrentSetting;

        public ModSettings_EDPrometheus()
        {
            this.Shields = new SettingSection_Shields();
            this.NanoShields = new SettingSection_NanoShields();
            this.LaserDrill = new SettingSection_LaserDrill();
            this.Quest = new SettingSection_Quest();
            this.m_CurrentSetting = (SettingSection)null;
            base.\u002Ector();
            List<SettingSection> settingSectionList = new List<SettingSection>();
            settingSectionList.Add((SettingSection)this.NanoShields);
            settingSectionList.Add((SettingSection)this.LaserDrill);
            settingSectionList.Add((SettingSection)this.Quest);
            this.m_Settings = settingSectionList;
        }

        public virtual void ExposeData()
        {
            base.ExposeData();
            // ISSUE: method pointer
            this.m_Settings.ForEach(ModSettings_EDPrometheus.\u003C\u003Ec.\u003C\u003E9__6_0 ?? (ModSettings_EDPrometheus.\u003C\u003Ec.\u003C\u003E9__6_0 = new Action<SettingSection>((object)ModSettings_EDPrometheus.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CExposeData\u003Eb__6_0))));
        }

        public void DoSettingsWindowContents(Rect canvas)
        {
            Rect rect;
            ((Rect)ref rect).\u002Ector(350f, 0.0f, 150f, 35f);
            if (Widgets.ButtonText(rect, "Select Page", true, false, true))
            {
                List<FloatMenuOption> floatMenuOptionList = new List<FloatMenuOption>();
                foreach (SettingSection setting in this.m_Settings)
                {
                    ModSettings_EDPrometheus.\u003C\u003Ec__DisplayClass8_0 cDisplayClass80 = new ModSettings_EDPrometheus.\u003C\u003Ec__DisplayClass8_0();
                    cDisplayClass80.\u003C\u003E4__this = this;
                    cDisplayClass80._CurrentSettings = setting;
                    // ISSUE: method pointer
                    floatMenuOptionList.Add(new FloatMenuOption(cDisplayClass80._CurrentSettings.Name(), new Action((object)cDisplayClass80, __methodptr(\u003CDoSettingsWindowContents\u003Eb__0)), (MenuOptionPriority)4, (Action)null, (Thing)null, 0.0f, (Func<Rect, bool>)null, (WorldObject)null));
                    Find.WindowStack.Add((Window)new FloatMenu(floatMenuOptionList));
                }
            }
            if (this.m_CurrentSetting == null)
                return;
            Text.Font = (GameFont)2;
            Widgets.Label(new Rect(510f, 0.0f, 150f, 35f), this.m_CurrentSetting.Name());
            Text.Font = (GameFont)1;
            this.m_CurrentSetting.DoSettingsWindowContents(canvas);
        }

        [CompilerGenerated]
        [Serializable]
        private sealed class \u003C\u003Ec
    {
      public static readonly ModSettings_EDPrometheus.\u003C\u003Ec \u003C\u003E9;
      public static Action<SettingSection> \u003C\u003E9__6_0;

      static \u003C\u003Ec()
        {
            ModSettings_EDPrometheus.\u003C\u003Ec.\u003C\u003E9 = new ModSettings_EDPrometheus.\u003C\u003Ec();
        }

        public \u003C\u003Ec()
        {
            base.\u002Ector();
        }

        internal void \u003CExposeData\u003Eb__6_0(SettingSection s)
        {
            s.ExposeData();
        }
    }

    [CompilerGenerated]
    private sealed class \u003C\u003Ec__DisplayClass8_0
    {
      public SettingSection _CurrentSettings;
    public ModSettings_EDPrometheus \u003C\u003E4__this;

      public \u003C\u003Ec__DisplayClass8_0()
    {
        base.\u002Ector();
    }

    internal void \u003CDoSettingsWindowContents\u003Eb__0()
    {
        this.\u003C\u003E4__this.m_CurrentSetting = this._CurrentSettings;
    }
}
  }
}
