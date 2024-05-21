using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;

namespace Application.Roles.GetRoleById;

public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, GetRoleByIdResponse>
{
    private readonly IRoleQueries _roleQueries;
    private readonly IPermissionQueries _permissionQueries;
    private readonly ITranslator _translator;

    public GetRoleByIdQueryHandler(
        IRoleQueries roleQueries,
        IPermissionQueries permissionQueries,
        ITranslator translator)
    {
        _roleQueries = roleQueries;
        _permissionQueries = permissionQueries;
        _translator = translator;
    }

    public async Task<Result<GetRoleByIdResponse>> Handle(
        GetRoleByIdQuery request,
        CancellationToken cancellationToken)
    {
        Role? role = await _roleQueries.GetByIdWithPermissions(request.RoleId, cancellationToken);

        if (role is null)
        {
            return RoleErrors.NotFound(request.RoleId);
        }

        List<Permission> permissions = await _permissionQueries.GetList(cancellationToken);

        return new GetRoleByIdResponse
        {
            Id = role.Id,
            Name = role.Name,
            Permissions = GetPermissionsResponse(role, permissions)
        };
    }

    private GetRoleByIdPermissionResponse[] GetPermissionsResponse(
        Role role,
        List<Permission> permissions)
    {
        return permissions
            .Select(permission => new GetRoleByIdPermissionResponse
            {
                Id = permission.Id,
                Name = permission.Name,
                Description = _translator.GetString(permission.Name.ToString()),
                IsSelected = role.Permissions.Any(x => x.Name == permission.Name)
            })
            .ToArray();
    }
}
