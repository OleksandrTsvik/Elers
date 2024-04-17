using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Roles.UpdateRolePermissions;

public class UpdateRolePermissionsCommandHandler : ICommandHandler<UpdateRolePermissionsCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateRolePermissionsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateRolePermissionsCommand request, CancellationToken cancellationToken)
    {
        Role? role = await _context.Roles
            .FirstOrDefaultAsync(x => x.Id == request.RoleId, cancellationToken);

        if (role is null)
        {
            return RoleErrors.NotFound(request.RoleId);
        }

        if (request.PermissionIds.Length == 0)
        {
            role.Permissions.Clear();
        }
        else
        {
            List<Permission> newRolePermissions = await _context.Permissions
                .Where(x => request.PermissionIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            role.Permissions = newRolePermissions;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
