using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Roles.DeleteRole;

public class DeleteRoleCommandHandler : ICommandHandler<DeleteRoleCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRoleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        Role? role = await _context.Roles
            .FirstOrDefaultAsync(x => x.Id == request.RoleId, cancellationToken);

        if (role is null)
        {
            return RoleErrors.NotFound(request.RoleId);
        }

        _context.Roles.Remove(role);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
