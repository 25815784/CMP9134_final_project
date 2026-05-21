using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RobotDashboard.Data;
using RobotDashboard.DTOs;
using Xunit;

namespace RobotDashboard.Tests
{
    public class MissionStatsIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public MissionStatsIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<RobotDashboardContext>(options =>
                    {
                        options.UseInMemoryDatabase("MissionStats_Test_DB");
                    });
                });
            }).CreateClient();
        }

        [Fact]
        public async Task Calculate_ShouldReturnSuccessfulReconMission()
        {
            var request = new { missionType = 1, distance = 20.0, battery = 80.0, payload = 0.0 };
            var response = await _client.PostAsJsonAsync("/api/MissionStats/calculate", request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<MissionStatsResponseDto>();
            Assert.NotNull(result);
            Assert.Equal("Recon", result!.MissionCategory);
            Assert.Equal(48, result.Score);
        }

        [Fact]
        public async Task Calculate_ShouldReturnSuccessfulTransportMission_WithHeavyPayload()
        {
            var request = new { missionType = 2, distance = 30.0, battery = 80.0, payload = 60.0 };
            var response = await _client.PostAsJsonAsync("/api/MissionStats/calculate", request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<MissionStatsResponseDto>();
            Assert.NotNull(result);
            Assert.Equal("Transport", result!.MissionCategory);
            Assert.Equal(54, result.Score);
        }

        [Fact]
        public async Task Calculate_ShouldReturnBadRequest_WhenBatteryIsMissing()
        {
            var request = new { missionType = 1, distance = 20.0, payload = 0.0 };
            var response = await _client.PostAsJsonAsync("/api/MissionStats/calculate", request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Calculate_ShouldCapScoreAt100()
        {
            var request = new { missionType = 1, distance = 100.0, battery = 100.0, payload = 0.0 };
            var response = await _client.PostAsJsonAsync("/api/MissionStats/calculate", request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<MissionStatsResponseDto>();
            Assert.NotNull(result);
            Assert.Equal(100, result!.Score);
        }
    }
}