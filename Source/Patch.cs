using HarmonyLib;
using Verse;

namespace EnhancedDevelopment.Prometheus.Patch
{
    internal abstract class Patch
    {
        protected abstract bool ShouldPatchApply();

        protected abstract void ApplyPatch(Harmony harmony = null);

        protected abstract string PatchDescription();

		[System.Obsolete]
		public void ApplyPatchIfRequired(Harmony harmony = null)
        {
            string str = "Shields.Patch.ApplyPatchIfRequired: ";
            if (this.ShouldPatchApply())
            {
                Log.Message(str + "Applying Patch: " + this.PatchDescription(), false);
                this.ApplyPatch(harmony);
                Log.Message(str + "Applied Patch: " + this.PatchDescription(), false);
            }
            else
                Log.Message(str + "Skipping Applying Patch: " + this.PatchDescription(), false);
        }

        protected Patch()
        {
            base.\u002Ector();
        }
    }
}