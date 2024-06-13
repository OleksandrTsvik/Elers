using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal class CourseTabRepository : ApplicationDbRepository<CourseTab>, ICourseTabRepository
{
    public CourseTabRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task ReorderAsync(
        IEnumerable<ReorderItem> reorders,
        CancellationToken cancellationToken = default)
    {
        IEnumerable<Guid> tabIds = reorders.Select(x => x.Id);

        List<CourseTab> courseTabs = await DbContext.CourseTabs
            .Where(x => tabIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        if (courseTabs.Count == 0)
        {
            return;
        }

        foreach (CourseTab courseTab in courseTabs)
        {
            courseTab.Order = reorders
                .Where(x => x.Id == courseTab.Id)
                .Select(x => x.Order)
                .FirstOrDefault(courseTab.Order);
        }

        await DbContext.SaveChangesAsync(cancellationToken);
    }
}
