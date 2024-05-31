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

    public Task<GetListCourseRoleItemResponse[]> GetListCourseRoles(
        Guid courseId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.CourseRoles
            .Where(x => x.CourseId == courseId)
            .OrderBy(x => x.Name)
            .Select(x => new GetListCourseRoleItemResponse
            {
                Id = x.Id,
                Name = x.Name,
                CoursePermissions = x.CoursePermissions
                    .OrderBy(x => x.Name)
                    .Select(coursePermission => new GetListCourseRolePermissionItemResponse
                    {
                        Id = coursePermission.Id,
                        Description = coursePermission.Name.ToString()
                    })
                    .ToArray()
            })
            .ToArrayAsync(cancellationToken);
    }
}
