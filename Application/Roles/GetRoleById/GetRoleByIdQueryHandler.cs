using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Roles.GetRoleById;

public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, GetRoleByIdResponse>
{
    private readonly IApplicationDbContext _context;

    public GetRoleByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetRoleByIdResponse>> Handle(
        GetRoleByIdQuery request,
        CancellationToken cancellationToken)
    {
        GetRoleByIdResponse? role = await _context.Roles
            .Include(x => x.Permissions)
            .Where(x => x.Id == request.RoleId)
            .Select(x => new GetRoleByIdResponse
            {
                Id = x.Id,
                Name = x.Name,
                Permissions = x.Permissions.Select(permission => new PermissionResponse
                {
                    Id = permission.Id,
                    Name = permission.Name
                })
                .ToArray()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (role is null)
        {
            return RoleErrors.NotFound(request.RoleId);
        }

        return role;
    }
}
