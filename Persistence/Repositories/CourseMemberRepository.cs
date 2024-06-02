using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal class CourseMemberRepository : ApplicationDbRepository<CourseMember>, ICourseMemberRepository
{
    public CourseMemberRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task<CourseMember?> GetByCourseIdAndUserIdAsync(
        Guid courseId,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return DbContext.CourseMembers
            .FirstOrDefaultAsync(x => x.CourseId == courseId && x.UserId == userId, cancellationToken);
    }

    public Task<bool> IsEnrolledAsync(
        Guid courseId,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return DbContext.CourseMembers
            .AnyAsync(x => x.CourseId == courseId && x.UserId == userId, cancellationToken);
    }
}
