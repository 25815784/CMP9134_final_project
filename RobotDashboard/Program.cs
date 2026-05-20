using RobotDashboard.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to container
builder.Services.AddControllers();

// Register Robot client
builder.Services.AddHttpClient<IRobotClient, RobotClient>((sp, client) =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var baseUrl = configuration["RobotSimulator:BaseUrl"];

    if (string.IsNullOrWhiteSpace(baseUrl))
    {
        throw new InvalidOperationException(
            "RobotSimulator:BaseUrl is not configured in environment variables."
        );
    }

    client.BaseAddress = new Uri(baseUrl);
});

// Register Mission Statistics service
builder.Services.AddScoped<IMissionStatsService, MissionStatsService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Always enable Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }