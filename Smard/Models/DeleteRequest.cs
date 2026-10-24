using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smard.Models
{
    [Table("DeleteRequests")]
    public class DeleteRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string RequestedByUserId { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string RequestedByUserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string MasterName { get; set; } = string.Empty; // e.g. "Decking System", "Wall Panel System"

        [Required]
        [MaxLength(100)]
        public string ControllerName { get; set; } = string.Empty; // e.g. "WallPanelSystem"

        public int RecordId { get; set; }

        public int TypeIndex { get; set; }

        [Required]
        [MaxLength(250)]
        public string TypeName { get; set; } = string.Empty; // e.g. "DECKING WITH CEMENT BOARD ONLY"

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending"; // "Pending", "Approved", "Rejected"

        public DateTime RequestedOn { get; set; } = DateTime.Now;

        [MaxLength(150)]
        public string? ReviewedByUserId { get; set; }

        [MaxLength(200)]
        public string? ReviewedByUserName { get; set; }

        public DateTime? ReviewedOn { get; set; }

        [MaxLength(1000)]
        public string? RejectReason { get; set; }

        [MaxLength(500)]
        public string? ActionUrl { get; set; }
    }
}
