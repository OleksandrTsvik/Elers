using Application.Common.Services;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services;

public class CourseMemberService : ICourseMemberService
{
    private readonly ApplicationDbContext _dbContext;

    public CourseMemberService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> IsCourseStudentAsync(
        Guid studentId,
        Guid courseId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Courses.AnyAsync(x =>
            x.Id == courseId &&
            x.CourseMembers.Any(courseMember =>
                courseMember.CourseId == courseId &&
                courseMember.UserId == studentId &&
                courseMember.User != null &&
                courseMember.User.Type == UserType.Student),
            cancellationToken);
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
