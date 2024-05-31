using Application.CourseRoles.GetListCourseRoles;

namespace Application.Common.Queries;

public interface ICourseRoleQueries
{
    Task<GetListCourseRolesResponse?> GetListCourseRoles(
        Guid courseId,
        CancellationToken cancellationToken = default);
}
