using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using RobotDashboard.Services;
using RobotDashboard.DTOs;

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

    // ✅ Fake Robot Client
    public class FakeRobotClient : IRobotClient
    {
        public Task<RobotStatusDto> GetStatusAsync()
        {
            var fakeStatus = new RobotStatusDto
            {

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
    }
}