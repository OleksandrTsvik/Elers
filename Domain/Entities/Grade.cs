using System.Text.Json.Serialization;
using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities;

[JsonDerivedType(typeof(GradeAssignment))]
[JsonDerivedType(typeof(GradeTest))]
[JsonDerivedType(typeof(GradeManual))]
public abstract class Grade : Entity
{
    public GradeType Type { get; protected set; }
    public Guid CourseId { get; set; }
    public Guid StudentId { get; set; }
    public DateTime CreatedAt { get; set; }

    public Grade()
    {
        CreatedAt = DateTime.UtcNow;
    }
}

public class GradeAssignment : Grade
{
    public Guid TeacherId { get; set; }
    public Guid AssignmentId { get; set; }
    public double Value { get; set; }

    public GradeAssignment() : base()
    {
        Type = GradeType.Assignment;
    }
}

public class GradeTest : Grade
{
    public Guid TestId { get; set; }
    public GradingMethod GradingMethod { get; set; }
    public List<GradeTestItem> Values { get; set; } = [];

    public GradeTest() : base()
    {
        Type = GradeType.Test;
    }
}

public class GradeTestItem
{
    public Guid TestSessionId { get; set; }
    public double Value { get; set; }
}

public class GradeManual : Grade
{
    public Guid ManualGradesColumnId { get; set; }
    public Guid TeacherId { get; set; }
    public double Value { get; set; }

    public GradeManual() : base()
    {
        Type = GradeType.Manual;
    }
}
