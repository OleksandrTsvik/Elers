using Application.Common.Messaging;

namespace Application.Roles.CreateRole;

public record CreateRoleCommand(string Name, Guid[] PermissionIds) : ICommand;
