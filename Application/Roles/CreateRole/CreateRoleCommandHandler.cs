using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Roles.CreateRole;

public class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateRoleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        Role? roleByName = await _context.Roles
            .FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

        if (roleByName is not null)
        {
            return RoleErrors.NameNotUnique(request.Name);
        }

        List<Permission> rolePermissions = await _context.Permissions
                .Where(x => request.PermissionIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

        var role = new Role
        {
            Name = request.Name,
            Permissions = rolePermissions
        };

        _context.Roles.Add(role);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
