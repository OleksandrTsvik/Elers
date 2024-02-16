using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Authentication;

public class RolesService : IRolesService
{
    private readonly ApplicationDbContext _context;

    public RolesService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HashSet<string>> GetRolesAsync(Guid userId)
    {
        List<Role>[] roles = await _context.Users
            .Include(x => x.Roles)
            .Where(x => x.Id == userId)
            .Select(x => x.Roles)
            .ToArrayAsync();

        return roles
            .SelectMany(x => x)
            .Select(x => x.Name)
            .ToHashSet();
    }
}
