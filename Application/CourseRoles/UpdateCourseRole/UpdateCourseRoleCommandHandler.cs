using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseRoles.UpdateCourseRole;

public class UpdateCourseRoleCommandHandler : ICommandHandler<UpdateCourseRoleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseRoleRepository _courseRoleRepository;
    private readonly ICoursePermissionRepository _coursePermissionRepository;

    public UpdateCourseRoleCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseRoleRepository courseRoleRepository,
        ICoursePermissionRepository coursePermissionRepository)
    {
        _unitOfWork = unitOfWork;
        _courseRoleRepository = courseRoleRepository;
        _coursePermissionRepository = coursePermissionRepository;
    }

    public async Task<Result> Handle(UpdateCourseRoleCommand request, CancellationToken cancellationToken)
    {
        CourseRole? courseRole = await _courseRoleRepository.GetByIdWithCoursePermissionsAsync(
            request.RoleId, cancellationToken);

        if (courseRole is null)
        {
            return CourseRoleErrors.NotFound(request.RoleId);
        }

        if (await IsNotUniqueName(courseRole.CourseId, courseRole.Name, request.Name, cancellationToken))
        {
            return RoleErrors.NameNotUnique(request.Name);
        }

        courseRole.Name = request.Name;

        if (request.PermissionIds.Length == 0)
        {
            courseRole.CoursePermissions.Clear();
        }
        else
        {
            courseRole.CoursePermissions = await _coursePermissionRepository.GetListAsync(
                request.PermissionIds, cancellationToken);
        }

        _courseRoleRepository.Update(courseRole);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private async Task<bool> IsNotUniqueName(
        Guid courseId,
        string currentName,
        string newName,
        CancellationToken cancellationToken)
    {
        return currentName != newName
            && await _courseRoleRepository.ExistsByNameAsync(courseId, newName, cancellationToken);
    }
}
