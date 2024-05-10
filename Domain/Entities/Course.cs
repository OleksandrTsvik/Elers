namespace Domain.Entities;

public class Course
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? PhotoUrl { get; set; }
    public string? TabType { get; set; }

    public List<CourseTab> CourseTabs { get; set; } = [];
}
