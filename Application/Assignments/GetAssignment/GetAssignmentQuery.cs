using Application.Common.Messaging;

namespace Application.Assignments.GetAssignment;

public record GetAssignmentQuery(Guid AssignmentId) : IQuery<GetAssignmentResponse>;
