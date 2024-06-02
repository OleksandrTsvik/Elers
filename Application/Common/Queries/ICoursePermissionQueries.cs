using Application.CoursePermissions.GetCourseMemberPermissions;
using Application.CoursePermissions.GetListCoursePermissions;

namespace Application.Common.Queries;

public interface ICoursePermissionQueries
{
    Task<GetCourseMemberPermissionsResponse?> GetCourseMemberPermissions(
        Guid courseId,
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<GetListCoursePermissionItemResponse[]> GetListCoursePermissions(
        CancellationToken cancellationToken = default);
}
