using Microsoft.EntityFrameworkCore;
using website_backend.Data;
using website_backend.Models;

namespace website_backend.Services;

public interface IActivityService
{
    Task<List<Activity>> GetAllActivitiesAsync(string? type = null);
    Task<List<Activity>> GetLatestActivitiesAsync(int limit = 3);
    Task<Activity?> GetActivityByIdAsync(int id);
}

public class ActivityService : IActivityService
{
    private readonly ApplicationDbContext _context;

    public ActivityService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Activity>> GetAllActivitiesAsync(string? type = null)
    {
        var query = _context.Activities.AsQueryable();

        if (!string.IsNullOrEmpty(type))
        {
            if (type.Equals("upcoming", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(a => a.Status == ActivityStatus.Upcoming);
            }
            else if (type.Equals("past", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(a => a.Status == ActivityStatus.Past);
            }
        }

        return await query.ToListAsync();
    }

    public async Task<List<Activity>> GetLatestActivitiesAsync(int limit = 3)
    {
        return await _context.Activities
            .OrderByDescending(a => a.Date)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<Activity?> GetActivityByIdAsync(int id)
    {
        return await _context.Activities.FindAsync(id);
    }
}