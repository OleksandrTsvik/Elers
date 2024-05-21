using Domain.Entities;

namespace Domain.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Role?> GetByIdWithPermissionsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<Role>> GetListAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default);

    void Add(Role role);

    void Update(Role role);

    void Remove(Role role);

    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
}
