using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smard.Models
{
    [Table("AspNetRoles")]
    public class AspNetRole
    {
        [Key]
        [MaxLength(450)]
        public string Id { get; set; } = string.Empty;

        [MaxLength(256)]
        public string? Name { get; set; }

        [MaxLength(256)]
        public string? NormalizedName { get; set; }

        public string? ConcurrencyStamp { get; set; }
        public virtual ICollection<AspNetUserRole> UserRoles { get; set; } = new List<AspNetUserRole>();
    }
}
