using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smard.Models
{
    public abstract class GenericMasterBase
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string TypeName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string SystemDescription { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal DimensionLength { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DimensionHeight { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string DimensionSize { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal DimensionArea { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MaterialTotalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MaterialTotalPerSquareMeter { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LabourTotalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LabourTotalPerSquareMeter { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal GrandTotalPerSquareMeter { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TransportationPercent { get; set; } = 1m;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TransportationAmount { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string TransportationRemarks { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal WaterElectricityPercent { get; set; } = 2m;

        [Column(TypeName = "decimal(18,2)")]
        public decimal WaterElectricityAmount { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string WaterElectricityRemarks { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ToolsEquipmentPercent { get; set; } = 5m;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ToolsEquipmentAmount { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string ToolsEquipmentRemarks { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ProfitPercent { get; set; } = 20m;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ProfitAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalRateAmount { get; set; }

        public bool IsActive { get; set; } = true;

        [StringLength(100)]
        public string? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(100)]
        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
