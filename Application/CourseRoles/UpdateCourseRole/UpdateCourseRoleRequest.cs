namespace Application.CourseRoles.UpdateCourseRole;

public record UpdateCourseRoleRequest(string Name, Guid[] PermissionIds);
