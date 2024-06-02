using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Application.Common.Queries;
using Application.Common.Services;
using Domain.Enums;
using Domain.Shared;

namespace Application.CourseMembers.GetListCourseMembers;

public class GetListCourseMembersQueryHandler
    : IQueryHandler<GetListCourseMembersQuery, PagedList<CourseMemberListItem>>
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

    public async Task<Result<PagedList<CourseMemberListItem>>> Handle(
        GetListCourseMembersQuery request,
        CancellationToken cancellationToken)
    {
        bool isGetWithRoles = await IsGetWithRoles(request.CourseId);

        PagedList<CourseMemberListItem> courseMembersResponse = await _courseMemberQueries
                .GetListCourseMembers(
                    request.CourseId,
                    isGetWithRoles,
                    request.QueryParams,
                    cancellationToken);

        if (isGetWithRoles)
        {
            foreach (CourseMemberListItem courseMember in courseMembersResponse.Items)
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
