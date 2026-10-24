using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smard.Models
{
    public abstract class GenericCostDetailBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string CostType { get; set; } = string.Empty;

        public int SlNo { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string ItemDescription { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string Specification { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string Unit { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Qty { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitRate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Remarks { get; set; } = string.Empty;
    }
}
