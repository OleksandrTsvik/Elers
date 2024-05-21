using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Roles.UpdateRole;

public class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;

    public UpdateRoleCommandHandler(
        IUnitOfWork unitOfWork,
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository)
    {
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
    }

    public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        Role? role = await _roleRepository.GetByIdWithPermissionsAsync(request.RoleId, cancellationToken);

        if (role is null)
        {
            return RoleErrors.NotFound(request.RoleId);
        }

        if (await IsNotUniqueName(role.Name, request.Name, cancellationToken))
        {
            return RoleErrors.NameNotUnique(request.Name);
        }

        role.Name = request.Name;

        if (request.PermissionIds.Length == 0)
        {
            role.Permissions.Clear();
        }
        else
        {
            role.Permissions = await _permissionRepository.GetListAsync(
                request.PermissionIds, cancellationToken);
        }

        _roleRepository.Update(role);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private async Task<bool> IsNotUniqueName(
        string currentName,
        string newName,
        CancellationToken cancellationToken)
    {
        return currentName != newName
            && await _roleRepository.ExistsByNameAsync(newName, cancellationToken);
    }
}
