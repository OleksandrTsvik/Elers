using Application.Common.Messaging;

namespace Application.CourseRoles.DeleteCourseRole;

public record DeleteCourseRoleCommand(Guid RoleId) : ICommand;
