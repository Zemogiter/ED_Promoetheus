namespace EnhancedDevelopment.Prometheus.Quest.ShipSystems
{
    internal class ShipSystem_Regeneration : ShipSystem
    {
        public override int GetMaxLevel()
        {
            return 4;
        }

        public override string Name()
        {
            return "Regeneration";
        }

        public override void ApplyRequiredResearchUnlocks()
        {
        }
    }
}
