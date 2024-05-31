using Domain.Primitives;

namespace Domain.Entities;

public class CourseRole : Entity
{
    public Guid CourseId { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<CourseMember> CourseMembers { get; set; } = [];
    public List<CoursePermission> CoursePermissions { get; set; } = [];
}
