namespace RobotDashboard.DTOs;
public class RobotStatusDto
{
    public string? Id { get; set; }
    public PositionDto? Position { get; set; }
    public double Battery { get; set; }
    public string? Status { get; set; }
}

public class PositionDto
{
    public int X { get; set; }
    public int Y { get; set; }
}