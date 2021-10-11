using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Verse;

namespace EnhancedDevelopment.Prometheus.LaserDrill
{
    internal class LaserDrillVisual : ThingWithComps
    {
        private static readonly SimpleCurve DistanceChanceFactor;
        private static readonly FloatRange AngleRange;
        private float Angle;
        public int Duration;
        private int StartTick;

        public virtual void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            FloatRange angleRange = LaserDrillVisual.AngleRange;
            this.Angle = ((FloatRange)ref angleRange).RandomInRange;
            this.StartTick = Find.TickManager.TicksGame;
            ((CompAffectsSky)this.GetComp<CompAffectsSky>()).StartFadeInHoldFadeOut(30, this.Duration - 30 - 15, 15, 1f);
            ((CompOrbitalBeam)this.GetComp<CompOrbitalBeam>()).StartAnimation(this.Duration, 10, this.Angle);
            MoteMaker.MakeBombardmentMote(((Thing)this).Position, ((Thing)this).Map);
            MoteMaker.MakePowerBeamMote(((Thing)this).Position, ((Thing)this).Map);
        }

        public virtual void Tick()
        {
            base.Tick();
            if (this.TicksPassed >= this.Duration)
                ((Thing)this).Destroy((DestroyMode)0);
            if (((Thing)this).Destroyed || Find.TickManager.TicksGame % 50 != 0)
                return;
            this.StartRandomFire();
        }

        public virtual void ExposeData()
        {
            base.ExposeData();
            // ISSUE: cast to a reference type
            Scribe_Values.Look<int>((M0 &) ref this.Duration, "Duration", (M0)0, false);
            // ISSUE: cast to a reference type
            Scribe_Values.Look<float>((M0 &) ref this.Angle, "Angle", (M0)0.0, false);
            // ISSUE: cast to a reference type
            Scribe_Values.Look<int>((M0 &) ref this.StartTick, "StartTick", (M0)0, false);
        }

        public virtual void Draw()
        {
            this.Comps_PostDraw();
        }

        private void StartRandomFire()
        {
            // ISSUE: method pointer
            // ISSUE: method pointer
            FireUtility.TryStartFireIn((IntVec3)GenCollection.RandomElementByWeight<IntVec3>((IEnumerable<M0>)GenRadial.RadialCellsAround(((Thing)this).Position, 25f, true).Where<IntVec3>(new Func<IntVec3, bool>((object)this, __methodptr(\u003CStartRandomFire\u003Eb__9_0))), (Func<M0, float>)new Func<IntVec3, float>((object)this, __methodptr(\u003CStartRandomFire\u003Eb__9_1))), ((Thing)this).Map, Rand.Range(0.1f, 0.925f));
        }

        protected int TicksLeft
        {
            get
            {
                return this.Duration - this.TicksPassed;
            }
        }

        protected int TicksPassed
        {
            get
            {
                return Find.TickManager.TicksGame - this.StartTick;
            }
        }

        public LaserDrillVisual()
        {
            this.Duration = 600;
            base.\u002Ector();
        }

        static LaserDrillVisual()
        {
            SimpleCurve simpleCurve = new SimpleCurve();
            simpleCurve.Add(new CurvePoint(0.0f, 1f), true);
            simpleCurve.Add(new CurvePoint(10f, 0.0f), true);
            LaserDrillVisual.DistanceChanceFactor = simpleCurve;
            LaserDrillVisual.AngleRange = new FloatRange(-12f, 12f);
        }

        [CompilerGenerated]
        private bool \u003CStartRandomFire\u003Eb__9_0(IntVec3 x)
        {
            return GenGrid.InBounds(x, ((Thing)this).Map);
        }

        [CompilerGenerated]
        private float \u003CStartRandomFire\u003Eb__9_1(IntVec3 x)
        {
            return LaserDrillVisual.DistanceChanceFactor.Evaluate(IntVec3Utility.DistanceTo(x, ((Thing)this).Position));
        }
    }
}