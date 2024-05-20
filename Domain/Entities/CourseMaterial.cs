using System.Text.Json.Serialization;
using Domain.Enums;

namespace Domain.Entities;

[JsonDerivedType(typeof(CourseMaterialContent))]
[JsonDerivedType(typeof(CourseMaterialLink))]
[JsonDerivedType(typeof(CourseMaterialFile))]
public abstract class CourseMaterial
{
    public Guid Id { get; set; }
    public Guid CourseTabId { get; set; }
    public CourseMaterialType Type { get; set; }
    public bool IsActive { get; set; }
    public int Order { get; set; }

    public CourseMaterial()
    {
        IsActive = true;
        Order = -1;
    }
}

public class CourseMaterialContent : CourseMaterial
{
    public string Content { get; set; } = string.Empty;

    public CourseMaterialContent() : base()
    {
        Type = CourseMaterialType.Content;
    }
}

public class CourseMaterialLink : CourseMaterial
{
    public string Title { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;

    public CourseMaterialLink() : base()
    {
        Type = CourseMaterialType.Link;
    }
}

public class CourseMaterialFile : CourseMaterial
{
    public string Title { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;

    public CourseMaterialFile() : base()
    {
        Type = CourseMaterialType.File;
    }
}
