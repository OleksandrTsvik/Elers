using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Roles.GetListRoles;

public class GetListRolesQueryHandler : IQueryHandler<GetListRolesQuery, GetListRoleItemResponse[]>
{
    private readonly IApplicationDbContext _context;

    public GetListRolesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetListRoleItemResponse[]>> Handle(
        GetListRolesQuery request,
        CancellationToken cancellationToken)
    {
        GetListRoleItemResponse[] roles = await _context.Roles
            .Include(x => x.Permissions)
            .Select(x => new GetListRoleItemResponse
            {
                Id = x.Id,
                Name = x.Name,
                Permissions = x.Permissions
                    .Select(permission => permission.Name)
                    .ToArray()
            })
            .OrderBy(x => x.Name)
            .ToArrayAsync(cancellationToken);

        return roles;
    }
}
