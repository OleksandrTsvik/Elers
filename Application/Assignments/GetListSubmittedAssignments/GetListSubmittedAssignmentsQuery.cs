using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Assignments.GetListSubmittedAssignments;

public record GetListSubmittedAssignmentsQuery(
    Guid CourseId,
    GetListSubmittedAssignmentsQueryParams QueryParams) : IQuery<PagedList<SubmittedAssignmentListItem>>;
