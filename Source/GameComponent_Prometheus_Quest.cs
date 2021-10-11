using EnhancedDevelopment.Prometheus.Fabrication;
using EnhancedDevelopment.Prometheus.Power;
using EnhancedDevelopment.Prometheus.Quest;
using EnhancedDevelopment.Prometheus.Quest.Dialog;
using EnhancedDevelopment.Prometheus.Quest.ShipSystems;
using EnhancedDevelopment.Prometheus.Settings;
using EnhancedDevelopment.Prometheus.Transporter;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace EnhancedDevelopment.Prometheus.Core
{
    internal class GameComponent_Prometheus_Quest : GameComponent_BaseClass
    {
        public ShipSystem_Fabrication ShipSystem_Fabrication;
        public ShipSystem_PowerDistribution ShipSystem_PowerDistribution;
        public ShipSystem_Shield ShipSystem_Shield;
        public ShipSystem_Transport ShipSystem_Transport;
        public int m_QuestStatus = 0;
        private readonly List<ResourceUnit> m_ResourcesToTransport = new List<ResourceUnit>();
        private readonly Dictionary<GameComponent_Prometheus_Quest.EnumResourceType, int> m_ResourcesStored = new Dictionary<GameComponent_Prometheus_Quest.EnumResourceType, int>();
        public List<ShipSystem> m_ShipSystems = new List<ShipSystem>();

        public GameComponent_Prometheus_Quest()
        {
            this.m_ResourcesStored.Add(GameComponent_Prometheus_Quest.EnumResourceType.Power, 0);
            this.m_ResourcesStored.Add(GameComponent_Prometheus_Quest.EnumResourceType.ResourceUnits, 0);
            this.m_ResourcesStored.Add(GameComponent_Prometheus_Quest.EnumResourceType.NanoMaterials, 0);
            this.m_ResourcesStored.Add(GameComponent_Prometheus_Quest.EnumResourceType.DropPods, 0);
            this.m_ResourcesStored.Add(GameComponent_Prometheus_Quest.EnumResourceType.UtilityDrones, 0);
            this.m_ResourcesStored.Add(GameComponent_Prometheus_Quest.EnumResourceType.SolarCells, 0);
            this.ShipSystem_Fabrication = new ShipSystem_Fabrication();
            this.m_ShipSystems.Add((ShipSystem)this.ShipSystem_Fabrication);
            this.ShipSystem_PowerDistribution = new ShipSystem_PowerDistribution();
            this.m_ShipSystems.Add((ShipSystem)this.ShipSystem_PowerDistribution);
            this.ShipSystem_Shield = new ShipSystem_Shield();
            this.m_ShipSystems.Add((ShipSystem)this.ShipSystem_Shield);
            this.ShipSystem_Transport = new ShipSystem_Transport();
            this.m_ShipSystems.Add((ShipSystem)this.ShipSystem_Transport);
        }

        public override void ExposeData()
        {
            // ISSUE: cast to a reference type
            Scribe_Values.Look<int>(this.m_QuestStatus, "m_QuestStatus", (M0)0, false);
            this.m_ResourcesStored.ToList<KeyValuePair<GameComponent_Prometheus_Quest.EnumResourceType, int>>().ForEach((Action<KeyValuePair<GameComponent_Prometheus_Quest.EnumResourceType, int>>)(x =>
            {
                int num = x.Value;
                // ISSUE: cast to a reference type
                Scribe_Values.Look<int>(num, "m_ResourcesStored_" + x.Key.ToString(), (M0)0, false);
                this.m_ResourcesStored[x.Key] = num;
            }));
            this.m_ShipSystems.ForEach((Action<ShipSystem>)(s => s.ExposeData()));
        }

		[Obsolete]
		public override void TickOnInterval()
        {
            if (this.m_QuestStatus == 0 && (CommsConsoleUtility.PlayerHasPoweredCommsConsole() || Mod_EDPrometheus.Settings.Quest.Quest_OverrideConsoleRequired))
            {
                ++this.m_QuestStatus;
                this.ContactPrometheus();
            }
            if (GenCollection.Any<ResourceUnit>((List<M0>)this.m_ResourcesToTransport))
            {
                int _ResourceStackSizeAdded = 0;
                this.m_ResourcesToTransport.ForEach((Action<ResourceUnit>)(r =>
                {
                    if (((Thing)r).Destroyed)
                        return;
                    _ResourceStackSizeAdded += (int)((Thing)r).stackCount;
                    GameComponent_Prometheus.Instance.Comp_Quest.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.ResourceUnits, (int)((Thing)r).stackCount);
                    Comp_Transporter.DisplayTransportEffect((Thing)r);
                    IntVec3 position = ((Thing)r).Position;
                    Map map = ((Thing)r).Map;
                    ((Thing)r).Destroy((DestroyMode)0);
                    ((MapDrawer)map.mapDrawer).MapMeshDirty(position, (MapMeshFlag)1, true, false);
                }));
                Messages.Message("Transported " + _ResourceStackSizeAdded.ToString() + " resources.", (MessageTypeDef)MessageTypeDefOf.TaskCompletion, true);
            }
            this.m_ResourcesToTransport.Clear();
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();
            this.UpdateAllResearch();
        }

        public override int GetTickInterval() => 2000;

        public int ResourceGetReserveStatus(
          GameComponent_Prometheus_Quest.EnumResourceType resourceType)
        {
            return this.m_ResourcesStored[resourceType];
        }

        public void ResourceAddToReserves(
          GameComponent_Prometheus_Quest.EnumResourceType resourceType,
          int ammount)
        {
            this.m_ResourcesStored[resourceType] = this.m_ResourcesStored[resourceType] + ammount;
        }

        public int ResourceRequestReserve(
          GameComponent_Prometheus_Quest.EnumResourceType resourceType,
          int ammount)
        {
            if (this.m_ResourcesStored[resourceType] >= ammount)
            {
                this.m_ResourcesStored[resourceType] -= ammount;
                return ammount;
            }
            int num = this.m_ResourcesStored[resourceType];
            this.m_ResourcesStored[resourceType] -= num;
            return num;
        }

        public static string GetSingleLineResourceStatus() => "Nano Materials: " + (object)GameComponent_Prometheus.Instance.Comp_Quest.ResourceGetReserveStatus(GameComponent_Prometheus_Quest.EnumResourceType.NanoMaterials) + " RU: " + (object)GameComponent_Prometheus.Instance.Comp_Quest.ResourceGetReserveStatus(GameComponent_Prometheus_Quest.EnumResourceType.ResourceUnits) + " Power: " + (object)GameComponent_Prometheus.Instance.Comp_Quest.ResourceGetReserveStatus(GameComponent_Prometheus_Quest.EnumResourceType.Power);

        public void TagMaterialsForTransport(ResourceUnit resource)
        {
            if (this.m_ResourcesToTransport.Contains(resource))
                return;
            this.m_ResourcesToTransport.Add(resource);
        }

		[Obsolete]
		public void ContactPrometheus(Building contactSource = null)
        {
            Log.Message("Contacting Prometheus", false);
            switch (this.m_QuestStatus)
            {
                case 0:
                    ++this.m_QuestStatus;
                    break;
                case 1:
                    ++this.m_QuestStatus;
                    Find.WindowStack.Add((Window)new Dialog_0_Generic(Translator.Translate("EDE_Dialog_Title_1_SignalDetection"), Translator.Translate("EDE_Dialog_1_SignalDetection")));
                    break;
                case 2:
                    ++this.m_QuestStatus;
                    Find.WindowStack.Add((Window)new Dialog_0_Generic(Translator.Translate("EDE_Dialog_Title_2_FirstContact"), Translator.Translate("EDE_Dialog_2_FirstContact")));
                    Building_QuantumPowerRelay quantumPowerRelay = (Building_QuantumPowerRelay)ThingMaker.MakeThing(ThingDef.Named("QuantumPowerRelay"), (ThingDef)null);
					List<Thing> thingList = new List<Thing>
					{
						(Thing)quantumPowerRelay
					};
					DropPodUtility.DropThingsNear(((Thing)contactSource).Position, ((Thing)contactSource).Map, (IEnumerable<Thing>)thingList, 110, false, false, true);
                    break;
                case 3:
                    if (this.m_ResourcesStored[GameComponent_Prometheus_Quest.EnumResourceType.Power] >= Mod_EDPrometheus.Settings.Quest.InitialShipSetup_PowerRequired)
                    {
                        ++this.m_QuestStatus;
                        this.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.Power, -Mod_EDPrometheus.Settings.Quest.InitialShipSetup_PowerRequired);
                        ++this.ShipSystem_PowerDistribution.CurrentLevel;
                        this.ContactPrometheus();
                        break;
                    }
                    Find.WindowStack.Add((Window)new Dialog_1_PowerRequest(Translator.Translate("EDE_Dialog_Title_3_InitialCharge"), string.Format(Translator.Translate("EDE_Dialog_3_InitialCharge"), (object)this.m_ResourcesStored[GameComponent_Prometheus_Quest.EnumResourceType.Power].ToString(), (object)Mod_EDPrometheus.Settings.Quest.InitialShipSetup_PowerRequired.ToString()), contactSource));
                    break;
                case 4:
                    if (this.ResourceGetReserveStatus(GameComponent_Prometheus_Quest.EnumResourceType.ResourceUnits) >= Mod_EDPrometheus.Settings.Quest.InitialShipSetup_ResourcesRequired)
                    {
                        ++this.m_QuestStatus;
                        this.ResourceAddToReserves(GameComponent_Prometheus_Quest.EnumResourceType.ResourceUnits, -Mod_EDPrometheus.Settings.Quest.InitialShipSetup_ResourcesRequired);
                        ++this.ShipSystem_Fabrication.CurrentLevel;
                        this.ContactPrometheus();
                        break;
                    }
                    Find.WindowStack.Add((Window)new Dialog_0_Generic(Translator.Translate("EDE_Dialog_Title_4_NeedResources"), string.Format(Translator.Translate("EDE_Dialog_4_NeedResources"), (object)this.ResourceGetReserveStatus(GameComponent_Prometheus_Quest.EnumResourceType.ResourceUnits).ToString(), (object)Mod_EDPrometheus.Settings.Quest.InitialShipSetup_ResourcesRequired.ToString())));
                    break;
                case 5:
                    ++this.m_QuestStatus;
                    Find.WindowStack.Add((Window)new Dialog_0_Generic(Translator.Translate("EDE_Dialog_Title_5_ExecutingBurn"), Translator.Translate("EDE_Dialog_5_ExecutingBurn")));
                    break;
                case 6:
                    ++this.m_QuestStatus;
                    Find.WindowStack.Add((Window)new Dialog_0_Generic(Translator.Translate("EDE_Dialog_Title_6_ShipStabilised"), Translator.Translate("EDE_Dialog_6_ShipStabilised")));
                    break;
                case 7:
                    Find.WindowStack.Add((Window)new Dialog_Prometheus());
                    break;
                default:
                    Find.WindowStack.Add((Window)new Dialog_0_Generic("EDETestString", Translator.Translate("EDETestString")));
                    this.m_QuestStatus = 1;
                    break;
            }
            this.UpdateAllResearch();
        }

        public void UpdateAllResearch()
        {
            ResearchHelper.UpdateQuestStatusResearch();
            GameComponent_Prometheus.Instance.Comp_Quest.m_ShipSystems.ForEach((Action<ShipSystem>)(s => s.ApplyRequiredResearchUnlocks()));
        }

        public enum EnumResourceType
        {
            Power,
            ResourceUnits,
            NanoMaterials,
            DropPods,
            UtilityDrones,
            SolarCells,
        }
    }
}