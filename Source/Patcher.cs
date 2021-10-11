//using EnhancedDevelopment.Prometheus.Patch.Patches;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Verse;

namespace EnhancedDevelopment.Prometheus.Patch
{
    [StaticConstructorOnStartup]
    internal class Patcher
    {
        static Patcher()
        {
            string str = "EnhancedDevelopment.Prometheus.Patches.Patcher(): ";
            List<EnhancedDevelopment.Prometheus.Patch.Patch> patchList = new List<EnhancedDevelopment.Prometheus.Patch.Patch>();
            patchList.Add((EnhancedDevelopment.Prometheus.Patch.Patch)new PatchProjectile());
            patchList.Add((EnhancedDevelopment.Prometheus.Patch.Patch)new PatchThingWithComps_Nano());
            patchList.Add((EnhancedDevelopment.Prometheus.Patch.Patch)new PatchDropPodIncoming());
            patchList.Add((EnhancedDevelopment.Prometheus.Patch.Patch)new PatchNanoMaterialCost());
        }

        public static void LogNULL(object objectToTest, string name, bool logSucess = false)
        {
            if (objectToTest == null)
            {
                Log.Error(name + " Is NULL.", false);
            }
            else
            {
                if (!logSucess)
                    return;
                Log.Message(name + " Is Not NULL.", false);
            }
        }

        public Patcher()
        {
            base.\u002Ector();
        }

    {
      
}
}
