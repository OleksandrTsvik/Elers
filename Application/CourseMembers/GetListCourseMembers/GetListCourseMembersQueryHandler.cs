using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Application.Common.Services;
using Domain.Enums;
using Domain.Shared;

namespace Application.CourseMembers.GetListCourseMembers;

public class GetListCourseMembersQueryHandler
    : IQueryHandler<GetListCourseMembersQuery, GetListCourseMemberItemResponse[]>
{
    private readonly ICourseMemberQueries _courseMemberQueries;
    private readonly ICourseMemberPermissionService _courseMemberPermissionService;
    private readonly ITranslator _translator;
    private readonly IUserContext _userContext;

    public GetListCourseMembersQueryHandler(
        ICourseMemberQueries courseMemberQueries,
        ICourseMemberPermissionService courseMemberPermissionService,
        ITranslator translator,
        IUserContext userContext)
    {
        _courseMemberQueries = courseMemberQueries;
        _courseMemberPermissionService = courseMemberPermissionService;
        _translator = translator;
        _userContext = userContext;
    }

    public async Task<Result<GetListCourseMemberItemResponse[]>> Handle(
        GetListCourseMembersQuery request,
        CancellationToken cancellationToken)
    {
        GetListCourseMemberItemResponse[] courseMembersResponse;
        bool isGetWithRoles = await IsGetWithRoles(request.CourseId);

        if (isGetWithRoles)
        {
            courseMembersResponse = await _courseMemberQueries
                .GetListCourseMembersWithRoles(request.CourseId, cancellationToken);
        }
        else
        {
            courseMembersResponse = await _courseMemberQueries
                .GetListCourseMembers(request.CourseId, cancellationToken);
        }

        if (isGetWithRoles)
        {
            foreach (GetListCourseMemberItemResponse courseMember in courseMembersResponse)
            {
                if (courseMember.CourseRole is null || courseMember.CourseRole.Description is null)
                {
                    continue;
                }

                courseMember.CourseRole.Description = _translator.GetString(
                    courseMember.CourseRole.Description);
            }
        }

        return courseMembersResponse;
    }

    private Task<bool> IsGetWithRoles(Guid courseId)
    {
        return _courseMemberPermissionService.CheckCoursePermissionsAsync(
            _userContext.UserId,
            courseId,
            [CoursePermissionType.ChangeCourseMemberRole],
            [PermissionType.ManageCourse]);
    }
}
