using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smard.Models
{
    [Table("UserNotifications")]
    public class UserNotification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;

        [Required]
        public string NotifType { get; set; } = string.Empty;

        [Required]
        public bool IsRead { get; set; } = false;

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string? ActionUrl { get; set; }
    }
}
