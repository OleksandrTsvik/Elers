using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Roles.UpdateRole;

public class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateRoleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        Role? role = await _context.Roles
            .Include(x => x.Permissions)
            .FirstOrDefaultAsync(x => x.Id == request.RoleId, cancellationToken);

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
            List<Permission> newRolePermissions = await _context.Permissions
                .Where(x => request.PermissionIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            role.Permissions = newRolePermissions;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private async Task<bool> IsNotUniqueName(
        string currentName,
        string newName,
        CancellationToken cancellationToken)
    {
        return currentName != newName
            && await _context.Roles.AnyAsync(x => x.Name == newName, cancellationToken);
    }
}
