using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authentication;

public class PermissionService : IPermissionService
{
    private readonly IApplicationDbContext _context;

    public PermissionService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> GetPermissionsAsync(Guid userId)
    {
        List<Role>[] roles = await _context.Users
            .Include(x => x.Roles)
                .ThenInclude(x => x.Permissions)
            .Where(x => x.Id == userId)
            .Select(x => x.Roles)
            .ToArrayAsync();

        return roles
            .SelectMany(x => x)
            .SelectMany(x => x.Permissions)
            .Select(x => x.Name)
            .Distinct()
            .ToList();
    }
}
