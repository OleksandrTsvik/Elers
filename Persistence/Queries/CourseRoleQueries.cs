using Application.Common.Queries;
using Application.CourseRoles.GetListCourseRoles;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Queries;

public class CourseRoleQueries : ICourseRoleQueries
{
    private readonly ApplicationDbContext _dbContext;

    public CourseRoleQueries(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<GetListCourseRolesResponse?> GetListCourseRoles(
        Guid courseId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Courses
            .Where(x => x.Id == courseId)
            .Select(x => new GetListCourseRolesResponse
            {
                CourseId = x.Id,
                CourseTitle = x.Title,
                CourseRoles = x.CourseRoles
                    .Select(courseRole => new GetListCourseRoleItemResponse
                    {
                        Id = courseRole.Id,
                        Name = courseRole.Name,
                        CoursePermissions = courseRole.CoursePermissions
                            .OrderBy(x => x.Name)
                            .Select(coursePermission => new GetListCourseRolePermissionItemResponse
                            {
                                Id = coursePermission.Id,
                                Description = coursePermission.Name.ToString()
                            })
                            .ToArray()
                    })
                    .OrderBy(courseRole => courseRole.Name)
                    .ToArray()
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
