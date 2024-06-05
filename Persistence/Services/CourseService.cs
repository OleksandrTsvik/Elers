using Application.Common.Services;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Persistence.Services;

public class CourseService : ICourseService
{
    private readonly ApplicationDbContext _dbContext;

    public CourseService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> IsCourseMemberByCourseTabIdAsync(
        Guid userId,
        Guid courseTabId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Courses.AnyAsync(x =>
            x.CourseMembers.Any(courseMember => courseMember.UserId == userId) &&
            x.CourseTabs.Any(courseTab => courseTab.Id == courseTabId), cancellationToken);
    }
}
