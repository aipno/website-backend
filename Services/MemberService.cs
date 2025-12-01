using Microsoft.EntityFrameworkCore;
using website_backend.Data;
using website_backend.Models;

namespace website_backend.Services;

public interface IMemberService
{
    Task<List<Member>> GetAllMembersAsync(string? type = null);
    Task<Member?> GetMemberByIdAsync(int id);
}

public class MemberService : IMemberService
{
    private readonly ApplicationDbContext _context;

    public MemberService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Member>> GetAllMembersAsync(string? type = null)
    {
        var query = _context.Members.AsQueryable();

        if (!string.IsNullOrEmpty(type))
        {
            if (type.Equals("core", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(m => m.Type == MemberType.Core);
            }
            else if (type.Equals("general", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(m => m.Type == MemberType.General);
            }
        }

        return await query.ToListAsync();
    }

    public async Task<Member?> GetMemberByIdAsync(int id)
    {
        return await _context.Members.FindAsync(id);
    }
}