using EnhancedDevelopment.Prometheus.Core;
using RimWorld.Planet;

namespace EnhancedDevelopment.Prometheus.Transporter
{
    internal class WorldComponent_Transporter : WorldComponent
    {
        public WorldComponent_Transporter(World world)
        {
            base.\u002Ector(world);
        }

        public virtual void WorldComponentUpdate()
        {
        }

        public virtual void WorldComponentTick()
        {
        }

        public virtual void ExposeData()
        {
            GameComponent_Prometheus.Instance.Comp_Transporter.ExposeData_WorldComp();
        }

        public virtual void FinalizeInit()
        {
        }
    }
}