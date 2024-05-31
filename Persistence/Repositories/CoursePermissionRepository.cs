using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal class CoursePermissionRepository
    : ApplicationDbRepository<CoursePermission>, ICoursePermissionRepository
{
    public CoursePermissionRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task<List<CoursePermission>> GetListAsync(
        IEnumerable<Guid> permissionIds,
        CancellationToken cancellationToken = default)
    {
        return DbContext.CoursePermissions
            .Where(x => permissionIds.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }
}
