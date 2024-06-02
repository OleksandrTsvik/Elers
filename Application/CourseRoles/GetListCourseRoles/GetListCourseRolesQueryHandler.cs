using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Shared;

namespace Application.CourseRoles.GetListCourseRoles;

public class GetListCourseRolesQueryHandler
    : IQueryHandler<GetListCourseRolesQuery, GetListCourseRoleItemResponse[]>
{
    private readonly ICourseRoleQueries _courseRoleQueries;
    private readonly ITranslator _translator;

    public GetListCourseRolesQueryHandler(ICourseRoleQueries courseRoleQueries, ITranslator translator)
    {
        _courseRoleQueries = courseRoleQueries;
        _translator = translator;
    }

    public async Task<Result<GetListCourseRoleItemResponse[]>> Handle(
        GetListCourseRolesQuery request,
        CancellationToken cancellationToken)
    {
        GetListCourseRoleItemResponse[] courseRolesResponse = await _courseRoleQueries
            .GetListCourseRoles(request.CourseId, cancellationToken);

        foreach (GetListCourseRoleItemResponse courseRole in courseRolesResponse)
        {
            foreach (GetListCourseRolePermissionItemResponse permission in courseRole.CoursePermissions)
            {
                permission.Description = _translator.GetString(permission.Description);
            }
        }

        return courseRolesResponse;
    }
}
