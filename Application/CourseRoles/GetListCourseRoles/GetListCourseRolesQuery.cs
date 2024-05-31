using Application.Common.Messaging;

namespace Application.CourseRoles.GetListCourseRoles;

public record GetListCourseRolesQuery(Guid CourseId) : IQuery<GetListCourseRolesResponse>;
