using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities;

public class Course : Entity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? ImageName { get; set; }
    public CourseTabType TabType { get; set; }

    public List<CourseTab> CourseTabs { get; set; } = [];

    public Course()
    {
        TabType = CourseTabType.Tabs;
    }
}
