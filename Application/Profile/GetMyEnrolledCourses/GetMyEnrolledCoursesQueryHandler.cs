using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Shared;

namespace Application.Profile.GetMyEnrolledCourses;

public class GetMyEnrolledCoursesQueryHandler
    : IQueryHandler<GetMyEnrolledCoursesQuery, GetMyEnrolledCoursesResponse[]>
{
    private readonly IProfileQueries _profileQueries;
    private readonly IUserContext _userContext;

    public GetMyEnrolledCoursesQueryHandler(IProfileQueries profileQueries, IUserContext userContext)
    {
        _profileQueries = profileQueries;
        _userContext = userContext;
    }

    public async Task<Result<GetMyEnrolledCoursesResponse[]>> Handle(
        GetMyEnrolledCoursesQuery request,
        CancellationToken cancellationToken)
    {
        return await _profileQueries.GetMyEnrolledCourses(_userContext.UserId, cancellationToken);
    }
}
