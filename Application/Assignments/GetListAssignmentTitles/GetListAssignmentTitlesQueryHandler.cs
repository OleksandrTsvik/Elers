using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Shared;

namespace Application.Assignments.GetListAssignmentTitles;

public class GetListAssignmentTitlesQueryHandler
    : IQueryHandler<GetListAssignmentTitlesQuery, List<GetListAssignmentTitleItemResponse>>
{
    private readonly ICourseQueries _courseQueries;
    private readonly IAssignmentQueries _assignmentQueries;

    public GetListAssignmentTitlesQueryHandler(
        ICourseQueries courseQueries,
        IAssignmentQueries assignmentQueries)
    {
        _courseQueries = courseQueries;
        _assignmentQueries = assignmentQueries;
    }

    public async Task<Result<List<GetListAssignmentTitleItemResponse>>> Handle(
        GetListAssignmentTitlesQuery request,
        CancellationToken cancellationToken)
    {
        Guid[] tabIds = await _courseQueries.GetCourseTabIds(request.CourseId, cancellationToken);

        return await _assignmentQueries.GetListAssignmentTitles(tabIds, cancellationToken);
    }
}
