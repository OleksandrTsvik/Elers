namespace Application.Roles.GetRoleById;

public class GetRoleByIdResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public PermissionResponse[] Permissions { get; init; } = [];
}

public class PermissionResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public bool IsSelected { get; init; }
}
