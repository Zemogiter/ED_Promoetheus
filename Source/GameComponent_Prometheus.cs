using System;
using System.Collections.Generic;
using Verse;

namespace EnhancedDevelopment.Prometheus.Core
{
    internal class GameComponent_Prometheus : GameComponent
    {
        public static GameComponent_Prometheus Instance;
        public GameComponent_Prometheus_Quest Comp_Quest;
        public GameComponent_Prometheus_Fabrication Comp_Fabrication;
        public GameComponent_Prometheus_Transporter Comp_Transporter;
        private readonly List<GameComponent_BaseClass> m_SubComponents = new List<GameComponent_BaseClass>();

        public GameComponent_Prometheus(Game game)
        {
            GameComponent_Prometheus.Instance = this;
            this.Comp_Quest = new GameComponent_Prometheus_Quest();
            this.m_SubComponents.Add((GameComponent_BaseClass)this.Comp_Quest);
            this.Comp_Fabrication = new GameComponent_Prometheus_Fabrication();
            this.m_SubComponents.Add((GameComponent_BaseClass)this.Comp_Fabrication);
            this.Comp_Transporter = new GameComponent_Prometheus_Transporter();
            this.m_SubComponents.Add((GameComponent_BaseClass)this.Comp_Transporter);
        }

        public virtual void GameComponentTick()
        {
            base.GameComponentTick();
            int _CurrentTick = Find.TickManager.TicksGame;
            this.m_SubComponents.ForEach((Action<GameComponent_BaseClass>)(x => x.TickIfRequired(_CurrentTick)));
        }

        public virtual void ExposeData()
        {
            base.ExposeData();
            this.m_SubComponents.ForEach((Action<GameComponent_BaseClass>)(x => x.ExposeData()));
        }

        public virtual void FinalizeInit()
        {
            base.FinalizeInit();
            this.m_SubComponents.ForEach((Action<GameComponent_BaseClass>)(x => x.FinalizeInit()));
        }
    }
}
