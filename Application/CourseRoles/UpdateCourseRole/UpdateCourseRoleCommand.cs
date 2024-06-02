using Application.Common.Messaging;

namespace Application.CourseRoles.UpdateCourseRole;

public record UpdateCourseRoleCommand(
    Guid RoleId,
    string Name,
    Guid[] PermissionIds) : ICommand;
