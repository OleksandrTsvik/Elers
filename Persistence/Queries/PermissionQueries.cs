using Application.Common.Queries;
using Application.Permissions.GetListPermissions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Queries;

internal class PermissionQueries : IPermissionQueries
{
    private readonly ApplicationDbContext _dbContext;

    public PermissionQueries(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Permission>> GetList(CancellationToken cancellationToken = default)
    {
        return _dbContext.Permissions
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<GetListPermissionItemResponseDto[]> GetListPermissions(
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Permissions
            .Select(x => new GetListPermissionItemResponseDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .OrderBy(x => x.Name)
            .ToArrayAsync(cancellationToken);
    }
}
