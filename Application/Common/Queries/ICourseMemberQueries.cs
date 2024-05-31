using Application.CourseMembers.GetListCourseMembers;

namespace Application.Common.Queries;

public interface ICourseMemberQueries
{
    Task<GetListCourseMemberItemResponse[]> GetListCourseMembers(
        Guid courseId,
        CancellationToken cancellationToken = default);

    Task<GetListCourseMemberItemResponse[]> GetListCourseMembersWithRoles(
        Guid courseId,
        CancellationToken cancellationToken = default);
}
