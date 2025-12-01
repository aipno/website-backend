using Microsoft.EntityFrameworkCore;
using website_backend.Data;
using website_backend.Models;

namespace website_backend.Services;

public interface IAboutService
{
    Task<About?> GetAboutAsync();
    Task<string> GetBackgroundAsync();
    Task<List<string>> GetMissionsAsync();
    Task<List<HistoryItem>> GetHistoryAsync();
    Task<List<OrganizationItem>> GetOrganizationAsync();
}

public class AboutService : IAboutService
{
    private readonly ApplicationDbContext _context;

    public AboutService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<About?> GetAboutAsync()
    {
        return await _context.About
            .Include(a => a.History)
            .Include(a => a.Organization)
            .FirstOrDefaultAsync();
    }

    public async Task<string> GetBackgroundAsync()
    {
        var about = await _context.About.FirstOrDefaultAsync();
        return about?.Background ?? string.Empty;
    }

    public async Task<List<string>> GetMissionsAsync()
    {
        var about = await _context.About.FirstOrDefaultAsync();
        return about?.Missions ?? new List<string>();
    }

    public async Task<List<HistoryItem>> GetHistoryAsync()
    {
        var about = await _context.About
            .Include(a => a.History)
            .FirstOrDefaultAsync();
        return about?.History ?? new List<HistoryItem>();
    }

    public async Task<List<OrganizationItem>> GetOrganizationAsync()
    {
        var about = await _context.About
            .Include(a => a.Organization)
            .FirstOrDefaultAsync();
        return about?.Organization ?? new List<OrganizationItem>();
    }
}