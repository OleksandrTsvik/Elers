using Application.Common.Models;
using Application.CourseMembers.GetListCourseMembers;

namespace Application.Common.Queries;

public interface ICourseMemberQueries
{
    Task<PagedList<CourseMemberListItem>> GetListCourseMembers(
        Guid courseId,
        bool isGetWithRoles,
        GetListCourseMembersQueryParams queryParams,
        CancellationToken cancellationToken = default);
}
