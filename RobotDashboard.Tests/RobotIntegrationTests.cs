using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RobotDashboard.DTOs;
using RobotDashboard.Services;
using Xunit;

namespace RobotDashboard.Tests
{
    public class RobotIntegrationTests
        : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public RobotIntegrationTests(WebApplicationFactory<Program> factory)
        {
            var customFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IRobotClient, FakeRobotClient>();
                });
            });

            _client = customFactory.CreateClient();
        }

        [Fact]
        public async Task GetStatus_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/Robot/status");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }

    public class FakeRobotClient : IRobotClient
    {
        public Task<RobotStatusDto> GetStatusAsync()
        {
            var fakeStatus = new RobotStatusDto
            {
                Id = "FakeRobot01",
                Position = new PositionDto
                {
                    X = 0,
                    Y = 0
                },
                Battery = 100,
                Status = "IDLE"
            };

            return Task.FromResult(fakeStatus);
        }

        public Task<bool> MoveAsync(int x, int y)
        {
            return Task.FromResult(true);
        }

        public Task<bool> ResetAsync()
        {
            return Task.FromResult(true);
        }

        public Task<RobotMapDto> GetMapAsync()
        {
            var fakeMap = new RobotMapDto
            {
                Width = 21,
                Height = 21,
                Grid = new int[21][]
            };

            for (int i = 0; i < 21; i++)
            {
                fakeMap.Grid[i] = new int[21];
            }

            return Task.FromResult(fakeMap);
        }

        public Task<RobotSensorDto> GetSensorAsync()
        {
            var fakeSensor = new RobotSensorDto
            {
                Proximity = new ProximityDto
                {
                    N = 5,
                    S = 5,
                    E = 5,
                    W = 5
                },
                Lidar = new double[360]
            };

            return Task.FromResult(fakeSensor);
        }
    }
}