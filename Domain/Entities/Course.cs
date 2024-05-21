using Domain.Primitives;

namespace Domain.Entities;

public class Course : Entity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? PhotoUrl { get; set; }
    public string? TabType { get; set; }

    public List<CourseTab> CourseTabs { get; set; } = [];
}
