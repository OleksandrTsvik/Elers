using Domain.Enums;

namespace Application.Grades.CreateGread;

public record CreateGreadRequest(
    Guid StudentId,
    Guid AssessmentId,
    GradeType GradeType,
    double Value);
