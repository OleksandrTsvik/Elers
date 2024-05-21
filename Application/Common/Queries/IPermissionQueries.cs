using Application.Permissions.GetListPermissions;
using Domain.Entities;

namespace Application.Common.Queries;

public interface IPermissionQueries
{
    Task<List<Permission>> GetList(CancellationToken cancellationToken = default);

    Task<GetListPermissionItemResponseDto[]> GetListPermissions(
        CancellationToken cancellationToken = default);
}
