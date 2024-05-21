using Domain.Primitives;

namespace Domain.Entities;

public class CourseTab : Entity
{
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
