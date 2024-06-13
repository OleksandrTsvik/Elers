using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Application.Common.Services;
using Domain.Enums;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseTabs.ReorderCourseTabs;

public class ReorderCourseTabsCommandHandler : ICommandHandler<ReorderCourseTabsCommand>
{
    private readonly ICourseTabRepository _courseTabRepository;
    private readonly ICourseQueries _courseQueries;
    private readonly ICourseMemberPermissionService _courseMemberPermissionService;
    private readonly IUserContext _userContext;

    public ReorderCourseTabsCommandHandler(
        ICourseTabRepository courseTabRepository,
        ICourseQueries courseQueries,
        ICourseMemberPermissionService courseMemberPermissionService,
        IUserContext userContext)
    {
        _courseTabRepository = courseTabRepository;
        _courseQueries = courseQueries;
        _courseMemberPermissionService = courseMemberPermissionService;
        _userContext = userContext;
    }

    public async Task<Result> Handle(ReorderCourseTabsCommand request, CancellationToken cancellationToken)
    {
        if (request.Reorders.Length == 0)
        {
            return Result.Success();
        }

        Guid courseTabId = request.Reorders[0].Id;
        Guid? courseId = await _courseQueries.GetCourseIdByCourseTabId(courseTabId, cancellationToken);

        if (!courseId.HasValue)
        {
            return CourseErrors.NotFoundByTabId(courseTabId);
        }

        if (!await _courseMemberPermissionService
            .CheckCoursePermissionsAsync(
                _userContext.UserId,
                courseId.Value,
                [CoursePermissionType.CreateCourseTab, CoursePermissionType.UpdateCourseTab],
                [PermissionType.ManageCourse]))
        {
            return CourseTabErrors.AccessDenied();
        }

        Guid[] courseTabIds = await _courseQueries.GetCourseTabIds(courseId.Value, cancellationToken);

        await _courseTabRepository.ReorderAsync(
            request.Reorders.Where(x => courseTabIds.Contains(x.Id)),
            cancellationToken);

        return Result.Success();
    }
}
