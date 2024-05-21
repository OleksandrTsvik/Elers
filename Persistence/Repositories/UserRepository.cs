using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal class UserRepository : ApplicationDbRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task<User?> GetByIdWithRolesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return DbContext.Users
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<User?> GetByEmailWithRolesAndPermissionsAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return DbContext.Users
            .Include(x => x.Roles)
                .ThenInclude(x => x.Permissions)
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public Task<User?> GetByIdWithRolesAndPermissionsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return DbContext.Users
            .Include(x => x.Roles)
                .ThenInclude(x => x.Permissions)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<List<PermissionType>> GetPermissionsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return DbContext.Users
            .Include(user => user.Roles)
                .ThenInclude(user => user.Permissions)
            .Where(user => user.Id == id)
            .Select(user => user.Roles)
            .SelectMany(roles => roles)
            .Select(role => role.Permissions)
            .SelectMany(permissions => permissions)
            .Select(permission => permission.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default)
    {
        return !await DbContext.Users.AnyAsync(x => x.Email == email, cancellationToken);
    }
}
