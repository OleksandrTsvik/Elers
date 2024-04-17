using Application.Common.Messaging;

namespace Application.Roles.DeleteRole;

public record DeleteRoleCommand(Guid RoleId) : ICommand;
