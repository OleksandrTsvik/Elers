using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities;

public class Course : Entity
{
    public Guid? CreatorId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? ImageName { get; set; }
    public CourseTabType TabType { get; set; }

    public User? Creator { get; set; }
    public List<CourseTab> CourseTabs { get; set; } = [];
    public List<CourseRole> CourseRoles { get; set; } = [];

    public Course()
    {
        TabType = CourseTabType.Tabs;
    }
}
