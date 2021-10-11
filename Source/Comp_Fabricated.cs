using Verse;

namespace EnhancedDevelopment.Prometheus.Fabrication
{
    internal class Comp_Fabricated : ThingComp
    {
        public CompProperties_Fabricated Properties;

        public virtual void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            this.Properties = (CompProperties_Fabricated)this.props;
        }
    }
}