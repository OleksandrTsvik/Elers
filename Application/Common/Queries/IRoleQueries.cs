using Application.Common.Models;
using Application.Roles.GetListRoles;
using Application.Roles.GetListUserRoles;
using Domain.Entities;

namespace Application.Common.Queries;

public interface IRoleQueries
{
    Task<Role?> GetByIdWithPermissions(Guid id, CancellationToken cancellationToken = default);

    Task<PagedList<GetListRoleItemResponseDto>> GetListRoles(
        GetListRolesQueryParams queryParams,
        CancellationToken cancellationToken = default);

    Task<GetListUserRoleItemResponse[]> GetListUserRoles(CancellationToken cancellationToken = default);
}
