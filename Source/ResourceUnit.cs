using EnhancedDevelopment.Prometheus.Core;
using Verse;

namespace EnhancedDevelopment.Prometheus.Fabrication
{
    internal class ResourceUnit : ThingWithComps
    {
        public virtual void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            GameComponent_Prometheus.Instance.Comp_Quest.TagMaterialsForTransport(this);
        }

        public virtual void TickLong() => GameComponent_Prometheus.Instance.Comp_Quest.TagMaterialsForTransport(this);
    }
}
