using Domain.Enums;

namespace Application.Assignments.GradeAssignment;

public record GradeAssignmentRequest(
    SubmittedAssignmentStatus Status,
    double Grade,
    string? Comment);
