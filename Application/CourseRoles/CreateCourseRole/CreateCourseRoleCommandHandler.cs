using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseRoles.CreateCourseRole;

public class CreateCourseRoleCommandHandler : ICommandHandler<CreateCourseRoleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseRoleRepository _courseRoleRepository;
    private readonly ICoursePermissionRepository _coursePermissionRepository;

    public CreateCourseRoleCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseRepository courseRepository,
        ICourseRoleRepository courseRoleRepository,
        ICoursePermissionRepository coursePermissionRepository)
    {
        _unitOfWork = unitOfWork;
        _courseRepository = courseRepository;
        _courseRoleRepository = courseRoleRepository;
        _coursePermissionRepository = coursePermissionRepository;
    }

    public async Task<Result> Handle(CreateCourseRoleCommand request, CancellationToken cancellationToken)
    {
        if (!await _courseRepository.ExistsByIdAsync(request.CourseId, cancellationToken))
        {
            return CourseErrors.NotFound(request.CourseId);
        }

        if (await _courseRoleRepository.ExistsByNameAsync(request.CourseId, request.Name, cancellationToken))
        {
            return CourseRoleErrors.NameNotUnique(request.Name);
        }

        List<CoursePermission> coursePermissions = await _coursePermissionRepository.GetListAsync(
            request.PermissionIds, cancellationToken);

        var courseRole = new CourseRole
        {
            CourseId = request.CourseId,
            Name = request.Name,
            CoursePermissions = coursePermissions
        };

        _courseRoleRepository.Add(courseRole);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
