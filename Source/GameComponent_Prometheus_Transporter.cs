using System.Collections.Generic;
using Verse;

namespace EnhancedDevelopment.Prometheus.Core
{
    internal class GameComponent_Prometheus_Transporter : GameComponent_BaseClass
    {
        public List<Thing> TransportBuffer = new List<Thing>();

        public override void ExposeData()
        {
        }

        public void ExposeData_WorldComp() => Scribe_Collections.Look<Thing>(ref this.TransportBuffer, "TransportBuffer", (LookMode)2, new object[0]);

        public override int GetTickInterval() => int.MaxValue;

        public override void TickOnInterval()
        {
        }
    }
}
