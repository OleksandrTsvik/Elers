namespace Application.Permissions.GetListPermissions;

public class GetListPermissionItemResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
