using Microsoft.EntityFrameworkCore;
using RobotDashboard.Models;

namespace RobotDashboard.Data
{
    public class RobotDashboardContext : DbContext
    {
        public RobotDashboardContext(DbContextOptions<RobotDashboardContext> options)
            : base(options)
        {
        }

        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}