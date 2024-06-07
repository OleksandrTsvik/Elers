using System.Text.Json.Serialization;
using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities;

[JsonDerivedType(typeof(GradeAssignment))]
[JsonDerivedType(typeof(GradeTest))]
public class Grade : Entity
{
    public GradeType Type { get; protected set; }
    public Guid CourseId { get; set; }
    public Guid StudentId { get; set; }
    public double Value { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class GradeAssignment : Grade
{
    public Guid TeacherId { get; set; }
    public Guid AssignmentId { get; set; }

    public GradeAssignment() : base()
    {
        Type = GradeType.Assignment;
    }
}

public class GradeTest : Grade
{
    public Guid TestId { get; set; }

    public GradeTest() : base()
    {
        Type = GradeType.Test;
    }
}
