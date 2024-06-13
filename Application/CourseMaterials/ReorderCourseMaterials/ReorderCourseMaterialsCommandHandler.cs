using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Application.Common.Services;
using Domain.Enums;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.ReorderCourseMaterials;

public class ReorderCourseMaterialsCommandHandler : ICommandHandler<ReorderCourseMaterialsCommand>
{
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly ICourseMaterialQueries _courseMaterialQueries;
    private readonly ICourseMemberPermissionService _courseMemberPermissionService;
    private readonly IUserContext _userContext;

    public ReorderCourseMaterialsCommandHandler(
        ICourseMaterialRepository courseMaterialRepository,
        ICourseMaterialQueries courseMaterialQueries,
        ICourseMemberPermissionService courseMemberPermissionService,
        IUserContext userContext)
    {
        _courseMaterialRepository = courseMaterialRepository;
        _courseMaterialQueries = courseMaterialQueries;
        _courseMemberPermissionService = courseMemberPermissionService;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        ReorderCourseMaterialsCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Reorders.Length == 0)
        {
            return Result.Success();
        }

        Guid courseMaterialId = request.Reorders[0].Id;
        Guid? courseTabId = await _courseMaterialQueries.GetCourseTabId(courseMaterialId, cancellationToken);

        if (!courseTabId.HasValue)
        {
            return CourseMaterialErrors.NotFound(courseMaterialId);
        }

        if (!await _courseMemberPermissionService
            .CheckCoursePermissionsByCourseTabIdAsync(
                _userContext.UserId,
                courseTabId.Value,
                [CoursePermissionType.CreateCourseMaterial, CoursePermissionType.UpdateCourseMaterial],
                [PermissionType.ManageCourse]))
        {
            return CourseMaterialErrors.AccessDenied();
        }

        List<Guid> courseMaterialIds = await _courseMaterialQueries.GetCourseMaterialIdsByTabId(
            courseTabId.Value, cancellationToken);

        await _courseMaterialRepository.ReorderAsync(
            request.Reorders.Where(x => courseMaterialIds.Contains(x.Id)),
            cancellationToken);

        return Result.Success();
    }
}
