using Application.Roles.GetListRoles;
using Application.Roles.GetListUserRoles;
using Domain.Entities;

namespace Application.Common.Queries;

public interface IRoleQueries
{
    Task<Role?> GetByIdWithPermissions(Guid id, CancellationToken cancellationToken = default);

    Task<GetListRoleItemResponse[]> GetListRoles(CancellationToken cancellationToken = default);

    Task<GetListUserRoleItemResponse[]> GetListUserRoles(CancellationToken cancellationToken = default);
}
