using Application.Common.Messaging;
using Domain.Enums;

namespace Application.Grades.CreateGread;

public record CreateGreadCommand(
    Guid StudentId,
    Guid AssessmentId,
    GradeType GradeType,
    double Value) : ICommand;
