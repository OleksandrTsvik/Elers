using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal class CourseRoleRepository : ApplicationDbRepository<CourseRole>, ICourseRoleRepository
{
    public CourseRoleRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task<CourseRole?> GetByIdWithCoursePermissionsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return DbContext.CourseRoles
            .Include(x => x.CoursePermissions)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<bool> ExistsByNameAsync(
        Guid courseId,
        string name,
        CancellationToken cancellationToken = default)
    {
        return DbContext.CourseRoles
            .AnyAsync(x => x.CourseId == courseId && x.Name == name, cancellationToken);
    }
}
