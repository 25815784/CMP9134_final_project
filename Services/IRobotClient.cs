public interface IRobotClient
{
    Task<RobotStatusDto> GetStatusAsync();
    Task<bool> MoveAsync(int x, int y);
    Task<bool> ResetAsync();
}
