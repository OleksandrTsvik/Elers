namespace Domain.Entities;

public class CourseTab
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int Order { get; set; }
    public string? Color { get; set; }
    public bool ShowMaterialsCount { get; set; }

    public Course? Course { get; set; }

    public CourseTab()
    {
        IsActive = true;
        Order = -1;
        ShowMaterialsCount = true;
    }
}
