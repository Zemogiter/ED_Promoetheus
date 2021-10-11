using EnhancedDevelopment.Prometheus.Core;
using RimWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Quest
{
    [StaticConstructorOnStartup]
    internal class Comp_EDSNTransponder :
    /*[StaticConstructorOnStartup]*/
    ThingComp
    {
        public IntVec3 DropLocation;
        private static Texture2D UI_Contact;

        static Comp_EDSNTransponder()
        {
            Comp_EDSNTransponder.UI_Contact = ContentFinder<Texture2D>.Get("UI/Quest/UI_Contact", true);
        }

        public virtual string CompInspectStringExtra()
        {
            return GameComponent_Prometheus_Quest.GetSingleLineResourceStatus() + base.CompInspectStringExtra();
        }

        public virtual IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            Comp_EDSNTransponder.\u003CCompGetGizmosExtra\u003Ed__4 getGizmosExtraD4 = new Comp_EDSNTransponder.\u003CCompGetGizmosExtra\u003Ed__4(-2);
            getGizmosExtraD4.\u003C\u003E4__this = this;
            return (IEnumerable<Gizmo>)getGizmosExtraD4;
        }

        public virtual void PostExposeData()
        {
            base.PostExposeData();
            // ISSUE: cast to a reference type
            Scribe_Values.Look<IntVec3>((M0 &) ref this.DropLocation, "DropLocation", (M0)new IntVec3(), false);
        }

        public Comp_EDSNTransponder()
        {
            this.DropLocation = IntVec3.Invalid;
            base.\u002Ector();
        }

        [CompilerGenerated]
        private void \u003CCompGetGizmosExtra\u003Eb__4_0()
        {
            GameComponent_Prometheus.Instance.Comp_Quest.ContactPrometheus(this.parent as Building);
        }

        [CompilerGenerated]
        [DebuggerHidden]
        private IEnumerable<Gizmo> \u003C\u003En__0()
        {
            return base.CompGetGizmosExtra();
        }

        [CompilerGenerated]
        [Serializable]
        private sealed class \u003C\u003Ec
    {
      public static readonly Comp_EDSNTransponder.\u003C\u003Ec \u003C\u003E9;
      public static Action \u003C\u003E9__4_1;

      static \u003C\u003Ec()
        {
            Comp_EDSNTransponder.\u003C\u003Ec.\u003C\u003E9 = new Comp_EDSNTransponder.\u003C\u003Ec();
        }

        public \u003C\u003Ec()
        {
            base.\u002Ector();
        }

        internal void \u003CCompGetGizmosExtra\u003Eb__4_1()
        {
            if (((KeyBindingDef)KeyBindingDefOf.ModifierIncrement_10x).IsDown || ((KeyBindingDef)KeyBindingDefOf.ModifierIncrement_100x).IsDown)
            {
                GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.ResourceUnits, 500);
                GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.Power, 5000);
                GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.NanoMaterials, 10);
            }
            else
            {
                GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.ResourceUnits, 50);
                GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.Power, 500);
                GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.NanoMaterials, 1);
            }
        }
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
      public Comp_EDSNTransponder \u003C\u003E4__this;
      private Command_Action \u003Cact\u003E5__1;
      private IEnumerator<Gizmo> \u003C\u003Es__2;
      private Gizmo \u003Cg\u003E5__3;
      private Command_Action \u003Cact2\u003E5__4;

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
                this.\u003C\u003Es__2 = this.\u003C\u003E4__this.\u003C\u003En__0().GetEnumerator();
                this.\u003C\u003E1__state = -3;
                break;
            case 1:
              this.\u003C\u003E1__state = -3;
                this.\u003Cg\u003E5__3 = (Gizmo)null;
                break;
            case 2:
              this.\u003C\u003E1__state = -1;
                if ((bool)DebugSettings.godMode)
                {
                    this.\u003Cact2\u003E5__4 = new Command_Action();
                    // ISSUE: method pointer
                    this.\u003Cact2\u003E5__4.action = (__Null)(Comp_EDSNTransponder.\u003C\u003Ec.\u003C\u003E9__4_1 ?? (Comp_EDSNTransponder.\u003C\u003Ec.\u003C\u003E9__4_1 = new Action((object)Comp_EDSNTransponder.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCompGetGizmosExtra\u003Eb__4_1))));
                    ((Command)this.\u003Cact2\u003E5__4).defaultLabel = (__Null)"Debug Resources";
                    ((Command)this.\u003Cact2\u003E5__4).defaultDesc = (__Null)"Debug Resources";
                    ((Command)this.\u003Cact2\u003E5__4).activateSound = (__Null)SoundDef.Named("Click");
                    this.\u003C\u003E2__current = (Gizmo)this.\u003Cact2\u003E5__4;
                    this.\u003C\u003E1__state = 3;
                    return true;
                }
                goto label_10;
            case 3:
              this.\u003C\u003E1__state = -1;
                this.\u003Cact2\u003E5__4 = (Command_Action)null;
                goto label_10;
                default:
              return false;
            }
            if (((IEnumerator)this.\u003C\u003Es__2).MoveNext())
          {
                this.\u003Cg\u003E5__3 = this.\u003C\u003Es__2.Current;
                this.\u003C\u003E2__current = this.\u003Cg\u003E5__3;
                this.\u003C\u003E1__state = 1;
                return true;
            }
            this.\u003C\u003Em__Finally1();
            this.\u003C\u003Es__2 = (IEnumerator<Gizmo>)null;
            this.\u003Cact\u003E5__1 = new Command_Action();
            // ISSUE: method pointer
            this.\u003Cact\u003E5__1.action = (__Null)new Action((object)this.\u003C\u003E4__this, __methodptr(\u003CCompGetGizmosExtra\u003Eb__4_0));
            ((Command)this.\u003Cact\u003E5__1).icon = (__Null)Comp_EDSNTransponder.UI_Contact;
            ((Command)this.\u003Cact\u003E5__1).defaultLabel = (__Null)"Contact Prometheus";
            ((Command)this.\u003Cact\u003E5__1).defaultDesc = (__Null)"Contact Prometheus";
            ((Command)this.\u003Cact\u003E5__1).activateSound = (__Null)SoundDef.Named("Click");
            this.\u003C\u003E2__current = (Gizmo)this.\u003Cact\u003E5__1;
            this.\u003C\u003E1__state = 2;
            return true;
        label_10:
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
        if (this.\u003C\u003Es__2 == null)
          return;
        ((IDisposable)this.\u003C\u003Es__2).Dispose();
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
        Comp_EDSNTransponder.\u003CCompGetGizmosExtra\u003Ed__4 getGizmosExtraD4;
        if (this.\u003C\u003E1__state == -2 && this.\u003C\u003El__initialThreadId == Thread.CurrentThread.ManagedThreadId)
        {
            this.\u003C\u003E1__state = 0;
            getGizmosExtraD4 = this;
        }
        else
        {
            getGizmosExtraD4 = new Comp_EDSNTransponder.\u003CCompGetGizmosExtra\u003Ed__4(0);
            getGizmosExtraD4.\u003C\u003E4__this = this.\u003C\u003E4__this;
        }
        return (IEnumerator<Gizmo>)getGizmosExtraD4;
    }

    [DebuggerHidden]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)this.System\u002ECollections\u002EGeneric\u002EIEnumerable\u003CVerse\u002EGizmo\u003E\u002EGetEnumerator();
    }
}
  }
}
