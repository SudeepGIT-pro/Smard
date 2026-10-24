using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smard.Models
{
    [Table("AspNetUserRoles")]
    public class AspNetUserRole
    {
        [MaxLength(450)]
        public string UserId { get; set; } = string.Empty;

        [MaxLength(450)]
        public string RoleId { get; set; } = string.Empty;

        public virtual AspNetRole? Role { get; set; }
    }
}
