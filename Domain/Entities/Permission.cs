using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities;

public class Permission : Entity
{
    public PermissionType Name { get; set; }

    public List<Role> Roles { get; set; } = [];
}
