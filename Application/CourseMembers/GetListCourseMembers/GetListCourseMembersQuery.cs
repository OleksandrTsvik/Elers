using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.CourseMembers.GetListCourseMembers;

public record GetListCourseMembersQuery(
    Guid CourseId,
    GetListCourseMembersQueryParams QueryParams) : IQuery<PagedList<CourseMemberListItem>>;
