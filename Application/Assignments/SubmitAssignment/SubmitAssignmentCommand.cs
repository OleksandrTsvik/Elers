using Application.Common.Interfaces;
using Application.Common.Messaging;

namespace Application.Assignments.SubmitAssignment;

public record SubmitAssignmentCommand(
    Guid AssignmentId,
    string? Text,
    IFile[]? Files) : ICommand;
