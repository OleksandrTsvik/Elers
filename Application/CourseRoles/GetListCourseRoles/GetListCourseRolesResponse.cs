namespace Application.CourseRoles.GetListCourseRoles;

public class GetListCourseRolesResponse
{
    public required Guid CourseId { get; init; }
    public required string CourseTitle { get; init; }
    public required GetListCourseRoleItemResponse[] CourseRoles { get; init; }
}

public class GetListCourseRoleItemResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required GetListCourseRolePermissionItemResponse[] CoursePermissions { get; init; }
}

public class GetListCourseRolePermissionItemResponse
{
    public required Guid Id { get; init; }
    public required string Description { get; set; }
}
