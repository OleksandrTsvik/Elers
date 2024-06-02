using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Shared;

namespace Application.CoursePermissions.GetListCoursePermissions;

public class GetListCoursePermissionsQueryHandler
    : IQueryHandler<GetListCoursePermissionsQuery, GetListCoursePermissionItemResponse[]>
{
    private readonly ICoursePermissionQueries _coursePermissionQueries;
    private readonly ITranslator _translator;

    public GetListCoursePermissionsQueryHandler(
        ICoursePermissionQueries coursePermissionQueries,
        ITranslator translator)
    {
        _coursePermissionQueries = coursePermissionQueries;
        _translator = translator;
    }

    public async Task<Result<GetListCoursePermissionItemResponse[]>> Handle(
        GetListCoursePermissionsQuery request,
        CancellationToken cancellationToken)
    {
        GetListCoursePermissionItemResponse[] coursePermissions = await _coursePermissionQueries
            .GetListCoursePermissions(cancellationToken);

        foreach (GetListCoursePermissionItemResponse coursePermission in coursePermissions)
        {
            coursePermission.Description = _translator.GetString(coursePermission.Description);
        }

        return coursePermissions;
    }
}
