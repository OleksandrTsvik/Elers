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

    public Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return DbContext.Courses.AnyAsync(x => x.Id == id, cancellationToken);
    }
}
