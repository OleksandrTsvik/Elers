using Application.Common.Messaging;

namespace Application.Roles.UpdateRole;

public record UpdateRoleCommand(Guid RoleId, string Name) : ICommand;
