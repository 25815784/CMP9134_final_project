using RobotDashboard.DTOs;

namespace RobotDashboard.Services
{
    public class MissionStatsService : IMissionStatsService
    {
        private readonly bool _isAdvancedStatsEnabled;

        public MissionStatsService(IConfiguration configuration)
        {
            var flagValue = configuration["FF_ADVANCED_STATS"] ?? "false";
            _isAdvancedStatsEnabled = flagValue.Equals("true", StringComparison.OrdinalIgnoreCase);
        }

        public MissionStatsResponseDto Calculate(MissionStatsRequestDto request)
        {
            if (!_isAdvancedStatsEnabled)
            {
                return new MissionStatsResponseDto
                {
                    Status = "Feature not yet available.",
                    Score = 0,
                    Capped = false,
                    MissionCategory = string.Empty
                };
            }

            var missionType = request.MissionType!.Value;
            var distance = request.Distance!.Value;
            var battery = request.Battery!.Value;
            var payload = request.Payload ?? 0;

            if (battery <= 0)
            {
                return new MissionStatsResponseDto
                {
                    MissionCategory = GetMissionCategory(missionType),
                    Score = 0,
                    Capped = false,
                    Status = "Battery depleted"
                };
            }

            double rawScore = missionType switch
            {
                1 => (distance * 2) + (battery * 0.1),
                2 => (distance + (battery * 0.05)) + (payload > 50 ? 20 : 5),
                _ => 0
            };

            return new MissionStatsResponseDto
            {
                MissionCategory = GetMissionCategory(missionType),
                Score = Math.Min(rawScore, 100),
                Capped = rawScore > 100,
                Status = "Operational"
            };
        }

        private static string GetMissionCategory(int missionType)
        {
            return missionType == 1 ? "Recon" : "Transport";
        }
    }
}