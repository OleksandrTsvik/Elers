using Application.CourseRoles.GetListCourseRoles;

namespace Application.Common.Queries;

public interface ICourseRoleQueries
{
    Task<GetListCourseRoleItemResponse[]> GetListCourseRoles(
        Guid courseId,
        CancellationToken cancellationToken = default);
}
