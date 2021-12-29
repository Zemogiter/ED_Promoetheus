using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Verse;
using static HarmonyLib.Harmony;

namespace EnhancedDevelopment.Prometheus.Patch
{
    [StaticConstructorOnStartup]
    internal class Patcher
    {
        static Patcher()
        {
            string _LogLocation = "EnhancedDevelopment.Prometheus.Patches.Patcher(): ";

            Log.Message(_LogLocation + "Starting.");

			//Create List of Patches
			List<Patch> _Patches = new List<Patch>
			{
				new Patches.PatchProjectile(),
				new Patches.PatchThingWithComps_Nano(),
				new Patches.PatchDropPodIncoming(),
				new Patches.PatchNanoMaterialCost()
			};

			//Create Harmony Instance
			//HarmonyLib.Harmony _Harmony = HarmonyLib.Harmony.("EnhancedDevelopment.Prometheus");

			//Iterate Patches
			//_Patches.ForEach(p => p.ApplyPatchIfRequired(Harmony));

            Log.Message(_LogLocation + "Complete.");
        }

        /// <summary>
        /// Debug Logging Helper
        /// </summary>
        /// <param name="objectToTest"></param>
        /// <param name="name"></param>
        /// <param name="logSucess"></param>
        public static void LogNULL(object objectToTest, String name, bool logSucess = false)
        {
            if (objectToTest == null)
            {
                Log.Error(name + " Is NULL.");
            }
            else if (logSucess)
            {
                Log.Message(name + " Is Not NULL.");
            }
        }

    }

}
