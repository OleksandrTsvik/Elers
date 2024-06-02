using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities;

public class CoursePermission : Entity
{
    public CoursePermissionType Name { get; set; }

    public List<CourseRole> CourseRoles { get; set; } = [];
}
