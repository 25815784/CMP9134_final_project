using Microsoft.EntityFrameworkCore;
using RobotDashboard.Data;
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

// Register Database Context
builder.Services.AddDbContext<RobotDashboardContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Always enable Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Enable static files such as dashboard.html from wwwroot
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();

// Automatically apply database migrations on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RobotDashboardContext>();
    db.Database.Migrate();
}

app.Run();

public partial class Program { }