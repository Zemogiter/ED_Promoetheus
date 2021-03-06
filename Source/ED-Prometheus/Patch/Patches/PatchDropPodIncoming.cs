using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Verse;
using HarmonyLib;
using UnityEngine;
using RimWorld;

namespace EnhancedDevelopment.Prometheus.Patch.Patches
{
    class PatchDropPodIncoming : Patch
    {

        protected override void ApplyPatch(Harmony harmony = null)
        {

            this.ApplyImpactPatch(harmony);

        }

        protected override string PatchDescription()
        {
            return "PatchDropPodIncoming";
        }

        protected override bool ShouldPatchApply()
        {
            return true;
            //return Mod_EnhancedOptions.Settings.Plant24HEnabled;
        }
        
        private void ApplyImpactPatch(Harmony harmony)
        {

            //Get the Launch Method
            //Type[] _TypeArray = new Type[] { typeof(Verse.Thing), typeof(Vector3), typeof(LocalTargetInfo), typeof(LocalTargetInfo), typeof(ProjectileHitFlags), typeof(Thing), typeof(ThingDef) };
            MethodInfo _DropPodIncomingImpact = typeof(DropPodIncoming).GetMethod("Impact", BindingFlags.Instance | BindingFlags.NonPublic);
            Patcher.LogNULL(_DropPodIncomingImpact, "_DropPodIncomingImpact");

            //Get the Launch Prefix Patch
            MethodInfo _DropPodIncomingImpactPrefix = typeof(PatchDropPodIncoming).GetMethod("ImpactPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_DropPodIncomingImpactPrefix, "_DropPodIncomingImpactPrefix");

            //Apply the Prefix Patches
            harmony.Patch(_DropPodIncomingImpact, new HarmonyMethod(_DropPodIncomingImpactPrefix), null);
        }

        
    }
}