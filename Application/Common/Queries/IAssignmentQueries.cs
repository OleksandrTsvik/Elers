using Application.Assignments.GetListAssignmentTitles;
using Application.Assignments.GetListSubmittedAssignments;
using Application.Common.Models;

namespace Application.Common.Queries;

public interface IAssignmentQueries
{
    Task<PagedList<SubmittedAssignmentListItemDto>> GetListSubmittedAssignments(
        IEnumerable<Guid> tabIds,
        GetListSubmittedAssignmentsQueryParams queryParams,
        CancellationToken cancellationToken = default);

    Task<List<GetListAssignmentTitleItemResponse>> GetListAssignmentTitles(
        IEnumerable<Guid> tabIds,
        CancellationToken cancellationToken = default);
}
