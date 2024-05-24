using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories;

internal class CourseRepository : ApplicationDbRepository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
}
