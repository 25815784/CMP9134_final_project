namespace RobotDashboard.DTOs
{
    public class RobotSensorDto
    {
        public ProximityDto? Proximity { get; set; }
        public double[]? Lidar { get; set; }
    }

    public class ProximityDto
    {
        public double N { get; set; }
        public double S { get; set; }
        public double E { get; set; }
        public double W { get; set; }
    }
}