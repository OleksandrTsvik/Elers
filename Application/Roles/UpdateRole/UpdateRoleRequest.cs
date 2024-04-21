namespace Application.Roles.UpdateRole;

public record UpdateRoleRequest(string Name, Guid[] PermissionIds);
