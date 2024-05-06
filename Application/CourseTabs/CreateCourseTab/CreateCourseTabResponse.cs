namespace Application.CourseTabs.CreateCourseTab;

public class CreateCourseTabResponse
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int Order { get; set; }
    public string? Color { get; set; }
    public bool ShowMaterialsCount { get; set; }
}
