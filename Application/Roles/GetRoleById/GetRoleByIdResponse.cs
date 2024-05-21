using Domain.Enums;

namespace Application.Roles.GetRoleById;

public class GetRoleByIdResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required GetRoleByIdPermissionResponse[] Permissions { get; init; }
}

public class GetRoleByIdPermissionResponse
{
    public required Guid Id { get; init; }
    public required PermissionType Name { get; init; }
    public required string Description { get; init; }
    public required bool IsSelected { get; init; }
}
