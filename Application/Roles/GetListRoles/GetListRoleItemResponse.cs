using Domain.Enums;

namespace Application.Roles.GetListRoles;

public class GetListRoleItemResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string[] Permissions { get; init; }
}

public class GetListRoleItemResponseDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required PermissionType[] Permissions { get; init; }
}
