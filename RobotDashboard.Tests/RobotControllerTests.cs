using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using RobotDashboard.Controllers;
using RobotDashboard.Data;
using RobotDashboard.Services;
using Xunit;

namespace RobotDashboard.Tests
{
    public class RobotControllerTests
    {
        private readonly RobotController _controller;
        private readonly Mock<IRobotClient> _mockRobotClient;
        private readonly RobotDashboardContext _context;

        public RobotControllerTests()
        {
            _mockRobotClient = new Mock<IRobotClient>();

            // --- SETUP IN-MEMORY DATABASE FOR TESTING ---
            var options = new DbContextOptionsBuilder<RobotDashboardContext>()
                .UseInMemoryDatabase(databaseName: "TestLogDb")
                .Options;
            
            _context = new RobotDashboardContext(options);

            // Pass both the mock client AND the test database to the controller
            _controller = new RobotController(_mockRobotClient.Object, _context);
        }

        [Fact]
        public async Task GetStatus_ReturnsOkResult()
        {
            // Arrange
            _mockRobotClient.Setup(c => c.GetStatusAsync())
                .ReturnsAsync(new DTOs.RobotStatusDto());

            // Act
            var result = await _controller.GetStatus();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}