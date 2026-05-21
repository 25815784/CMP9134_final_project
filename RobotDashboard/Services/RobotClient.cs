using System.Net;
using System.Net.Http.Json;
using RobotDashboard.DTOs;

namespace RobotDashboard.Services
{
    public class RobotClient : IRobotClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RobotClient>? _logger;

        
        public RobotClient(HttpClient httpClient, ILogger<RobotClient>? logger = null)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<RobotStatusDto> GetStatusAsync()
        {
            var result = await SendWithRetryAsync(() => _httpClient.GetFromJsonAsync<RobotStatusDto>("api/status"));
            return result ?? throw new HttpRequestException("Received empty status from robot.");
        }

        public async Task<bool> MoveAsync(int x, int y)
        {
            var response = await SendWithRetryAsync(() => _httpClient.PostAsJsonAsync("api/move", new { x, y }));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ResetAsync()
        {
            var response = await SendWithRetryAsync(() => _httpClient.PostAsync("api/reset", null));
            return response.IsSuccessStatusCode;
        }

        public async Task<RobotMapDto> GetMapAsync()
        {
            var result = await SendWithRetryAsync(() => _httpClient.GetFromJsonAsync<RobotMapDto>("api/map"));
            return result ?? throw new HttpRequestException("Received empty map from robot.");
        }

        public async Task<RobotSensorDto> GetSensorAsync()
        {
            var result = await SendWithRetryAsync(() => _httpClient.GetFromJsonAsync<RobotSensorDto>("api/sensor"));
            return result ?? throw new HttpRequestException("Received empty sensor data from robot.");
        }

        private async Task<T> SendWithRetryAsync<T>(Func<Task<T>> action)
        {
            int maxRetries = 3;
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    var result = await action();
                    if (result == null) continue; 
                    return result;
                }
                catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    _logger?.LogWarning("Robot API returned 503. Retry {RetryCount} of {MaxRetries}...", i + 1, maxRetries);
                    await Task.Delay(2000);
                }
            }
            throw new HttpRequestException("Robot API is unavailable after multiple retries.");
        }
    }
}