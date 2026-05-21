using System.Threading.Tasks;
using RobotDashboard.DTOs;

namespace RobotDashboard.Services
{
    public interface IRobotClient
    {
        Task<RobotStatusDto> GetStatusAsync();
        Task<bool> MoveAsync(int x, int y);
        Task<bool> ResetAsync();
        Task<RobotMapDto> GetMapAsync();
        Task<RobotSensorDto> GetSensorAsync();
    }
}