using Application.Common.Messaging;

namespace Application.Roles.UpdateRolePermissions;

public record UpdateRolePermissionsCommand(Guid RoleId, Guid[] PermissionIds) : ICommand;
