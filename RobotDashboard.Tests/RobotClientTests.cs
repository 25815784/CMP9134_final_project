using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Moq;
using Moq.Protected;
using RobotDashboard.DTOs;
using RobotDashboard.Services;
using Xunit;

namespace RobotDashboard.Tests
{
    public class RobotClientTests
    {
        private HttpClient CreateHttpClient(HttpResponseMessage response)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
            return new HttpClient(handlerMock.Object) { BaseAddress = new Uri("http://localhost") };
        }

        [Fact] public async Task GetStatusAsync_ReturnsDeserializedRobotStatus() {
            var expected = new RobotStatusDto { Id = "r1", Position = new PositionDto { X = 5, Y = 10 }, Battery = 87.5, Status = "Active" };
            var client = new RobotClient(CreateHttpClient(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonSerializer.Serialize(expected), Encoding.UTF8, "application/json") }), null);
            var result = await client.GetStatusAsync();
            Assert.Equal("r1", result.Id);
        }

        [Fact] public async Task MoveAsync_ReturnsTrue_WhenResponseIsSuccess() {
            var client = new RobotClient(CreateHttpClient(new HttpResponseMessage(HttpStatusCode.OK)), null);
            Assert.True(await client.MoveAsync(3, 4));
        }

        [Fact] public async Task ResetAsync_ReturnsTrue_WhenResponseIsSuccess() {
            var client = new RobotClient(CreateHttpClient(new HttpResponseMessage(HttpStatusCode.OK)), null);
            Assert.True(await client.ResetAsync());
        }

        [Fact] public async Task GetMapAsync_ReturnsMapData() {
            var expected = new RobotMapDto { Width = 21, Height = 21 };
            var client = new RobotClient(CreateHttpClient(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonSerializer.Serialize(expected), Encoding.UTF8, "application/json") }), null);
            var result = await client.GetMapAsync();
            Assert.Equal(21, result.Width);
        }

        [Fact] public async Task GetSensorAsync_ReturnsSensorData() {
            var expected = new RobotSensorDto { Proximity = new ProximityDto { N = 5 } };
            var client = new RobotClient(CreateHttpClient(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonSerializer.Serialize(expected), Encoding.UTF8, "application/json") }), null);
            var result = await client.GetSensorAsync();
            Assert.Equal(5, result.Proximity!.N);
        }

        [Fact] public async Task MoveAsync_ReturnsFalse_WhenResponseFails() {
            var client = new RobotClient(CreateHttpClient(new HttpResponseMessage(HttpStatusCode.InternalServerError)), null);
            Assert.False(await client.MoveAsync(3, 4));
        }

        [Fact] public async Task ResetAsync_ReturnsFalse_WhenResponseFails() {
            var client = new RobotClient(CreateHttpClient(new HttpResponseMessage(HttpStatusCode.InternalServerError)), null);
            Assert.False(await client.ResetAsync());
        }

        [Fact] public async Task GetStatusAsync_Throws_WhenResponseFails() {
            var client = new RobotClient(CreateHttpClient(new HttpResponseMessage(HttpStatusCode.InternalServerError)), null);
            await Assert.ThrowsAsync<HttpRequestException>(() => client.GetStatusAsync());
        }

        [Fact] public async Task GetStatusAsync_HandlesBoundaryCoordinates() {
            var expected = new RobotStatusDto { Id = "r2", Position = new PositionDto { X = 0, Y = 100 }, Battery = 100, Status = "Idle" };
            var client = new RobotClient(CreateHttpClient(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonSerializer.Serialize(expected), Encoding.UTF8, "application/json") }), null);
            var result = await client.GetStatusAsync();
            Assert.Equal(0, result.Position!.X);
            Assert.Equal(100, result.Position.Y);
        }

        [Fact] public async Task SendWithRetryAsync_RetriesOn503() {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .SetupSequence<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("{}", Encoding.UTF8, "application/json") });
            var client = new RobotClient(new HttpClient(handlerMock.Object) { BaseAddress = new Uri("http://localhost") }, null);
            var result = await client.GetStatusAsync();
            Assert.NotNull(result);
        }
    }
}