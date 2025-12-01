using Microsoft.AspNetCore.Mvc;
using website_backend.Models;
using website_backend.Services;

namespace website_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMembers([FromQuery] string? type)
    {
        var members = await _memberService.GetAllMembersAsync(type);
        return Ok(ApiResponse<List<Member>>.SuccessResponse(members));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMemberById(int id)
    {
        var member = await _memberService.GetMemberByIdAsync(id);
        if (member == null)
        {
            return NotFound(ApiResponse<Member>.ErrorResponse("成员不存在"));
        }
        return Ok(ApiResponse<Member>.SuccessResponse(member));
    }
}