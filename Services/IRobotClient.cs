public interface IRobotClient
{
    Task<string> GetStatusAsync();
    Task<bool> MoveAsync(int x, int y);
    Task<bool> ResetAsync();
}
