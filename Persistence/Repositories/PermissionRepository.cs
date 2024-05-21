using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal class PermissionRepository : ApplicationDbRepository<Permission>, IPermissionRepository
{
    public PermissionRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task<List<Permission>> GetListAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        return DbContext.Permissions
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }
}
