namespace EnhancedDevelopment.Prometheus.Core
{
    internal abstract class GameComponent_BaseClass
    {
        public void TickIfRequired(int currentTick)
        {
            if (currentTick % this.GetTickInterval() != 0)
                return;
            this.TickOnInterval();
        }

        public virtual void FinalizeInit()
        {
        }

        public abstract int GetTickInterval();

        public abstract void TickOnInterval();

        public abstract void ExposeData();
    }
}
