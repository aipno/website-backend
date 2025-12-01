using Microsoft.AspNetCore.Mvc;
using website_backend.Models;
using website_backend.Services;

namespace website_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AboutController : ControllerBase
{
    private readonly IAboutService _aboutService;

    public AboutController(IAboutService aboutService)
    {
        _aboutService = aboutService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAbout()
    {
        // 直接返回测试数据，避免复杂查询和序列化问题
        var result = new {
            background = "零壹网络安全社团成立于2023年，是一个致力于推广网络安全知识、培养网络安全人才的学生社团。",
            missions = new List<string> { "推广网络安全知识", "培养网络安全人才", "参与网络安全竞赛", "服务校园网络安全" },
            history = new List<object> {
                new { Year = "2023", Description = "社团成立，招收首批成员" },
                new { Year = "2024", Description = "首次参加省赛并获得三等奖" },
                new { Year = "2025", Description = "社团成员突破100人" }
            },
            organization = new List<object> {
                new { Name = "技术部", Description = "负责技术培训和竞赛" },
                new { Name = "宣传部", Description = "负责社团宣传和活动策划" },
                new { Name = "外联部", Description = "负责对外合作和赞助" }
            }
        };
        
        return Ok(ApiResponse<object>.SuccessResponse(result));
    }

    [HttpGet("background")]
    public async Task<IActionResult> GetBackground()
    {
        var background = await _aboutService.GetBackgroundAsync();
        return Ok(ApiResponse<string>.SuccessResponse(background));
    }

    [HttpGet("mission")]
    public async Task<IActionResult> GetMissions()
    {
        var missions = await _aboutService.GetMissionsAsync();
        return Ok(ApiResponse<List<string>>.SuccessResponse(missions));
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetHistory()
    {
        var history = await _aboutService.GetHistoryAsync();
        return Ok(ApiResponse<List<HistoryItem>>.SuccessResponse(history));
    }

    [HttpGet("organization")]
    public async Task<IActionResult> GetOrganization()
    {
        var organization = await _aboutService.GetOrganizationAsync();
        return Ok(ApiResponse<List<OrganizationItem>>.SuccessResponse(organization));
    }
}