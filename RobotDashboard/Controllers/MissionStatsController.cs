using Microsoft.AspNetCore.Mvc;
using RobotDashboard.DTOs;
using RobotDashboard.Services;

namespace RobotDashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MissionStatsController : ControllerBase
    {
        private readonly IMissionStatsService _missionStatsService;

        // Dependency Injection of our new service
        public MissionStatsController(IMissionStatsService missionStatsService)
        {
            _missionStatsService = missionStatsService;
        }

        [HttpPost("calculate")]
        public ActionResult<MissionStatsResponseDto> Calculate([FromBody] MissionStatsRequestDto request)
        {
            // The [ApiController] attribute automatically handles 400 Bad Request 
            // if the DTO validation rules (from Step 1) fail.

            try
            {
                var result = _missionStatsService.Calculate(request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}