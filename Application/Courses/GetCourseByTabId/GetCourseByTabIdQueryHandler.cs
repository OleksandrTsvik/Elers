using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Errors;
using Domain.Shared;

namespace Application.Courses.GetCourseByTabId;

public class GetCourseByTabIdQueryHandler : IQueryHandler<GetCourseByTabIdQuery, GetCourseByTabIdResponse>
{
    private readonly ICourseQueries _courseQueries;

    public GetCourseByTabIdQueryHandler(ICourseQueries courseQueries)
    {
        _courseQueries = courseQueries;
    }

    public async Task<Result<GetCourseByTabIdResponse>> Handle(
        GetCourseByTabIdQuery request,
        CancellationToken cancellationToken)
    {
        GetCourseByTabIdResponse? courseByTabId = await _courseQueries.GetCourseByTabId(
            request.TabId, cancellationToken);

        if (courseByTabId is null)
        {
            return CourseTabErrors.NotFound(request.TabId);
        }

        return courseByTabId;
    }
}
