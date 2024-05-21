namespace Application.Roles.GetRoleById;

public class GetRoleByIdResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required PermissionResponse[] Permissions { get; init; }
}

public class PermissionResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required bool IsSelected { get; init; }
}
