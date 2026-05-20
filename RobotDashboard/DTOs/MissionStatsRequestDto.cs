using System.ComponentModel.DataAnnotations;

namespace RobotDashboard.DTOs
{
    public class MissionStatsRequestDto
    {
        [Required]
        [Range(1, 2, ErrorMessage = "MissionType must be 1 (Recon) or 2 (Transport).")]
        public int? MissionType { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Distance must be 0 or greater.")]
        public double? Distance { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Battery must be between 0 and 100.")]
        public double? Battery { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Payload must be 0 or greater.")]
        public double? Payload { get; set; }
    }
}