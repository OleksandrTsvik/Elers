using Domain.Primitives;

namespace Domain.Entities;

public class Role : Entity
{
    public string Name { get; set; } = string.Empty;

    public List<User> Users { get; set; } = [];
    public List<Permission> Permissions { get; set; } = [];
}
