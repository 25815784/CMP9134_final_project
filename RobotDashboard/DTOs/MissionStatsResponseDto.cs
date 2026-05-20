namespace RobotDashboard.DTOs
{
    public class MissionStatsResponseDto
    {
        public string MissionCategory { get; set; } = string.Empty;
        public double Score { get; set; }
        public bool Capped { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}