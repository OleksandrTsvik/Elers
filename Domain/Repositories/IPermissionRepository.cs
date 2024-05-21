using Domain.Entities;

namespace Domain.Repositories;

public interface IPermissionRepository
{
    Task<List<Permission>> GetListAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default);
}
