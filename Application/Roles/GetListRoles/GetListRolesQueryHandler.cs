using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Application.Common.Queries;
using Domain.Shared;

namespace Application.Roles.GetListRoles;

public class GetListRolesQueryHandler : IQueryHandler<GetListRolesQuery, PagedList<GetListRoleItemResponse>>
{
    private readonly IRoleQueries _roleQueries;
    private readonly ITranslator _translator;

    public GetListRolesQueryHandler(IRoleQueries roleQueries, ITranslator translator)
    {
        _roleQueries = roleQueries;
        _translator = translator;
    }

    public async Task<Result<PagedList<GetListRoleItemResponse>>> Handle(
        GetListRolesQuery request,
        CancellationToken cancellationToken)
    {
        PagedList<GetListRoleItemResponseDto> roles = await _roleQueries.GetListRoles(
            request.QueryParams, cancellationToken);

        var response = roles.Items
            .Select(x => new GetListRoleItemResponse
            {
                Id = x.Id,
                Name = x.Name,
                Permissions = x.Permissions
                    .Select(permission => _translator.GetString(permission.ToString()))
                    .ToArray()
            })
            .ToList();

        return new PagedList<GetListRoleItemResponse>(
            response,
            roles.TotalCount,
            roles.CurrentPage,
            roles.PageSize);
    }
}
