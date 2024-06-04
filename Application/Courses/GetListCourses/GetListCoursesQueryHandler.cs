using Application.Common.Messaging;
using Application.Common.Models;
using Application.Common.Queries;
using Domain.Shared;

namespace Application.Courses.GetListCourses;

public class GetListCoursesQueryHandler
    : IQueryHandler<GetListCoursesQuery, PagedList<GetListCourseItemResponse>>
{
    private readonly ICourseQueries _courseQueries;

    public GetListCoursesQueryHandler(ICourseQueries courseQueries)
    {
        _courseQueries = courseQueries;
    }

    public async Task<Result<PagedList<GetListCourseItemResponse>>> Handle(
        GetListCoursesQuery request,
        CancellationToken cancellationToken)
    {
        return await _courseQueries.GetListCourses(request.QueryParams, cancellationToken);
    }
}
