using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Roles.GetRoleById;

public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, GetRoleByIdResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly ITranslator _translator;

    public GetRoleByIdQueryHandler(
        IApplicationDbContext context,
        ITranslator translator)
    {
        _context = context;
        _translator = translator;
    }

    public async Task<Result<GetRoleByIdResponse>> Handle(
        GetRoleByIdQuery request,
        CancellationToken cancellationToken)
    {
        Role? role = await _context.Roles
            .Include(x => x.Permissions)
            .Where(x => x.Id == request.RoleId)
            .FirstOrDefaultAsync(cancellationToken);

        if (role is null)
        {
            return RoleErrors.NotFound(request.RoleId);
        }

        List<Permission> permissions = await _context.Permissions
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);

        return new GetRoleByIdResponse
        {
            Id = role.Id,
            Name = role.Name,
            Permissions = GetPermissionsResponse(role, permissions)
        };
    }

    private PermissionResponse[] GetPermissionsResponse(
        Role role,
        List<Permission> permissions)
    {
        return permissions.Select(permission => new PermissionResponse
        {
            Id = permission.Id,
            Name = permission.Name,
            Description = _translator.GetString(permission.Name),
            IsSelected = role.Permissions.Contains(permission)
        }).ToArray();
    }
}
