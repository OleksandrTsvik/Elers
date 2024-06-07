using Application.Common.Messaging;

namespace Application.Assignments.GetSubmittedAssignment;

public record GetSubmittedAssignmentQuery(Guid SubmittedAssignmentId)
    : IQuery<GetSubmittedAssignmentResponse>;
