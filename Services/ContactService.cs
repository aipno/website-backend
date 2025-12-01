using Microsoft.EntityFrameworkCore;
using website_backend.Data;
using website_backend.Models;

namespace website_backend.Services;

public interface IContactService
{
    Task<Contact?> GetContactAsync();
    Task<List<ContactDetail>> GetContactDetailsAsync();
    Task<List<SocialLink>> GetSocialLinksAsync();
    Task<JoinUsInfo?> GetJoinUsInfoAsync();
}

public class ContactService : IContactService
{
    private readonly ApplicationDbContext _context;

    public ContactService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Contact?> GetContactAsync()
    {
        return await _context.Contact
            .Include(c => c.Details)
            .Include(c => c.SocialLinks)
            .Include(c => c.JoinUs)
            .FirstOrDefaultAsync();
    }

    public async Task<List<ContactDetail>> GetContactDetailsAsync()
    {
        var contact = await _context.Contact
            .Include(c => c.Details)
            .FirstOrDefaultAsync();
        return contact?.Details ?? new List<ContactDetail>();
    }

    public async Task<List<SocialLink>> GetSocialLinksAsync()
    {
        var contact = await _context.Contact
            .Include(c => c.SocialLinks)
            .FirstOrDefaultAsync();
        return contact?.SocialLinks ?? new List<SocialLink>();
    }

    public async Task<JoinUsInfo?> GetJoinUsInfoAsync()
    {
        var contact = await _context.Contact
            .Include(c => c.JoinUs)
            .FirstOrDefaultAsync();
        return contact?.JoinUs;
    }
}