using Domain.Primitives;

namespace Domain.Entities;

public class Permission : Entity
{
    public string Name { get; set; } = string.Empty;

    public List<Role> Roles { get; set; } = [];
}
