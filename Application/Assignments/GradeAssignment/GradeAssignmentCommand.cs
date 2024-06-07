using Application.Common.Messaging;
using Domain.Enums;

namespace Application.Assignments.GradeAssignment;

public record GradeAssignmentCommand(
    Guid SubmittedAssignmentId,
    SubmittedAssignmentStatus Status,
    double Grade,
    string? Comment) : ICommand;
