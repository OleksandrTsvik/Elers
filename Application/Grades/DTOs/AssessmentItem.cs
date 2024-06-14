using System.Text.Json.Serialization;
using Domain.Enums;

namespace Application.Grades.DTOs;

[JsonDerivedType(typeof(AssessmentManualItem))]
[JsonDerivedType(typeof(AssessmentAssignmentItem))]
public class AssessmentItem
{
    public required Guid Id { get; init; }
    public required GradeType Type { get; init; }
    public required string Title { get; init; }
}

public class AssessmentAssignmentItem : AssessmentItem
{
    public required int MaxGrade { get; init; }
}

public class AssessmentManualItem : AssessmentItem
{
    public required DateTime Date { get; init; }
    public required int MaxGrade { get; init; }
}
