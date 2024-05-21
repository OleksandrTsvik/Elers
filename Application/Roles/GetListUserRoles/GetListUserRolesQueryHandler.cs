using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Shared;

namespace Application.Roles.GetListUserRoles;

public class GetListUserRolesQueryHandler
    : IQueryHandler<GetListUserRolesQuery, GetListUserRoleItemResponse[]>
{
    private readonly IRoleQueries _roleQueries;

    public GetListUserRolesQueryHandler(IRoleQueries roleQueries)
    {
        _roleQueries = roleQueries;
    }

    public async Task<Result<GetListUserRoleItemResponse[]>> Handle(
        GetListUserRolesQuery request,
        CancellationToken cancellationToken)
    {
        return await _roleQueries.GetListUserRoles(cancellationToken);
    }
}
