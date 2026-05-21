using Microsoft.EntityFrameworkCore;
using RobotDashboard.Data;
using RobotDashboard.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient<IRobotClient, RobotClient>((sp, client) =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var baseUrl = configuration["RobotSimulator:BaseUrl"];
    if (string.IsNullOrWhiteSpace(baseUrl)) throw new InvalidOperationException("BaseUrl missing");
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddScoped<IMissionStatsService, MissionStatsService>();


if (builder.Environment.EnvironmentName != "Testing")
{
    builder.Services.AddDbContext<RobotDashboardContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RobotDashboardContext>();
    if (db.Database.ProviderName == "Microsoft.EntityFrameworkCore.SqlServer")
    {
        db.Database.Migrate();
    }
}

app.Run();

public partial class Program { }