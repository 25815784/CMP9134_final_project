using System.Net.Http;
using System.Text;
using System.Text.Json;

public class RobotClient : IRobotClient
{
    private readonly HttpClient _httpClient;

    public RobotClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetStatusAsync()
    {
        var response = await _httpClient.GetAsync("/status");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<bool> MoveAsync(int x, int y)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(new { x, y }),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync("/move", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ResetAsync()
    {
        var response = await _httpClient.PostAsync("/reset", null);
        return response.IsSuccessStatusCode;
    }
}
