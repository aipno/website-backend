using Microsoft.AspNetCore.Mvc;
using website_backend.Models;
using website_backend.Services;

namespace website_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivitiesController : ControllerBase
{
    private readonly IActivityService _activityService;

    public ActivitiesController(IActivityService activityService)
    {
        _activityService = activityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllActivities([FromQuery] string? type)
    {
        var activities = await _activityService.GetAllActivitiesAsync(type);
        return Ok(ApiResponse<List<Activity>>.SuccessResponse(activities));
    }

    [HttpGet("latest")]
    public async Task<IActionResult> GetLatestActivities([FromQuery] int limit = 3)
    {
        var activities = await _activityService.GetLatestActivitiesAsync(limit);
        return Ok(ApiResponse<List<Activity>>.SuccessResponse(activities));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActivityById(int id)
    {
        var activity = await _activityService.GetActivityByIdAsync(id);
        if (activity == null)
        {
            return NotFound(ApiResponse<Activity>.ErrorResponse("活动不存在"));
        }
        return Ok(ApiResponse<Activity>.SuccessResponse(activity));
    }
}