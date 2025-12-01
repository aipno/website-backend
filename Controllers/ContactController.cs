using Microsoft.AspNetCore.Mvc;
using website_backend.Models;
using website_backend.Services;

namespace website_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    public async Task<IActionResult> GetContact()
    {
        // 直接返回测试数据，避免复杂查询和序列化问题
        var result = new {
            details = new List<object> {
                new { Type = "邮箱", Value = "contact@lingyi-sec.com" },
                new { Type = "QQ群", Value = "123456789" }
            },
            socialLinks = new List<object> {
                new { Name = "GitHub", Url = "https://github.com/lingyi-sec" },
                new { Name = "微博", Url = "https://weibo.com/lingyi-sec" }
            },
            joinUs = new {
                description = "欢迎对网络安全感兴趣的同学加入我们！",
                conditions = new List<string> { "对网络安全感兴趣", "遵守社团章程", "积极参与活动" },
                steps = new List<string> { "填写申请表", "参加面试", "通过培训" },
                applicationUrl = "https://example.com/join"
            }
        };
        
        return Ok(ApiResponse<object>.SuccessResponse(result));
    }

    [HttpGet("details")]
    public async Task<IActionResult> GetContactDetails()
    {
        var details = await _contactService.GetContactDetailsAsync();
        return Ok(ApiResponse<List<ContactDetail>>.SuccessResponse(details));
    }

    [HttpGet("social")]
    public async Task<IActionResult> GetSocialLinks()
    {
        var socialLinks = await _contactService.GetSocialLinksAsync();
        return Ok(ApiResponse<List<SocialLink>>.SuccessResponse(socialLinks));
    }

    [HttpGet("join")]
    public async Task<IActionResult> GetJoinUsInfo()
    {
        var joinUsInfo = await _contactService.GetJoinUsInfoAsync();
        if (joinUsInfo == null)
        {
            return Ok(ApiResponse<object>.SuccessResponse(new { description = string.Empty, conditions = new List<string>(), steps = new List<string>(), applicationUrl = string.Empty }));
        }
        return Ok(ApiResponse<JoinUsInfo>.SuccessResponse(joinUsInfo));
    }
}