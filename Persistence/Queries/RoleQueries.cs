using Application.Common.Models;
using Application.Common.Queries;
using Application.Roles.GetListRoles;
using Application.Roles.GetListUserRoles;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

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

    public Task<PagedList<GetListRoleItemResponseDto>> GetListRoles(
        GetListRolesQueryParams queryParams,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Roles
            .Include(x => x.Permissions)
            .OrderBy(x => x.Name)
            .Select(x => new GetListRoleItemResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Permissions = x.Permissions
                    .Select(permission => permission.Name)
                    .ToArray()
            })
            .ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize, cancellationToken);
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
