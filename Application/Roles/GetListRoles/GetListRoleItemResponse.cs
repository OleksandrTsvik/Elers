namespace Application.Roles.GetListRoles;

public class GetListRoleItemResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string[] Permissions { get; init; } = [];
}
