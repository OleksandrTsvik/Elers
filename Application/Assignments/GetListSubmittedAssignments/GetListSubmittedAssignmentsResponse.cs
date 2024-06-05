namespace Application.Assignments.GetListSubmittedAssignments;

public class SubmittedAssignmentListItem : SubmittedAssignmentListItemDto
{
    public required string? StudentFirstName { get; init; }
    public required string? StudentLastName { get; init; }
    public required string? StudentPatronymic { get; init; }
}

public class SubmittedAssignmentListItemDto
{
    public required Guid SubmittedAssignmentId { get; init; }
    public required Guid AssignmentId { get; init; }
    public required Guid StudentId { get; init; }
    public required string AssignmentTitle { get; init; }
    public required DateTime SubmittedDate { get; init; }
}
