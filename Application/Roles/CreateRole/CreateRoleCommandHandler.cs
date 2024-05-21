using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Roles.CreateRole;

public class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;

    public CreateRoleCommandHandler(
        IUnitOfWork unitOfWork,
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository)
    {
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
    }

    public async Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        if (await _roleRepository.ExistsByNameAsync(request.Name, cancellationToken))
        {
            return RoleErrors.NameNotUnique(request.Name);
        }

        List<Permission> rolePermissions = await _permissionRepository.GetListAsync(
            request.PermissionIds, cancellationToken);

        var role = new Role
        {
            Name = request.Name,
            Permissions = rolePermissions
        };

        _roleRepository.Add(role);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
