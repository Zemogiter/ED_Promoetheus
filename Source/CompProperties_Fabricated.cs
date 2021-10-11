using Verse;

namespace EnhancedDevelopment.Prometheus.Fabrication
{
    internal class CompProperties_Fabricated : CompProperties
    {
        public int RequiredPower = 0;
        public int RequiredMaterials = 0;
        public int RequiredWork = 0;
        public bool PreventConstruction = false;

        public CompProperties_Fabricated() => this.compClass = (EditorNullableAttribute)typeof(Comp_Fabricated);
    }
}
