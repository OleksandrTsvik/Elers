namespace Application.CourseRoles.CreateCourseRole;

public record CreateCourseRoleRequest(string Name, Guid[] PermissionIds);
