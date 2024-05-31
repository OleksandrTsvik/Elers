using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Errors;
using Domain.Shared;

namespace Application.CoursePermissions.GetCourseMemberPermissions;

public class GetCourseMemberPermissionsQueryHandler
    : IQueryHandler<GetCourseMemberPermissionsQuery, GetCourseMemberPermissionsResponse>
{
    private readonly ICoursePermissionQueries _coursePermissionQueries;
    private readonly IUserContext _userContext;

    public GetCourseMemberPermissionsQueryHandler(
        ICoursePermissionQueries coursePermissionQueries,
        IUserContext userContext)
    {
        _coursePermissionQueries = coursePermissionQueries;
        _userContext = userContext;
    }

    public async Task<Result<GetCourseMemberPermissionsResponse>> Handle(
        GetCourseMemberPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        if (!_userContext.IsAuthenticated)
        {
            return new GetCourseMemberPermissionsResponse
            {
                IsCreator = false,
                IsMember = false,
                MemberPermissions = []
            };
        }

        GetCourseMemberPermissionsResponse? courseMemberPermissions = await _coursePermissionQueries
            .GetCourseMemberPermissions(request.CourseId, _userContext.UserId, cancellationToken);

        if (courseMemberPermissions is null)
        {
            return CourseErrors.NotFound(request.CourseId);
        }

        return courseMemberPermissions;
    }
}
