namespace Application.Permissions.GetListPermissions;

public class GetListPermissionItemResponse : GetListPermissionItemResponseDto
{
    public required string Description { get; init; }
}

public class GetListPermissionItemResponseDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}
