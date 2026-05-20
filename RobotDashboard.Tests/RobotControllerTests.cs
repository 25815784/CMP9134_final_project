using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using RobotDashboard.Controllers;
using RobotDashboard.Services;
using RobotDashboard.DTOs;
using System.Threading.Tasks;

public class RobotControllerTests
{
    private readonly Mock<IRobotClient> _mockClient;
    private readonly RobotController _controller;

    public RobotControllerTests()
    {
        _mockClient = new Mock<IRobotClient>();
        _controller = new RobotController(_mockClient.Object);
    }

    [Fact]
    public async Task GetStatus_ReturnsOk_WithStatus()
    {
        var status = new RobotStatusDto();
        _mockClient.Setup(c => c.GetStatusAsync())
                   .ReturnsAsync(status);

        var result = await _controller.GetStatus();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(status, okResult.Value);
    }

    [Fact]
    public async Task Move_ReturnsOk_WhenMoveSuccessful()
    {
        _mockClient.Setup(c => c.MoveAsync(1, 1))
                   .ReturnsAsync(true);

        var result = await _controller.Move(1, 1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Move_ReturnsBadRequest_WhenMoveFails()
    {
        _mockClient.Setup(c => c.MoveAsync(1, 1))
                   .ReturnsAsync(false);

        var result = await _controller.Move(1, 1);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Reset_ReturnsOk_WhenSuccessful()
    {
        _mockClient.Setup(c => c.ResetAsync())
                   .ReturnsAsync(true);

        var result = await _controller.Reset();

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Reset_ReturnsBadRequest_WhenFails()
    {
        _mockClient.Setup(c => c.ResetAsync())
                   .ReturnsAsync(false);

        var result = await _controller.Reset();

        Assert.IsType<BadRequestObjectResult>(result);
    }
}