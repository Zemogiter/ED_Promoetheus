using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Settings
{
    internal class SettingSection_LaserDrill : SettingSection
    {
        private const int DEFAULT_REQUIRED_DRILL_SCANNING = 500;
        private const int DEFAULT_REQUIRED_DRILL_SHIP_POWER = 10000;
        public int RequiredDrillScanning;
        public int RequiredDrillShipPower;

        public override void DoSettingsWindowContents(Rect canvas)
        {
            Listing_Standard listingStandard1 = new Listing_Standard();
            ((Listing)listingStandard1).ColumnWidth = 250f;
            ((Listing)listingStandard1).Begin(canvas);
            ((Listing)listingStandard1).GapLine(12f);
            listingStandard1.Label("Drill Scanning Time: " + this.RequiredDrillScanning.ToString(), -1f, (string)null);
            ((Listing)listingStandard1).Gap(12f);
            Listing_Standard listingStandard2 = new Listing_Standard();
            ((Listing)listingStandard2).Begin(((Listing)listingStandard1).GetRect(30f));
            ((Listing)listingStandard2).ColumnWidth = 70f;
            listingStandard2.IntAdjuster(ref this.RequiredDrillScanning, 10, 10);
            ((Listing)listingStandard2).NewColumn();
            listingStandard2.IntAdjuster(ref this.RequiredDrillScanning, 100, 100);
            ((Listing)listingStandard2).NewColumn();
            listingStandard2.IntSetter(ref this.RequiredDrillScanning, 500, "Default", 42f);
            ((Listing)listingStandard2).End();
            ((Listing)listingStandard1).GapLine(12f);
            listingStandard1.Label("Required Ship Power: " + this.RequiredDrillShipPower.ToString(), -1f, (string)null);
            ((Listing)listingStandard1).Gap(12f);
            Listing_Standard listingStandard3 = new Listing_Standard();
            ((Listing)listingStandard3).Begin(((Listing)listingStandard1).GetRect(30f));
            ((Listing)listingStandard3).ColumnWidth = 70f;
            listingStandard3.IntAdjuster(ref this.RequiredDrillShipPower, 100, 100);
            ((Listing)listingStandard3).NewColumn();
            listingStandard3.IntAdjuster(ref this.RequiredDrillShipPower, 1000, 1000);
            ((Listing)listingStandard3).NewColumn();
            listingStandard3.IntSetter(ref this.RequiredDrillShipPower, 10000, "Default", 42f);
            ((Listing)listingStandard3).End();
            ((Listing)listingStandard1).GapLine(12f);
            ((Listing)listingStandard1).End();
        }

        public override void ExposeData()
        {
            // ISSUE: cast to a reference type
            Scribe_Values.Look<int>(ref this.RequiredDrillScanning, "RequiredDrillScanning", 500, false);
            // ISSUE: cast to a reference type
            Scribe_Values.Look<int>(ref this.RequiredDrillShipPower, "RequiredDrillShipPower", 10000, false);
        }

        public override string Name()
        {
            return "Laser Drill";
        }

        public SettingSection_LaserDrill()
        {
            this.RequiredDrillScanning = 500;
            this.RequiredDrillShipPower = 10000;
        }
    }
}
