namespace EnhancedDevelopment.Prometheus.Quest.ShipSystems
{
    internal class ShipSystem_Resourcing : ShipSystem
    {
        public override int GetMaxLevel()
        {
            return 4;
        }

        public override string Name()
        {
            return "Resourcing";
        }

        public override void ApplyRequiredResearchUnlocks()
        {
        }
    }
}
