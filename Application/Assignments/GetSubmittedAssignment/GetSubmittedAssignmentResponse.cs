using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Assignments.GetSubmittedAssignment;

public class GetSubmittedAssignmentResponse
{
    public required Guid SubmittedAssignmentId { get; init; }
    public required Guid AssignmentId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required DateTime? Deadline { get; init; }
    public required int MaxFiles { get; init; }
    public required int MaxGrade { get; init; }

    public required UserDto Student { get; init; }
    public required UserDto? Teacher { get; init; }
    public required double? Grade { get; init; }

    public required SubmittedAssignmentStatus Status { get; init; }
    public required int AttemptNumber { get; init; }
    public required string? TeacherComment { get; init; }
    public required string? Text { get; init; }
    public required List<SubmitAssignmentFile> Files { get; init; }
    public required DateTime SubmittedAt { get; init; }
}
