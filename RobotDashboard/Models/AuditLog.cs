using System.ComponentModel.DataAnnotations;

namespace RobotDashboard.Models
{
    public class AuditLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Role { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Action { get; set; } = string.Empty;

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}