namespace Application.Courses.GetCourseById;

public class GetCourseByIdResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? PhotoUrl { get; init; }
    public string? TabType { get; init; }
    public CourseTabResponse[] CourseTabs { get; set; } = [];
}

public class CourseTabResponse
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int Order { get; set; }
    public string? Color { get; set; }
    public bool ShowMaterialsCount { get; set; }
}
