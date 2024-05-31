using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Errors;
using Domain.Shared;

namespace Application.CourseRoles.GetListCourseRoles;

public class GetListCourseRolesQueryHandler
    : IQueryHandler<GetListCourseRolesQuery, GetListCourseRolesResponse>
{
    private readonly ICourseRoleQueries _courseRoleQueries;
    private readonly ITranslator _translator;

    public GetListCourseRolesQueryHandler(ICourseRoleQueries courseRoleQueries, ITranslator translator)
    {
        _courseRoleQueries = courseRoleQueries;
        _translator = translator;
    }

    public async Task<Result<GetListCourseRolesResponse>> Handle(
        GetListCourseRolesQuery request,
        CancellationToken cancellationToken)
    {
        GetListCourseRolesResponse? courseRolesResponse = await _courseRoleQueries
            .GetListCourseRoles(request.CourseId, cancellationToken);

        if (courseRolesResponse is null)
        {
            return CourseErrors.NotFound(request.CourseId);
        }

        foreach (GetListCourseRoleItemResponse courseRole in courseRolesResponse.CourseRoles)
        {
            foreach (GetListCourseRolePermissionItemResponse permission in courseRole.CoursePermissions)
            {
                permission.Description = _translator.GetString(permission.Description);
            }
        }

        return courseRolesResponse;
    }
}
