using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories;

internal class CourseTabRepository : ApplicationDbRepository<CourseTab>, ICourseTabRepository
{
    public CourseTabRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
}
