using RobotDashboard.DTOs;

namespace RobotDashboard.Services
{
    public class MissionStatsService : IMissionStatsService
    {
        public MissionStatsResponseDto Calculate(MissionStatsRequestDto request)
        {
            // We use .Value because the DTO properties are nullable for validation purposes
            var missionType = request.MissionType!.Value;
            var distance = request.Distance!.Value;
            var battery = request.Battery!.Value;
            var payload = request.Payload ?? 0;

            // --- GUARD CLAUSES (Workshop Task 4.2) ---
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

            // --- CALCULATION LOGIC ---
            double rawScore = missionType switch
            {
                1 => (distance * 2) + (battery * 0.1), // Recon
                2 => (distance + (battery * 0.05)) + (payload > 50 ? 20 : 5), // Transport
                _ => 0
            };

            // --- SCORE CAPPING (Workshop Task 4.3) ---
            double cappedScore = Math.Min(rawScore, 100);

            return new MissionStatsResponseDto
            {
                MissionCategory = GetMissionCategory(missionType),
                Score = cappedScore,
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