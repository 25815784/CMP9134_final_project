using Microsoft.AspNetCore.Mvc;
using RobotDashboard.Data;
using RobotDashboard.DTOs;
using RobotDashboard.Models;
using RobotDashboard.Services;

namespace RobotDashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RobotController : ControllerBase
    {
        private readonly IRobotClient _robotClient;
        private readonly RobotDashboardContext _context;

        public RobotController(IRobotClient robotClient, RobotDashboardContext context)
        {
            _robotClient = robotClient;
            _context = context;
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var status = await _robotClient.GetStatusAsync();
            return Ok(status);
        }

        [HttpPost("move")]
        public async Task<IActionResult> Move(int x, int y, [FromHeader(Name = "X-User-Role")] string role = "Commander")
        {
            var result = await _robotClient.MoveAsync(x, y);

            if (!result)
            {
                return BadRequest("Move failed");
            }

            var log = new AuditLog
            {
                Role = role,
                Action = $"Moved to ({x}, {y})",
                Timestamp = DateTime.UtcNow
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();

            return Ok("Move successful");
        }

        [HttpPost("reset")]
        public async Task<IActionResult> Reset([FromHeader(Name = "X-User-Role")] string role = "Commander")
        {
            var result = await _robotClient.ResetAsync();

            if (!result)
            {
                return BadRequest("Reset failed");
            }

            var log = new AuditLog
            {
                Role = role,
                Action = "Robot reset",
                Timestamp = DateTime.UtcNow
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();

            return Ok("Robot reset successful");
        }

        [HttpGet("map")]
        public async Task<ActionResult<RobotMapDto>> GetMap()
        {
            var map = await _robotClient.GetMapAsync();
            return Ok(map);
        }

        [HttpGet("sensor")]
        public async Task<ActionResult<RobotSensorDto>> GetSensor()
        {
            var sensor = await _robotClient.GetSensorAsync();
            return Ok(sensor);
        }

        [HttpGet("logs")]
        public ActionResult<IEnumerable<AuditLog>> GetLogs()
        {
            var logs = _context.AuditLogs
                .OrderByDescending(l => l.Timestamp)
                .Take(50)
                .ToList();

            return Ok(logs);
        }
    }
}