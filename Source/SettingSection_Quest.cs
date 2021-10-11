using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Settings
{
    internal class SettingSection_Quest : SettingSection
    {
        private const int DEFAULT_INITAL_SHIP_SETUP_POWER_REQUIRED = 10000;
        private const int DEFAULT_INITAL_SHIP_SETUP_RESOURCES_REQUIRED = 500;
        private const int DEFAULT_INITAL_OVERRIDE_DISABLED = -1;
        public int InitialShipSetup_PowerRequired;
        public int InitialShipSetup_ResourcesRequired;
        public int Quest_PowerForNanoMaterial;
        public int Quest_ResourceUnitsForNanoMaterial;
        public bool Quest_OverrideConsoleRequired;

        public override void DoSettingsWindowContents(Rect canvas)
        {
            Listing_Standard listingStandard1 = new Listing_Standard();
            ((Listing)listingStandard1).ColumnWidth = 250f;
            ((Listing)listingStandard1).Begin(canvas);
            ((Listing)listingStandard1).GapLine(12f);
            listingStandard1.Label("InitialShipSetup_PowerRequired: " + this.InitialShipSetup_PowerRequired.ToString(), -1f, (string)null);
            ((Listing)listingStandard1).Gap(12f);
            Listing_Standard listingStandard2 = new Listing_Standard();
            ((Listing)listingStandard2).Begin(((Listing)listingStandard1).GetRect(30f));
            ((Listing)listingStandard2).ColumnWidth = 70f;
            listingStandard2.IntAdjuster(ref this.InitialShipSetup_PowerRequired, 100, 0);
            ((Listing)listingStandard2).NewColumn();
            listingStandard2.IntAdjuster(ref this.InitialShipSetup_PowerRequired, 500, 0);
            ((Listing)listingStandard2).NewColumn();
            listingStandard2.IntSetter(ref this.InitialShipSetup_PowerRequired, 10000, "Default", 42f);
            ((Listing)listingStandard2).End();
            ((Listing)listingStandard1).GapLine(12f);
            listingStandard1.Label("InitialShipSetup_ResourcesRequired: " + this.InitialShipSetup_ResourcesRequired.ToString(), -1f, (string)null);
            ((Listing)listingStandard1).Gap(12f);
            Listing_Standard listingStandard3 = new Listing_Standard();
            ((Listing)listingStandard3).Begin(((Listing)listingStandard1).GetRect(30f));
            ((Listing)listingStandard3).ColumnWidth = 70f;
            listingStandard3.IntAdjuster(ref this.InitialShipSetup_ResourcesRequired, 10, 0);
            ((Listing)listingStandard3).NewColumn();
            listingStandard3.IntAdjuster(ref this.InitialShipSetup_ResourcesRequired, 100, 0);
            ((Listing)listingStandard3).NewColumn();
            listingStandard3.IntSetter(ref this.InitialShipSetup_ResourcesRequired, 500, "Default", 42f);
            ((Listing)listingStandard3).End();
            ((Listing)listingStandard1).GapLine(12f);
            listingStandard1.Label("-1 indicates a default value set in XML", -1f, (string)null);
            ((Listing)listingStandard1).Gap(12f);
            listingStandard1.Label("Power For Nano Material: " + this.Quest_PowerForNanoMaterial.ToString(), -1f, (string)null);
            listingStandard1.Label("XML Default is: 500", -1f, (string)null);
            ((Listing)listingStandard1).Gap(12f);
            Listing_Standard listingStandard4 = new Listing_Standard();
            ((Listing)listingStandard4).Begin(((Listing)listingStandard1).GetRect(30f));
            ((Listing)listingStandard4).ColumnWidth = 70f;
            listingStandard4.IntAdjuster(ref this.Quest_PowerForNanoMaterial, 1, 0);
            ((Listing)listingStandard4).NewColumn();
            listingStandard4.IntAdjuster(ref this.Quest_PowerForNanoMaterial, 100, 0);
            ((Listing)listingStandard4).NewColumn();
            listingStandard4.IntSetter(ref this.Quest_PowerForNanoMaterial, -1, "Default", 42f);
            ((Listing)listingStandard4).End();
            ((Listing)listingStandard1).Gap(12f);
            listingStandard1.Label("Resource Units For Nano Material: " + this.Quest_ResourceUnitsForNanoMaterial.ToString(), -1f, (string)null);
            listingStandard1.Label("XML Default is: 100", -1f, (string)null);
            ((Listing)listingStandard1).Gap(12f);
            Listing_Standard listingStandard5 = new Listing_Standard();
            ((Listing)listingStandard5).Begin(((Listing)listingStandard1).GetRect(30f));
            ((Listing)listingStandard5).ColumnWidth = 70f;
            listingStandard5.IntAdjuster(ref this.Quest_ResourceUnitsForNanoMaterial, 1, 0);
            ((Listing)listingStandard5).NewColumn();
            listingStandard5.IntAdjuster(ref this.Quest_ResourceUnitsForNanoMaterial, 100, 0);
            ((Listing)listingStandard5).NewColumn();
            listingStandard5.IntSetter(ref this.Quest_ResourceUnitsForNanoMaterial, -1, "Default", 42f);
            ((Listing)listingStandard5).End();
            ((Listing)listingStandard1).GapLine(12f);
            listingStandard1.CheckboxLabeled("Override Console Required", ref this.Quest_OverrideConsoleRequired, "Allows detecting the initial signal without needing a communication console.");
            ((Listing)listingStandard1).GapLine(12f);
            ((Listing)listingStandard1).End();
        }

        public override void ExposeData()
        {
            // ISSUE: cast to a reference type
            Scribe_Values.Look<int>(ref this.InitialShipSetup_PowerRequired, "InitialShipSetup_PowerRequired", 10000, false);
            // ISSUE: cast to a reference type
            Scribe_Values.Look<int>(ref this.InitialShipSetup_ResourcesRequired, "InitialShipSetup_ResourcesRequired", 500, false);
            // ISSUE: cast to a reference type
            Scribe_Values.Look<int>(ref this.Quest_PowerForNanoMaterial, "Quest_PowerForNanoMaterial", - 1, false);
            // ISSUE: cast to a reference type
            Scribe_Values.Look<int>(ref this.Quest_ResourceUnitsForNanoMaterial, "Quest_ResourceUnitsForNanoMaterial", - 1, false);
            // ISSUE: cast to a reference type
            Scribe_Values.Look<bool>(ref this.Quest_OverrideConsoleRequired, "Quest_OverrideConsoleRequired", 0, false);
        }

        public override string Name()
        {
            return "Quest";
        }

        public SettingSection_Quest()
        {
            this.InitialShipSetup_PowerRequired = 10000;
            this.InitialShipSetup_ResourcesRequired = 500;
            this.Quest_PowerForNanoMaterial = -1;
            this.Quest_ResourceUnitsForNanoMaterial = -1;
            this.Quest_OverrideConsoleRequired = false;
        }
    }
}