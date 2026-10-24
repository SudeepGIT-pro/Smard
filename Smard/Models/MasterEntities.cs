using System.ComponentModel.DataAnnotations.Schema;

namespace Smard.Models
{
    public interface IMasterWithCostDetails<TCostDetail> where TCostDetail : GenericCostDetailBase
    {
        int Id { get; set; }
        ICollection<TCostDetail> CostDetails { get; set; }
    }

    // 1. Wall Panel System
    [Table("WallPanelSystemMasters")]
    public class WallPanelSystemMaster : GenericMasterBase, IMasterWithCostDetails<WallPanelSystemCostDetail>
    {
        public ICollection<WallPanelSystemCostDetail> CostDetails { get; set; } = new List<WallPanelSystemCostDetail>();
    }

    [Table("WallPanelSystemCostDetails")]
    public class WallPanelSystemCostDetail : GenericCostDetailBase
    {
        public int WallPanelSystemMasterId { get; set; }

        [ForeignKey(nameof(WallPanelSystemMasterId))]
        public WallPanelSystemMaster? WallPanelSystemMaster { get; set; }
    }

    // 2. Ceiling System
    [Table("CeilingSystemMasters")]
    public class CeilingSystemMaster : GenericMasterBase, IMasterWithCostDetails<CeilingSystemCostDetail>
    {
        public ICollection<CeilingSystemCostDetail> CostDetails { get; set; } = new List<CeilingSystemCostDetail>();
    }

    [Table("CeilingSystemCostDetails")]
    public class CeilingSystemCostDetail : GenericCostDetailBase
    {
        public int CeilingSystemMasterId { get; set; }

        [ForeignKey(nameof(CeilingSystemMasterId))]
        public CeilingSystemMaster? CeilingSystemMaster { get; set; }
    }

    // 3. Peripheral Light
    [Table("PeripheralLightMasters")]
    public class PeripheralLightMaster : GenericMasterBase, IMasterWithCostDetails<PeripheralLightCostDetail>
    {
        public ICollection<PeripheralLightCostDetail> CostDetails { get; set; } = new List<PeripheralLightCostDetail>();
    }

    [Table("PeripheralLightCostDetails")]
    public class PeripheralLightCostDetail : GenericCostDetailBase
    {
        public int PeripheralLightMasterId { get; set; }

        [ForeignKey(nameof(PeripheralLightMasterId))]
        public PeripheralLightMaster? PeripheralLightMaster { get; set; }
    }

    // 4. Laminar Flow
    [Table("LaminarFlowMasters")]
    public class LaminarFlowMaster : GenericMasterBase, IMasterWithCostDetails<LaminarFlowCostDetail>
    {
        public ICollection<LaminarFlowCostDetail> CostDetails { get; set; } = new List<LaminarFlowCostDetail>();
    }

    [Table("LaminarFlowCostDetails")]
    public class LaminarFlowCostDetail : GenericCostDetailBase
    {
        public int LaminarFlowMasterId { get; set; }

        [ForeignKey(nameof(LaminarFlowMasterId))]
        public LaminarFlowMaster? LaminarFlowMaster { get; set; }
    }

    // 5. AVSU
    [Table("AVSUMasters")]
    public class AVSUMaster : GenericMasterBase, IMasterWithCostDetails<AVSUCostDetail>
    {
        public ICollection<AVSUCostDetail> CostDetails { get; set; } = new List<AVSUCostDetail>();
    }

    [Table("AVSUCostDetails")]
    public class AVSUCostDetail : GenericCostDetailBase
    {
        public int AVSUMasterId { get; set; }

        [ForeignKey(nameof(AVSUMasterId))]
        public AVSUMaster? AVSUMaster { get; set; }
    }

    // 6. Doors
    [Table("DoorsMasters")]
    public class DoorsMaster : GenericMasterBase, IMasterWithCostDetails<DoorsCostDetail>
    {
        public ICollection<DoorsCostDetail> CostDetails { get; set; } = new List<DoorsCostDetail>();
    }

    [Table("DoorsCostDetails")]
    public class DoorsCostDetail : GenericCostDetailBase
    {
        public int DoorsMasterId { get; set; }

        [ForeignKey(nameof(DoorsMasterId))]
        public DoorsMaster? DoorsMaster { get; set; }
    }

    // 7. Storage Cabinet
    [Table("StorageCabinetMasters")]
    public class StorageCabinetMaster : GenericMasterBase, IMasterWithCostDetails<StorageCabinetCostDetail>
    {
        public ICollection<StorageCabinetCostDetail> CostDetails { get; set; } = new List<StorageCabinetCostDetail>();
    }

    [Table("StorageCabinetCostDetails")]
    public class StorageCabinetCostDetail : GenericCostDetailBase
    {
        public int StorageCabinetMasterId { get; set; }

        [ForeignKey(nameof(StorageCabinetMasterId))]
        public StorageCabinetMaster? StorageCabinetMaster { get; set; }
    }

    // 8. Sink
    [Table("SinkMasters")]
    public class SinkMaster : GenericMasterBase, IMasterWithCostDetails<SinkCostDetail>
    {
        public ICollection<SinkCostDetail> CostDetails { get; set; } = new List<SinkCostDetail>();
    }

    [Table("SinkCostDetails")]
    public class SinkCostDetail : GenericCostDetailBase
    {
        public int SinkMasterId { get; set; }

        [ForeignKey(nameof(SinkMasterId))]
        public SinkMaster? SinkMaster { get; set; }
    }

    // 9. Work Station
    [Table("WorkStationMasters")]
    public class WorkStationMaster : GenericMasterBase, IMasterWithCostDetails<WorkStationCostDetail>
    {
        public ICollection<WorkStationCostDetail> CostDetails { get; set; } = new List<WorkStationCostDetail>();
    }

    [Table("WorkStationCostDetails")]
    public class WorkStationCostDetail : GenericCostDetailBase
    {
        public int WorkStationMasterId { get; set; }

        [ForeignKey(nameof(WorkStationMasterId))]
        public WorkStationMaster? WorkStationMaster { get; set; }
    }

    // 10. BHU
    [Table("BHUMasters")]
    public class BHUMaster : GenericMasterBase, IMasterWithCostDetails<BHUCostDetail>
    {
        public ICollection<BHUCostDetail> CostDetails { get; set; } = new List<BHUCostDetail>();
    }

    [Table("BHUCostDetails")]
    public class BHUCostDetail : GenericCostDetailBase
    {
        public int BHUMasterId { get; set; }

        [ForeignKey(nameof(BHUMasterId))]
        public BHUMaster? BHUMaster { get; set; }
    }

    // 11. Return Air System
    [Table("ReturnAirSystemMasters")]
    public class ReturnAirSystemMaster : GenericMasterBase, IMasterWithCostDetails<ReturnAirSystemCostDetail>
    {
        public ICollection<ReturnAirSystemCostDetail> CostDetails { get; set; } = new List<ReturnAirSystemCostDetail>();
    }

    [Table("ReturnAirSystemCostDetails")]
    public class ReturnAirSystemCostDetail : GenericCostDetailBase
    {
        public int ReturnAirSystemMasterId { get; set; }

        [ForeignKey(nameof(ReturnAirSystemMasterId))]
        public ReturnAirSystemMaster? ReturnAirSystemMaster { get; set; }
    }

    // 12. Windows
    [Table("WindowsMasters")]
    public class WindowsMaster : GenericMasterBase, IMasterWithCostDetails<WindowsCostDetail>
    {
        public ICollection<WindowsCostDetail> CostDetails { get; set; } = new List<WindowsCostDetail>();
    }

    [Table("WindowsCostDetails")]
    public class WindowsCostDetail : GenericCostDetailBase
    {
        public int WindowsMasterId { get; set; }

        [ForeignKey(nameof(WindowsMasterId))]
        public WindowsMaster? WindowsMaster { get; set; }
    }

    // 13. Counters
    [Table("CountersMasters")]
    public class CountersMaster : GenericMasterBase, IMasterWithCostDetails<CountersCostDetail>
    {
        public ICollection<CountersCostDetail> CostDetails { get; set; } = new List<CountersCostDetail>();
    }

    [Table("CountersCostDetails")]
    public class CountersCostDetail : GenericCostDetailBase
    {
        public int CountersMasterId { get; set; }

        [ForeignKey(nameof(CountersMasterId))]
        public CountersMaster? CountersMaster { get; set; }
    }

    // 14. LGS Panel System
    [Table("LGSPanelSystemMasters")]
    public class LGSPanelSystemMaster : GenericMasterBase, IMasterWithCostDetails<LGSPanelSystemCostDetail>
    {
        public ICollection<LGSPanelSystemCostDetail> CostDetails { get; set; } = new List<LGSPanelSystemCostDetail>();
    }

    [Table("LGSPanelSystemCostDetails")]
    public class LGSPanelSystemCostDetail : GenericCostDetailBase
    {
        public int LGSPanelSystemMasterId { get; set; }

        [ForeignKey(nameof(LGSPanelSystemMasterId))]
        public LGSPanelSystemMaster? LGSPanelSystemMaster { get; set; }
    }

    // 15. Fixed Furnitures
    [Table("FixedFurnituresMasters")]
    public class FixedFurnituresMaster : GenericMasterBase, IMasterWithCostDetails<FixedFurnituresCostDetail>
    {
        public ICollection<FixedFurnituresCostDetail> CostDetails { get; set; } = new List<FixedFurnituresCostDetail>();
    }

    [Table("FixedFurnituresCostDetails")]
    public class FixedFurnituresCostDetail : GenericCostDetailBase
    {
        public int FixedFurnituresMasterId { get; set; }

        [ForeignKey(nameof(FixedFurnituresMasterId))]
        public FixedFurnituresMaster? FixedFurnituresMaster { get; set; }
    }

    // 16. Loose Furnitures
    [Table("LooseFurnituresMasters")]
    public class LooseFurnituresMaster : GenericMasterBase, IMasterWithCostDetails<LooseFurnituresCostDetail>
    {
        public ICollection<LooseFurnituresCostDetail> CostDetails { get; set; } = new List<LooseFurnituresCostDetail>();
    }

    [Table("LooseFurnituresCostDetails")]
    public class LooseFurnituresCostDetail : GenericCostDetailBase
    {
        public int LooseFurnituresMasterId { get; set; }

        [ForeignKey(nameof(LooseFurnituresMasterId))]
        public LooseFurnituresMaster? LooseFurnituresMaster { get; set; }
    }

    // 17. CSSD Furnitures
    [Table("CSSDFurnituresMasters")]
    public class CSSDFurnituresMaster : GenericMasterBase, IMasterWithCostDetails<CSSDFurnituresCostDetail>
    {
        public ICollection<CSSDFurnituresCostDetail> CostDetails { get; set; } = new List<CSSDFurnituresCostDetail>();
    }

    [Table("CSSDFurnituresCostDetails")]
    public class CSSDFurnituresCostDetail : GenericCostDetailBase
    {
        public int CSSDFurnituresMasterId { get; set; }

        [ForeignKey(nameof(CSSDFurnituresMasterId))]
        public CSSDFurnituresMaster? CSSDFurnituresMaster { get; set; }
    }

    // 18. Job Works
    [Table("JobWorksMasters")]
    public class JobWorksMaster : GenericMasterBase, IMasterWithCostDetails<JobWorksCostDetail>
    {
        public ICollection<JobWorksCostDetail> CostDetails { get; set; } = new List<JobWorksCostDetail>();
    }

    [Table("JobWorksCostDetails")]
    public class JobWorksCostDetail : GenericCostDetailBase
    {
        public int JobWorksMasterId { get; set; }

        [ForeignKey(nameof(JobWorksMasterId))]
        public JobWorksMaster? JobWorksMaster { get; set; }
    }
}
