using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Shared;

namespace Application.Roles.GetListRoles;

public class GetListRolesQueryHandler : IQueryHandler<GetListRolesQuery, GetListRoleItemResponse[]>
{
    private readonly IRoleQueries _roleQueries;
    private readonly ITranslator _translator;

    public GetListRolesQueryHandler(IRoleQueries roleQueries, ITranslator translator)
    {
        _roleQueries = roleQueries;
        _translator = translator;
    }

    public async Task<Result<GetListRoleItemResponse[]>> Handle(
        GetListRolesQuery request,
        CancellationToken cancellationToken)
    {
        GetListRoleItemResponseDto[] roles = await _roleQueries.GetListRoles(cancellationToken);

        return roles
            .Select(x => new GetListRoleItemResponse
            {
                Id = x.Id,
                Name = x.Name,
                Permissions = x.Permissions
                    .Select(permission => _translator.GetString(permission.ToString()))
                    .ToArray()
            })
            .ToArray();
    }
}
