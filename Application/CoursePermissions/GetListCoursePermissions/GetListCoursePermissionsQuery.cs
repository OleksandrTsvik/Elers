using Application.Common.Messaging;

namespace Application.CoursePermissions.GetListCoursePermissions;

public record GetListCoursePermissionsQuery() : IQuery<GetListCoursePermissionItemResponse[]>;
