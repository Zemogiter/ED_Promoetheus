namespace EnhancedDevelopment.Prometheus.Quest.ShipSystems
{
    internal class ShipSystem_PowerGeneration : ShipSystem
    {
        public override int GetMaxLevel()
        {
            return 4;
        }

        public override string Name()
        {
            return "Power Generation";
        }

        public override void ApplyRequiredResearchUnlocks()
        {
        }
    }
}
