using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using RobotDashboard.DTOs;
using Xunit;

namespace RobotDashboard.Tests
{
    public class MissionStatsIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public MissionStatsIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Calculate_ShouldReturnSuccessfulReconMission()
        {
            var request = new
            {
                missionType = 1,
                distance = 20,
                battery = 80,
                payload = 0
            };

            var response = await _client.PostAsJsonAsync("/api/MissionStats/calculate", request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<MissionStatsResponseDto>();

            Assert.NotNull(result);
            Assert.Equal("Recon", result!.MissionCategory);
            Assert.Equal(48, result.Score);
            Assert.False(result.Capped);
            Assert.Equal("Operational", result.Status);
        }

        [Fact]
        public async Task Calculate_ShouldReturnSuccessfulTransportMission_WithHeavyPayload()
        {
            var request = new
            {
                missionType = 2,
                distance = 30,
                battery = 80,
                payload = 60
            };

            var response = await _client.PostAsJsonAsync("/api/MissionStats/calculate", request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<MissionStatsResponseDto>();

            Assert.NotNull(result);
            Assert.Equal("Transport", result!.MissionCategory);
            Assert.Equal(54, result.Score);
            Assert.False(result.Capped);
            Assert.Equal("Operational", result.Status);
        }

        [Fact]
        public async Task Calculate_ShouldReturnBadRequest_WhenBatteryIsMissing()
        {
            var request = new
            {
                missionType = 1,
                distance = 20,
                payload = 0
            };

            var response = await _client.PostAsJsonAsync("/api/MissionStats/calculate", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Calculate_ShouldCapScoreAt100()
        {
            var request = new
            {
                missionType = 1,
                distance = 100,
                battery = 100,
                payload = 0
            };

            var response = await _client.PostAsJsonAsync("/api/MissionStats/calculate", request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<MissionStatsResponseDto>();

            Assert.NotNull(result);
            Assert.Equal(100, result!.Score);
            Assert.True(result.Capped);
        }
    }
}