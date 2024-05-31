using Application.Common.Messaging;

namespace Application.CoursePermissions.GetCourseMemberPermissions;

public record GetCourseMemberPermissionsQuery(Guid CourseId) : IQuery<GetCourseMemberPermissionsResponse>;
