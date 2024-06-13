using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal class CourseRepository : ApplicationDbRepository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task<Course?> GetByIdWithCourseTabsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return DbContext.Courses
            .Include(x => x.CourseTabs)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> CourseTabsInSameCourseAsync(
        IEnumerable<Guid> tabIds,
        CancellationToken cancellationToken = default)
    {
        return 1 == await DbContext.CourseTabs
            .Where(x => tabIds.Contains(x.Id))
            .GroupBy(cm => cm.CourseId)
            .CountAsync(cancellationToken);
    }
}
