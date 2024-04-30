using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Roles.GetListUserRoles;

public class GetListUserRolesQueryHandler
    : IQueryHandler<GetListUserRolesQuery, GetListUserRoleItemResponse[]>
{
    private readonly IApplicationDbContext _context;

    public GetListUserRolesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetListUserRoleItemResponse[]>> Handle(
        GetListUserRolesQuery request,
        CancellationToken cancellationToken)
    {
        GetListUserRoleItemResponse[] roles = await _context.Roles
            .Select(x => new GetListUserRoleItemResponse
            {
                Id = x.Id,
                Name = x.Name
            })
            .OrderBy(x => x.Name)
            .ToArrayAsync(cancellationToken);

        return roles;
    }
}
