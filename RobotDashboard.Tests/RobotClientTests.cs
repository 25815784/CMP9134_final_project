using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Moq;
using Moq.Protected;
using RobotDashboard.DTOs;
using RobotDashboard.Services;

namespace RobotDashboard.Tests
{
    public class RobotClientTests
    {
        private HttpClient CreateHttpClient(HttpResponseMessage response)
        {
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response);

            return new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost")
            };
        }


        [Fact]
        public async Task GetStatusAsync_ReturnsDeserializedRobotStatus()
        {
            var expectedStatus = new RobotStatusDto
            {
                Id = "robot-1",
                Position = new PositionDto
                {
                    X = 5,
                    Y = 10
                },
                Battery = 87.5,
                Status = "Active"
            };

            var json = JsonSerializer.Serialize(expectedStatus);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var httpClient = CreateHttpClient(response);
            var robotClient = new RobotClient(httpClient);

            var result = await robotClient.GetStatusAsync();

            Assert.NotNull(result);
            Assert.Equal("robot-1", result.Id);
            Assert.NotNull(result.Position);
            Assert.Equal(5, result.Position!.X);
            Assert.Equal(10, result.Position.Y);
            Assert.Equal(87.5, result.Battery);
            Assert.Equal("Active", result.Status);
        }


        [Fact]
        public async Task MoveAsync_ReturnsTrue_WhenResponseIsSuccess()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var httpClient = CreateHttpClient(response);
            var robotClient = new RobotClient(httpClient);

            var result = await robotClient.MoveAsync(3, 4);

            Assert.True(result);
        }


        [Fact]
        public async Task ResetAsync_ReturnsTrue_WhenResponseIsSuccess()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var httpClient = CreateHttpClient(response);
            var robotClient = new RobotClient(httpClient);

            var result = await robotClient.ResetAsync();

            Assert.True(result);
        }


        [Fact]
        public async Task MoveAsync_ReturnsFalse_WhenResponseFails()
        {
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            var httpClient = CreateHttpClient(response);
            var robotClient = new RobotClient(httpClient);

            var result = await robotClient.MoveAsync(3, 4);

            Assert.False(result);
        }


        [Fact]
        public async Task ResetAsync_ReturnsFalse_WhenResponseFails()
        {
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            var httpClient = CreateHttpClient(response);
            var robotClient = new RobotClient(httpClient);

            var result = await robotClient.ResetAsync();

            Assert.False(result);
        }


        [Fact]
        public async Task GetStatusAsync_Throws_WhenResponseFails()
        {
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            var httpClient = CreateHttpClient(response);
            var robotClient = new RobotClient(httpClient);

            await Assert.ThrowsAsync<HttpRequestException>(() =>
                robotClient.GetStatusAsync());
        }


        [Fact]
        public async Task GetStatusAsync_HandlesBoundaryCoordinates()
        {
            var expectedStatus = new RobotStatusDto
            {
                Id = "robot-2",
                Position = new PositionDto
                {
                    X = 0,
                    Y = 100
                },
                Battery = 100,
                Status = "Idle"
            };

            var json = JsonSerializer.Serialize(expectedStatus);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var httpClient = CreateHttpClient(response);
            var robotClient = new RobotClient(httpClient);

            var result = await robotClient.GetStatusAsync();

            Assert.NotNull(result.Position);
            Assert.Equal(0, result.Position!.X);
            Assert.Equal(100, result.Position.Y);
        }
    }
}