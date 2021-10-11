using EnhancedDevelopment.Prometheus.Core;
using EnhancedDevelopment.Prometheus.Settings;
using RimWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.LaserDrill
{
    [StaticConstructorOnStartup]
    internal class Comp_LaserDrill :
    /*[StaticConstructorOnStartup]*/
    ThingComp
    {
        private int DrillScanningRemaining;
        private CompProperties_LaserDrill Properties;
        private static Texture2D UI_LASER_ACTIVATE;
        private static Texture2D UI_LASER_ACTIVATEFILL;
        private Comp_LaserDrill.EnumLaserDrillState m_CurrentStaus;

        static Comp_LaserDrill()
        {
            Comp_LaserDrill.UI_LASER_ACTIVATE = ContentFinder<Texture2D>.Get("UI/Power/SteamGeyser", true);
            Comp_LaserDrill.UI_LASER_ACTIVATEFILL = ContentFinder<Texture2D>.Get("UI/Power/RemoveSteamGeyser", true);
        }

        public virtual void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            this.Properties = this.props as CompProperties_LaserDrill;
            if (respawningAfterLoad)
                return;
            this.SetRequiredDrillScanningToDefault();
        }

        private bool HasSufficientShipPower()
        {
            return GameComponent_Prometheus.Instance.Comp_Quest.ResourceGetReserveStatus(GameComponent_Prometheus_Quest.EnumResourceType.Power) < Mod_EDPrometheus.Settings.LaserDrill.RequiredDrillShipPower;
        }

        public virtual void PostExposeData()
        {
            base.PostExposeData();
            // ISSUE: cast to a reference type
            Scribe_Values.Look<int>(ref this.DrillScanningRemaining, "DrillScanningRemaining", 0, false);
        }

        public virtual void CompTickRare()
        {
            if (this.DrillScanningRemaining <= 0)
            {
                this.m_CurrentStaus = !this.HasSufficientShipPower() ? Comp_LaserDrill.EnumLaserDrillState.ReadyToActivate : Comp_LaserDrill.EnumLaserDrillState.LowPower;
            }
            else
            {
                this.m_CurrentStaus = Comp_LaserDrill.EnumLaserDrillState.Scanning;
                --this.DrillScanningRemaining;
            }
            base.CompTickRare();
        }

        public virtual string CompInspectStringExtra()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (this.m_CurrentStaus == Comp_LaserDrill.EnumLaserDrillState.LowPower)
            {
                stringBuilder.AppendLine("Scan complete");
                stringBuilder.AppendLine("Low Power on Ship");
            }
            else if (this.m_CurrentStaus == Comp_LaserDrill.EnumLaserDrillState.ReadyToActivate)
            {
                stringBuilder.AppendLine("Scan complete");
                stringBuilder.AppendLine("Ready for Laser Activation");
            }
            else if (this.m_CurrentStaus == Comp_LaserDrill.EnumLaserDrillState.Scanning)
            {
                stringBuilder.AppendLine("Scanning in Progress - Remaining: " + (object)this.DrillScanningRemaining);
                if (!this.HasSufficientShipPower())
                    stringBuilder.AppendLine("Low Power on Ship");
            }
            stringBuilder.Append("Ship Power: " + (GameComponent_Prometheus.Instance.Comp_Quest.ResourceGetReserveStatus(GameComponent_Prometheus_Quest.EnumResourceType.Power).ToString() + " / " + (object)Mod_EDPrometheus.Settings.LaserDrill.RequiredDrillShipPower).ToString());
            return stringBuilder.ToString();
        }

        public virtual IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            Comp_LaserDrill.CompGetGizmosExtra getGizmosExtraD12 = new Comp_LaserDrill.CompGetGizmosExtra(-2);
            getGizmosExtraD12.\u003C\u003E4__this = this;
            return (IEnumerable<Gizmo>)getGizmosExtraD12;
        }

        public virtual void PostDeSpawn(Map map)
        {
            this.SetRequiredDrillScanningToDefault();
            base.PostDeSpawn(map);
        }

        private void SetRequiredDrillScanningToDefault()
        {
            this.DrillScanningRemaining = Mod_EDPrometheus.Settings.LaserDrill.RequiredDrillScanning;
        }

        public Thing FindClosestGeyser()
        {
            List<Thing> thingList = ((ListerThings)((Thing)this.parent).Map.listerThings).ThingsOfDef((ThingDef)ThingDefOf.SteamGeyser);
            Thing thing1 = (Thing)null;
            double num1 = double.MaxValue;
            foreach (Thing thing2 in thingList)
            {
                if (thing2.Spawned)
                {
                    IntVec3 position = ((Thing)this.parent).Position;
                    if (((IntVec3)ref position).InHorDistOf(thing2.Position, 5f))
                    {
                        double num2 = Math.Sqrt(Math.Pow((double)(((Thing)this.parent).Position.x - thing2.Position.x), 2.0) + Math.Pow((double)(((Thing)this.parent).Position.y - thing2.Position.y), 2.0));
                        if (num2 < num1)
                        {
                            num1 = num2;
                            thing1 = thing2;
                        }
                    }
                }
            }
            return thing1;
        }

        public void TriggerLaserToFill()
        {
            if (this.FindClosestGeyser() != null)
            {
                Messages.Message("SteamGeyser Removed.", (MessageTypeDef)MessageTypeDefOf.TaskCompletion, true);
                ((Entity)this.FindClosestGeyser()).DeSpawn((DestroyMode)0);
                GameComponent_Prometheus.Instance.Comp_Quest.ResourceRequestReserve(GameComponent_Prometheus_Quest.EnumResourceType.Power, Mod_EDPrometheus.Settings.LaserDrill.RequiredDrillShipPower);
                this.ShowLaserVisually();
                ((Thing)this.parent).Destroy((DestroyMode)0);
            }
            else
                Messages.Message("SteamGeyser not found to Remove.", (MessageTypeDef)MessageTypeDefOf.NegativeEvent, true);
        }

        public void TriggerLaser()
        {
            Messages.Message("SteamGeyser Created.", (MessageTypeDef)MessageTypeDefOf.TaskCompletion, true);
            GameComponent_Prometheus.Instance.Comp_Quest.ResourceRequestReserve(GameComponent_Prometheus_Quest.EnumResourceType.Power, Mod_EDPrometheus.Settings.LaserDrill.RequiredDrillShipPower);
            this.ShowLaserVisually();
            GenSpawn.Spawn(ThingDef.Named("SteamGeyser"), ((Thing)this.parent).Position, ((Thing)this.parent).Map, (WipeMode)0);
            ((Thing)this.parent).Destroy((DestroyMode)0);
        }

        private void ShowLaserVisually()
        {
            IntVec3 intVec3 = IntVec3.FromVector3(new Vector3((float)((Thing)this.parent).Position.x, (float)((Thing)this.parent).Position.y, (float)(((Thing)this.parent).Position.z - 2)));
            LaserDrillVisual laserDrillVisual = (LaserDrillVisual)GenSpawn.Spawn(ThingDef.Named("LaserDrillVisual"), intVec3, ((Thing)this.parent).Map, (WipeMode)0);
        }

        public Comp_LaserDrill()
        {
            this.m_CurrentStaus = Comp_LaserDrill.EnumLaserDrillState.Scanning;
            base.\u002Ector();
        }

        [CompilerGenerated]
        private void \u003CCompGetGizmosExtra\u003Eb__12_0()
        {
            this.TriggerLaser();
        }

        [CompilerGenerated]
        private void \u003CCompGetGizmosExtra\u003Eb__12_1()
        {
            this.TriggerLaserToFill();
        }

        [CompilerGenerated]
        [DebuggerHidden]
        private IEnumerable<Gizmo> \u003C\u003En__0()
        {
            return base.CompGetGizmosExtra();
        }

        public enum EnumLaserDrillState
        {
            Scanning,
            LowPower,
            ReadyToActivate,
        }

        [CompilerGenerated]
        private sealed class \u003CCompGetGizmosExtra\u003Ed__12 : 
      IEnumerable<Gizmo>,
      IEnumerable,
      IEnumerator<Gizmo>,
      IDisposable,
      IEnumerator
    {
      private int \u003C\u003E1__state;
      private Gizmo \u003C\u003E2__current;
      private int \u003C\u003El__initialThreadId;
      public Comp_LaserDrill \u003C\u003E4__this;
      private IEnumerator<Gizmo> \u003C\u003Es__1;
      private Gizmo \u003Cg\u003E5__2;
      private Command_Action \u003Cact\u003E5__3;
      private Command_Action \u003Cact\u003E5__4;

      [DebuggerHidden]
        public \u003CCompGetGizmosExtra\u003Ed__12(int _param1)
        {
            base.\u002Ector();
            this.\u003C\u003E1__state = _param1;
            this.\u003C\u003El__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        [DebuggerHidden]
        void IDisposable.Dispose()
        {
            switch (this.\u003C\u003E1__state)
        {
          case -3:
          case 1:
            try
                {
                }
                finally
                {
                    this.\u003C\u003Em__Finally1();
                }
                break;
            }
        }

        bool IEnumerator.MoveNext()
        {
            // ISSUE: fault handler
            try
            {
                switch (this.\u003C\u003E1__state)
          {
            case 0:
              this.\u003C\u003E1__state = -1;
                    this.\u003C\u003Es__1 = this.\u003C\u003E4__this.\u003C\u003En__0().GetEnumerator();
                    this.\u003C\u003E1__state = -3;
                    break;
            case 1:
              this.\u003C\u003E1__state = -3;
                    this.\u003Cg\u003E5__2 = (Gizmo)null;
                    break;
            case 2:
              this.\u003C\u003E1__state = -1;
                    this.\u003Cact\u003E5__3 = (Command_Action)null;
                    goto label_9;
            case 3:
              this.\u003C\u003E1__state = -1;
                    this.\u003Cact\u003E5__4 = (Command_Action)null;
                    goto label_12;
                    default:
              return false;
                }
                if (((IEnumerator)this.\u003C\u003Es__1).MoveNext())
          {
                    this.\u003Cg\u003E5__2 = this.\u003C\u003Es__1.Current;
                    this.\u003C\u003E2__current = this.\u003Cg\u003E5__2;
                    this.\u003C\u003E1__state = 1;
                    return true;
                }
                this.\u003C\u003Em__Finally1();
                this.\u003C\u003Es__1 = (IEnumerator<Gizmo>)null;
                if (this.\u003C\u003E4__this.m_CurrentStaus == Comp_LaserDrill.EnumLaserDrillState.ReadyToActivate)
          {
                    this.\u003Cact\u003E5__3 = new Command_Action();
                    // ISSUE: method pointer
                    this.\u003Cact\u003E5__3.action = (Nullable)new Action((object)this.\u003C\u003E4__this, __methodptr(\u003CCompGetGizmosExtra\u003Eb__12_0));
                    ((Command)this.\u003Cact\u003E5__3).icon = (Nullable)Comp_LaserDrill.UI_LASER_ACTIVATE;
                    ((Command)this.\u003Cact\u003E5__3).defaultLabel = (Nullable)"Activate Laser";
                    ((Command)this.\u003Cact\u003E5__3).defaultDesc = (Nullable)"Activate Laser";
                    ((Command)this.\u003Cact\u003E5__3).activateSound = (Nullable)SoundDef.Named("Click");
                    this.\u003C\u003E2__current = (Gizmo)this.\u003Cact\u003E5__3;
                    this.\u003C\u003E1__state = 2;
                    return true;
                }
            label_9:
                if (this.\u003C\u003E4__this.m_CurrentStaus == Comp_LaserDrill.EnumLaserDrillState.ReadyToActivate)
          {
                    this.\u003Cact\u003E5__4 = new Command_Action();
                    // ISSUE: method pointer
                    this.\u003Cact\u003E5__4.action = (Nullable)new Action((object)this.\u003C\u003E4__this, __methodptr(\u003CCompGetGizmosExtra\u003Eb__12_1));
                    ((Command)this.\u003Cact\u003E5__4).icon = (Nullable)Comp_LaserDrill.UI_LASER_ACTIVATEFILL;
                    ((Command)this.\u003Cact\u003E5__4).defaultLabel = (Nullable)"Activate Laser Fill";
                    ((Command)this.\u003Cact\u003E5__4).defaultDesc = (Nullable)"Activate Laser Fill";
                    ((Command)this.\u003Cact\u003E5__4).activateSound = (Nullable)SoundDef.Named("Click");
                    this.\u003C\u003E2__current = (Gizmo)this.\u003Cact\u003E5__4;
                    this.\u003C\u003E1__state = 3;
                    return true;
                }
            label_12:
                return false;
            }
        __fault
          {
                this.System\u002EIDisposable\u002EDispose();
            }
        }

        private void \u003C\u003Em__Finally1()
        {
            this.\u003C\u003E1__state = -1;
            if (this.\u003C\u003Es__1 == null)
          return;
            ((IDisposable)this.\u003C\u003Es__1).Dispose();
        }

        Gizmo IEnumerator<Gizmo>.Current
        {
            [DebuggerHidden]
            get
            {
                return this.\u003C\u003E2__current;
            }
        }

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return (object)this.\u003C\u003E2__current;
            }
        }

        [DebuggerHidden]
        IEnumerator<Gizmo> IEnumerable<Gizmo>.GetEnumerator()
        {
            Comp_LaserDrill.\u003CCompGetGizmosExtra\u003Ed__12 getGizmosExtraD12;
            if (this.\u003C\u003E1__state == -2 && this.\u003C\u003El__initialThreadId == Thread.CurrentThread.ManagedThreadId)
        {
                this.\u003C\u003E1__state = 0;
                getGizmosExtraD12 = this;
            }
        else
            {
                getGizmosExtraD12 = new Comp_LaserDrill.\u003CCompGetGizmosExtra\u003Ed__12(0);
                getGizmosExtraD12.\u003C\u003E4__this = this.\u003C\u003E4__this;
            }
            return (IEnumerator<Gizmo>)getGizmosExtraD12;
        }

        [DebuggerHidden]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.System\u002ECollections\u002EGeneric\u002EIEnumerable\u003CVerse\u002EGizmo\u003E\u002EGetEnumerator();
        }
    }
}
}
