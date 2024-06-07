using Domain.Enums;

namespace Application.Grades.DTOs;

public class AssessmentItem
{
    public required Guid Id { get; init; }
    public required GradeType Type { get; init; }
    public required string Title { get; init; }
}
