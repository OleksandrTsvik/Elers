using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Application.Common.Queries;
using Domain.Shared;

namespace Application.Courses.GetMyCourses;

public class GetMyCoursesQueryHandler : IQueryHandler<GetMyCoursesQuery, PagedList<GetMyCourseItemResponse>>
{
    private readonly ICourseQueries _courseQueries;
    private readonly IUserContext _userContext;

    public GetMyCoursesQueryHandler(ICourseQueries courseQueries, IUserContext userContext)
    {
        _courseQueries = courseQueries;
        _userContext = userContext;
    }

    public async Task<Result<PagedList<GetMyCourseItemResponse>>> Handle(
        GetMyCoursesQuery request,
        CancellationToken cancellationToken)
    {
        return await _courseQueries.GetMyCourses(
            _userContext.UserId, request.QueryParams, cancellationToken);
    }
}
