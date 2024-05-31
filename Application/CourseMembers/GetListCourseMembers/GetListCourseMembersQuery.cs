using Application.Common.Messaging;

namespace Application.CourseMembers.GetListCourseMembers;

public record GetListCourseMembersQuery(Guid CourseId) : IQuery<GetListCourseMemberItemResponse[]>;
