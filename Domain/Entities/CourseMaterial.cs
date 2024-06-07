using System.Text.Json.Serialization;
using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities;

[JsonDerivedType(typeof(CourseMaterialContent))]
[JsonDerivedType(typeof(CourseMaterialLink))]
[JsonDerivedType(typeof(CourseMaterialFile))]
[JsonDerivedType(typeof(CourseMaterialAssignment))]
public abstract class CourseMaterial : Entity
{
    public Guid CourseTabId { get; set; }
    public CourseMaterialType Type { get; protected set; }
    public bool IsActive { get; set; }
    public int Order { get; set; }
    public DateTime CreatedAt { get; set; }

    public CourseMaterial()
    {
        IsActive = true;
        Order = -1;
        CreatedAt = DateTime.UtcNow;
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
    public string FileName { get; set; } = string.Empty;
    public string UniqueFileName { get; set; } = string.Empty;

    public CourseMaterialFile() : base()
    {
        Type = CourseMaterialType.File;
    }
}

public class CourseMaterialAssignment : CourseMaterial
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? Deadline { get; set; }
    public int MaxFiles { get; set; }
    public int MaxGrade { get; set; }

    public CourseMaterialAssignment() : base()
    {
        Type = CourseMaterialType.Assignment;
    }
}
