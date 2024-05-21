using Application.Common.Queries;
using Application.Roles.GetListRoles;
using Application.Roles.GetListUserRoles;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Queries;

internal class RoleQueries : IRoleQueries
{
    private readonly ApplicationDbContext _dbContext;

    public RoleQueries(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Role?> GetByIdWithPermissions(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Roles
            .AsNoTracking()
            .Include(x => x.Permissions)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<GetListRoleItemResponse[]> GetListRoles(CancellationToken cancellationToken = default)
    {
        return _dbContext.Roles
            .Include(x => x.Permissions)
            .Select(x => new GetListRoleItemResponse
            {
                Id = x.Id,
                Name = x.Name,
                Permissions = x.Permissions
                    .Select(permission => permission.Name)
                    .ToArray()
            })
            .OrderBy(x => x.Name)
            .ToArrayAsync(cancellationToken);
    }

    public Task<GetListUserRoleItemResponse[]> GetListUserRoles(CancellationToken cancellationToken = default)
    {
        return _dbContext.Roles
            .Select(x => new GetListUserRoleItemResponse
            {
                Id = x.Id,
                Name = x.Name
            })
            .OrderBy(x => x.Name)
            .ToArrayAsync(cancellationToken);
    }
}