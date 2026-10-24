using System.ComponentModel.DataAnnotations;

namespace Smard.Models
{
    public class GenericMasterSaveRequest
    {
        public int? Id { get; set; }

        [Required]
        public string TypeName { get; set; } = string.Empty;

        public string SystemDescription { get; set; } = string.Empty;

        public decimal DimensionLength { get; set; }
        public decimal DimensionHeight { get; set; }

        public string DimensionSize { get; set; } = string.Empty;
        public decimal DimensionArea { get; set; }

        public decimal MaterialTotalAmount { get; set; }
        public decimal MaterialTotalPerSquareMeter { get; set; }
        public decimal LabourTotalAmount { get; set; }
        public decimal LabourTotalPerSquareMeter { get; set; }
        public decimal GrandTotalPerSquareMeter { get; set; }

        public decimal TransportationPercent { get; set; } = 1m;
        public decimal TransportationAmount { get; set; }
        public string TransportationRemarks { get; set; } = string.Empty;

        public decimal WaterElectricityPercent { get; set; } = 2m;
        public decimal WaterElectricityAmount { get; set; }
        public string WaterElectricityRemarks { get; set; } = string.Empty;

        public decimal ToolsEquipmentPercent { get; set; } = 5m;
        public decimal ToolsEquipmentAmount { get; set; }
        public string ToolsEquipmentRemarks { get; set; } = string.Empty;

        public decimal ProfitPercent { get; set; } = 20m;
        public decimal ProfitAmount { get; set; }
        public decimal TotalRateAmount { get; set; }

        public bool IsActive { get; set; } = true;

        public List<GenericCostDetailRequest> MaterialCosts { get; set; } = new();
        public List<GenericCostDetailRequest> LabourCosts { get; set; } = new();
    }

    public class GenericCostDetailRequest
    {
        public int SlNo { get; set; }
        public string ItemDescription { get; set; } = string.Empty;
        public string Specification { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal Qty { get; set; }
        public decimal UnitRate { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; } = string.Empty;
    }

    public class DeleteTypeRequest
    {
        public int Id { get; set; }
    }
}
