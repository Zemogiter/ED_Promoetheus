namespace EnhancedDevelopment.Prometheus.Quest.ShipSystems
{
    internal class ShipSystem_Tactical : ShipSystem
    {
        public override int GetMaxLevel()
        {
            return 6;
        }

        public override string Name()
        {
            return "Tactical";
        }

        public override void ApplyRequiredResearchUnlocks()
        {
        }
    }
}