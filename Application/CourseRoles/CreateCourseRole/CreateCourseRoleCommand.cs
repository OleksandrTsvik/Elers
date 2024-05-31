using Application.Common.Messaging;

namespace Application.CourseRoles.CreateCourseRole;

public record CreateCourseRoleCommand(
    Guid CourseId,
    string Name,
    Guid[] PermissionIds) : ICommand;
