using Application.Common.Queries;
using Application.CoursePermissions.GetCourseMemberPermissions;
using Application.CoursePermissions.GetListCoursePermissions;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Queries;

public class CoursePermissionQueries : ICoursePermissionQueries
{
    private readonly ApplicationDbContext _dbContext;

    public CoursePermissionQueries(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<GetCourseMemberPermissionsResponse?> GetCourseMemberPermissions(
        Guid courseId,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Courses
            .AsSplitQuery()
            .Include(x => x.CourseTabs)
            .Where(x => x.Id == courseId)
            .Select(x => new GetCourseMemberPermissionsResponse
            {
                IsCreator = x.CreatorId == userId,
                IsMember = x.CourseMembers.Any(courseMember =>
                    courseMember.CourseId == courseId && courseMember.UserId == userId),
                MemberPermissions = x.CreatorId == userId
                    ? Array.Empty<string>()
                    : x.CourseMembers
                        .Where(courseMember =>
                            courseMember.CourseId == courseId &&
                            courseMember.UserId == userId &&
                            courseMember.CourseRole != null)
                        .Select(courseMember => courseMember.CourseRole!.CoursePermissions)
                        .SelectMany(coursePermissions => coursePermissions)
                        .Select(coursePermission => coursePermission.Name.ToString())
                        .ToArray()
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<GetListCoursePermissionItemResponse[]> GetListCoursePermissions(
        CancellationToken cancellationToken = default)
    {
        return _dbContext.CoursePermissions
            .OrderBy(x => x.Name)
            .Select(x => new GetListCoursePermissionItemResponse
            {
                Id = x.Id,
                Description = x.Name.ToString()
            })
            .ToArrayAsync(cancellationToken);
    }
}
