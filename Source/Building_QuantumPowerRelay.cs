using EnhancedDevelopment.Prometheus.Core;
using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace EnhancedDevelopment.Prometheus.Power
{
    [StaticConstructorOnStartup]
    internal class Building_QuantumPowerRelay :
    /*[StaticConstructorOnStartup]*/
    Building
    {
		private static readonly Vector2 BarSize;
        private static readonly Material BatteryBarFilledMat;
        private static readonly Material BatteryBarUnfilledMat;
        private CompPowerBattery m_CompPowerBattery;

        private CompPowerBattery CompPowerBattery
        {
            get
            {
                if (this.m_CompPowerBattery == null)
                    this.m_CompPowerBattery = (CompPowerBattery)((ThingWithComps)this).GetComp<CompPowerBattery>();
                return this.m_CompPowerBattery;
            }
        }

        public virtual void Draw()
        {
            base.Draw();
            GenDraw.FillableBarRequest fillableBarRequest = new GenDraw.FillableBarRequest();
            fillableBarRequest.center = Vector3.op_Addition(((Thing)this).DrawPos, Vector3.op_Multiply(Vector3.up, 0.1f));
            fillableBarRequest.size = Building_QuantumPowerRelay.BarSize;
            fillableBarRequest.fillPercent = ((float)((double)this.CompPowerBattery.StoredEnergy / this.CompPowerBattery.Props.storedEnergyMax));
            fillableBarRequest.filledMat = Building_QuantumPowerRelay.BatteryBarFilledMat;
            fillableBarRequest.unfilledMat = Building_QuantumPowerRelay.BatteryBarUnfilledMat;
            fillableBarRequest.margin = (float)0.150000005960464;
            Rot4 rotation = ((Thing)this).Rotation;
            ((Rot4) rotation).Rotate((RotationDirection)1);
            fillableBarRequest.rotation = rotation;
            GenDraw.DrawFillableBar(fillableBarRequest);
        }

        public virtual string GetInspectString()
        {
            return ((ThingWithComps)this).GetInspectString() + Environment.NewLine + "Ship Power: " + (object)GameComponent_Prometheus.Instance.Comp_Quest.ResourceGetReserveStatus(GameComponent_Prometheus_Quest.EnumResourceType.Power);
        }

        public virtual void Tick()
        {
            ((ThingWithComps)this).Tick();
            float num = (float)(this.CompPowerBattery.Props.storedEnergyMax / 2.0);
            int ammount = (int)(this.CompPowerBattery.Props.storedEnergyMax / 4.0);
            if ((double)this.CompPowerBattery.StoredEnergy - (double)ammount >= (double)num)
            {
                this.CompPowerBattery.DrawPower((float)ammount);
                GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.Power, ammount);
            }
            if (!GameComponent_Prometheus.Instance.Comp_Quest.ShipSystem_PowerDistribution.IsShipToSurfacePowerAvalable() || (double)this.CompPowerBattery.StoredEnergy + (double)ammount > (double)num)
                return;
            this.CompPowerBattery.AddEnergy((float)GameComponent_Prometheus.Instance.Comp_Quest.ResourceRequestReserve(GameComponent_Prometheus_Quest.EnumResourceType.Power, ammount));
        }

        static Building_QuantumPowerRelay()
        {
            Building_QuantumPowerRelay.BarSize = new Vector2(1.3f, 0.4f);
            Building_QuantumPowerRelay.BatteryBarFilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.9f, 0.85f, 0.2f), false);
            Building_QuantumPowerRelay.BatteryBarUnfilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.3f, 0.3f, 0.3f), false);
        }
    }
}
