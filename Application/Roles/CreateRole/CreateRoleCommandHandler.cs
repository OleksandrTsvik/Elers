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

        var role = new Role { Name = request.Name };

        _context.Roles.Add(role);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
