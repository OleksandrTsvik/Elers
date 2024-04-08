namespace Domain.Entities;

public class Permission
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<Role> Roles { get; set; } = [];
}