using RobotDashboard.DTOs;

namespace RobotDashboard.Services
{
    public interface IMissionStatsService
    {
        MissionStatsResponseDto Calculate(MissionStatsRequestDto request);
    }
}