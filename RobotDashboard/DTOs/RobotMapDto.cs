namespace RobotDashboard.DTOs
{
    public class RobotMapDto
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int[][]? Grid { get; set; }
    }
}