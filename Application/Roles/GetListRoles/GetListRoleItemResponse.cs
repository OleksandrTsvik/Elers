namespace Application.Roles.GetListRoles;

public class GetListRoleItemResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public PermissionResponse[] Permissions { get; init; } = [];
}

public class PermissionResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
