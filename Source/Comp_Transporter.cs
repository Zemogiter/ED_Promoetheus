using EnhancedDevelopment.Prometheus.Core;
using RimWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Transporter
{
    [StaticConstructorOnStartup]
    internal class Comp_Transporter :
    /*[StaticConstructorOnStartup]*/
    ThingComp
    {
        private static Texture2D UI_TRANSPORT_PEOPLE;
        private static Texture2D UI_TRANSPORT_RESOURCES;
        private static Texture2D UI_TRANSPORT_RECALL;

        static Comp_Transporter()
        {
            Comp_Transporter.UI_TRANSPORT_PEOPLE = ContentFinder<Texture2D>.Get("UI/Transport/TransportPeople", true);
            Comp_Transporter.UI_TRANSPORT_RESOURCES = ContentFinder<Texture2D>.Get("UI/Transport/TransportResources", true);
            Comp_Transporter.UI_TRANSPORT_RECALL = ContentFinder<Texture2D>.Get("UI/Transport/TransportRecall", true);
        }

        public virtual IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            Comp_Transporter.\u003CCompGetGizmosExtra\u003Ed__4 getGizmosExtraD4 = new Comp_Transporter.\u003CCompGetGizmosExtra\u003Ed__4(-2);
            getGizmosExtraD4.\u003C\u003E4__this = this;
            return (IEnumerable<Gizmo>)getGizmosExtraD4;
        }

        private void TransportColonists()
        {
            // ISSUE: method pointer
            List<Pawn> list = ((MapPawns)((Thing)this.parent).Map.mapPawns).PawnsInFaction(Faction.OfPlayer).Where<Pawn>(new Func<Pawn, bool>((object)this, __methodptr(\u003CTransportColonists\u003Eb__5_0))).ToList<Pawn>();
            if (!GenCollection.Any<Pawn>((List<M0>)list))
                return;
            // ISSUE: method pointer
            list.ForEach(new Action<Pawn>((object)this, __methodptr(\u003CTransportColonists\u003Eb__5_1)));
        }

        private void TransportThings()
        {
            // ISSUE: method pointer
            // ISSUE: method pointer
            List<Thing> list = GenRadial.RadialDistinctThingsAround(((Thing)this.parent).Position, ((Thing)this.parent).Map, 5f, true).Where<Thing>(Comp_Transporter.\u003C\u003Ec.\u003C\u003E9__6_0 ?? (Comp_Transporter.\u003C\u003Ec.\u003C\u003E9__6_0 = new Func<Thing, bool>((object)Comp_Transporter.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CTransportThings\u003Eb__6_0)))).Where<Thing>(Comp_Transporter.\u003C\u003Ec.\u003C\u003E9__6_1 ?? (Comp_Transporter.\u003C\u003Ec.\u003C\u003E9__6_1 = new Func<Thing, bool>((object)Comp_Transporter.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CTransportThings\u003Eb__6_1)))).ToList<Thing>();
            if (!GenCollection.Any<Thing>((List<M0>)list))
                return;
            // ISSUE: method pointer
            list.ForEach(new Action<Thing>((object)this, __methodptr(\u003CTransportThings\u003Eb__6_2)));
        }

        private void TransportRecall()
        {
            // ISSUE: method pointer
            GameComponent_Prometheus.Instance.Comp_Transporter.TransportBuffer.ForEach(new Action<Thing>((object)this, __methodptr(\u003CTransportRecall\u003Eb__7_0)));
            GameComponent_Prometheus.Instance.Comp_Transporter.TransportBuffer.Clear();
        }

        public static void DisplayTransportEffect(Thing thingToTransport)
        {
            MoteMaker.MakeStaticMote(thingToTransport.Position, thingToTransport.Map, (ThingDef)ThingDefOf.Mote_ExplosionFlash, 10f);
        }

        public Comp_Transporter()
        {
            base.\u002Ector();
        }

        [CompilerGenerated]
        private void \u003CCompGetGizmosExtra\u003Eb__4_0()
        {
            this.TransportColonists();
        }

        [CompilerGenerated]
        private void \u003CCompGetGizmosExtra\u003Eb__4_1()
        {
            this.TransportThings();
        }

        [CompilerGenerated]
        private void \u003CCompGetGizmosExtra\u003Eb__4_2()
        {
            this.TransportRecall();
        }

        [CompilerGenerated]
        [DebuggerHidden]
        private IEnumerable<Gizmo> \u003C\u003En__0()
        {
            return base.CompGetGizmosExtra();
        }

        [CompilerGenerated]
        private bool \u003CTransportColonists\u003Eb__5_0(Pawn t)
        {
            IntVec3 position = ((Thing)t).Position;
            return ((IntVec3)ref position).InHorDistOf(((Thing)this.parent).Position, 5f);
        }

        [CompilerGenerated]
        private void \u003CTransportColonists\u003Eb__5_1(Pawn _x)
        {
            GameComponent_Prometheus.Instance.Comp_Transporter.TransportBuffer.Add((Thing)_x);
            Comp_Transporter.DisplayTransportEffect((Thing)_x);
            ((Entity)_x).DeSpawn((DestroyMode)0);
            ((MapDrawer)((Thing)this.parent).Map.mapDrawer).MapMeshDirty(((Thing)_x).Position, (MapMeshFlag)1, true, false);
        }

        [CompilerGenerated]
        private void \u003CTransportThings\u003Eb__6_2(Thing _x)
        {
            GameComponent_Prometheus.Instance.Comp_Transporter.TransportBuffer.Add(_x);
            Comp_Transporter.DisplayTransportEffect(_x);
            ((Entity)_x).DeSpawn((DestroyMode)0);
            ((MapDrawer)((Thing)this.parent).Map.mapDrawer).MapMeshDirty(_x.Position, (MapMeshFlag)1, true, false);
        }

        [CompilerGenerated]
        private void \u003CTransportRecall\u003Eb__7_0(Thing _X)
        {
            GenPlace.TryPlaceThing(_X, ((Thing)this.parent).Position, ((Thing)this.parent).Map, (ThingPlaceMode)1, (Action<Thing, int>)null, (Predicate<IntVec3>)null);
            Comp_Transporter.DisplayTransportEffect(_X);
            if (!(_X is Pawn pawn))
                return;
            ((Thing)pawn).SetFactionDirect(Faction.OfPlayer);
        }

        [CompilerGenerated]
        private sealed class \u003CCompGetGizmosExtra\u003Ed__4 : 
      IEnumerable<Gizmo>,
      IEnumerable,
      IEnumerator<Gizmo>,
      IDisposable,
      IEnumerator
    {
      private int \u003C\u003E1__state;
      private Gizmo \u003C\u003E2__current;
      private int \u003C\u003El__initialThreadId;
      public Comp_Transporter \u003C\u003E4__this;
      private IEnumerator<Gizmo> \u003C\u003Es__1;
      private Gizmo \u003Cg\u003E5__2;
      private Command_Action \u003Cact\u003E5__3;
      private Command_Action \u003Cact\u003E5__4;
      private Command_Action \u003Cact\u003E5__5;

      [DebuggerHidden]
        public \u003CCompGetGizmosExtra\u003Ed__4(int _param1)
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
                    this.\u003Cact\u003E5__4 = new Command_Action();
                    // ISSUE: method pointer
                    this.\u003Cact\u003E5__4.action = (__Null)new Action((object)this.\u003C\u003E4__this, __methodptr(\u003CCompGetGizmosExtra\u003Eb__4_1));
                    ((Command)this.\u003Cact\u003E5__4).icon = (__Null)Comp_Transporter.UI_TRANSPORT_RESOURCES;
                    ((Command)this.\u003Cact\u003E5__4).defaultLabel = (__Null)"Things";
                    ((Command)this.\u003Cact\u003E5__4).defaultDesc = (__Null)"Transport Things to the ship";
                    ((Command)this.\u003Cact\u003E5__4).activateSound = (__Null)SoundDef.Named("Click");
                    this.\u003C\u003E2__current = (Gizmo)this.\u003Cact\u003E5__4;
                    this.\u003C\u003E1__state = 3;
                    return true;
            case 3:
              this.\u003C\u003E1__state = -1;
                    this.\u003Cact\u003E5__4 = (Command_Action)null;
                    this.\u003Cact\u003E5__5 = new Command_Action();
                    // ISSUE: method pointer
                    this.\u003Cact\u003E5__5.action = (__Null)new Action((object)this.\u003C\u003E4__this, __methodptr(\u003CCompGetGizmosExtra\u003Eb__4_2));
                    ((Command)this.\u003Cact\u003E5__5).icon = (__Null)Comp_Transporter.UI_TRANSPORT_RECALL;
                    ((Command)this.\u003Cact\u003E5__5).defaultLabel = (__Null)"Recall";
                    ((Command)this.\u003Cact\u003E5__5).defaultDesc = (__Null)"Recall Colonists and Things from the ship";
                    ((Command)this.\u003Cact\u003E5__5).activateSound = (__Null)SoundDef.Named("Click");
                    this.\u003C\u003E2__current = (Gizmo)this.\u003Cact\u003E5__5;
                    this.\u003C\u003E1__state = 4;
                    return true;
            case 4:
              this.\u003C\u003E1__state = -1;
                    this.\u003Cact\u003E5__5 = (Command_Action)null;
                    goto label_11;
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
                if (GameComponent_Prometheus.Instance.Comp_Quest.ShipSystem_Transport.IsTransporterUnlocked())
                {
                    this.\u003Cact\u003E5__3 = new Command_Action();
                    // ISSUE: method pointer
                    this.\u003Cact\u003E5__3.action = (__Null)new Action((object)this.\u003C\u003E4__this, __methodptr(\u003CCompGetGizmosExtra\u003Eb__4_0));
                    ((Command)this.\u003Cact\u003E5__3).icon = (__Null)Comp_Transporter.UI_TRANSPORT_PEOPLE;
                    ((Command)this.\u003Cact\u003E5__3).defaultLabel = (__Null)"Colonists";
                    ((Command)this.\u003Cact\u003E5__3).defaultDesc = (__Null)"Transport Colonists to the ship";
                    ((Command)this.\u003Cact\u003E5__3).activateSound = (__Null)SoundDef.Named("Click");
                    this.\u003C\u003E2__current = (Gizmo)this.\u003Cact\u003E5__3;
                    this.\u003C\u003E1__state = 2;
                    return true;
                }
            label_11:
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
        ((IDisposable) this.\u003C\u003Es__1).Dispose();
      }

      Gizmo IEnumerator<Gizmo>.Current
      {
        [DebuggerHidden] get
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
        [DebuggerHidden] get
        {
          return (object) this.\u003C\u003E2__current;
        }
      }

      [DebuggerHidden]
      IEnumerator<Gizmo> IEnumerable<Gizmo>.GetEnumerator()
      {
        Comp_Transporter.\u003CCompGetGizmosExtra\u003Ed__4 getGizmosExtraD4;
        if (this.\u003C\u003E1__state == -2 && this.\u003C\u003El__initialThreadId == Thread.CurrentThread.ManagedThreadId)
        {
          this.\u003C\u003E1__state = 0;
          getGizmosExtraD4 = this;
        }
        else
        {
          getGizmosExtraD4 = new Comp_Transporter.\u003CCompGetGizmosExtra\u003Ed__4(0);
          getGizmosExtraD4.\u003C\u003E4__this = this.\u003C\u003E4__this;
        }
        return (IEnumerator<Gizmo>) getGizmosExtraD4;
      }

      [DebuggerHidden]
      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) this.System\u002ECollections\u002EGeneric\u002EIEnumerable\u003CVerse\u002EGizmo\u003E\u002EGetEnumerator();
      }
    }

    [CompilerGenerated]
    [Serializable]
    private sealed class \u003C\u003Ec
    {
      public static readonly Comp_Transporter.\u003C\u003Ec \u003C\u003E9;
      public static Func<Thing, bool> \u003C\u003E9__6_0;
      public static Func<Thing, bool> \u003C\u003E9__6_1;

      static \u003C\u003Ec()
      {
        Comp_Transporter.\u003C\u003Ec.\u003C\u003E9 = new Comp_Transporter.\u003C\u003Ec();
      }

      public \u003C\u003Ec()
      {
        base.\u002Ector();
      }

      internal bool \u003CTransportThings\u003Eb__6_0(Thing x)
      {
        return ((ThingDef) x.def).category == 2;
      }

      internal bool \u003CTransportThings\u003Eb__6_1(Thing x)
      {
        return x.Spawned;
      }
    }
  }
}
