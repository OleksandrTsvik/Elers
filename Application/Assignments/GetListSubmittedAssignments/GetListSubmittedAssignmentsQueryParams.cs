using Application.Common.Models;
using Domain.Enums;

namespace Application.Assignments.GetListSubmittedAssignments;

public class GetListSubmittedAssignmentsQueryParams : PagingParams
{
    public SubmittedAssignmentStatus? Status { get; init; }
    public Guid? AssignmentId { get; init; }
    public Guid? StudentId { get; init; }
}
