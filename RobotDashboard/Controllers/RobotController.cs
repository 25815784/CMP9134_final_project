using Microsoft.AspNetCore.Mvc;
using RobotDashboard.Services;

namespace RobotDashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RobotController : ControllerBase
    {
        private readonly IRobotClient _robotClient;

        public RobotController(IRobotClient robotClient)
        {
            _robotClient = robotClient;
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var status = await _robotClient.GetStatusAsync();
            return Ok(status);
        }

        [HttpPost("move")]
        public async Task<IActionResult> Move(int x, int y)
        {
            var result = await _robotClient.MoveAsync(x, y);

            if (!result)
                return BadRequest("Move failed");

            return Ok("Move successful");
        }

        [HttpPost("reset")]
        public async Task<IActionResult> Reset()
        {
            var result = await _robotClient.ResetAsync();

            if (!result)
                return BadRequest("Reset failed");

            return Ok("Robot reset successful");
        }
    }
}