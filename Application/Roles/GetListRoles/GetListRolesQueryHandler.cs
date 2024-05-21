using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Shared;

namespace Application.Roles.GetListRoles;

public class GetListRolesQueryHandler : IQueryHandler<GetListRolesQuery, GetListRoleItemResponse[]>
{
    private readonly IRoleQueries _roleQueries;

    public GetListRolesQueryHandler(IRoleQueries roleQueries)
    {
        _roleQueries = roleQueries;
    }

    public async Task<Result<GetListRoleItemResponse[]>> Handle(
        GetListRolesQuery request,
        CancellationToken cancellationToken)
    {
        return await _roleQueries.GetListRoles(cancellationToken);
    }
}
