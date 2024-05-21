using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> GetByIdWithRolesAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<User?> GetByIdWithRolesAndPermissionsAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<User?> GetByEmailWithRolesAndPermissionsAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<List<string>> GetPermissionsAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    void Add(User user);

    void Update(User user);

    void Remove(User user);

    Task<bool> IsEmailUniqueAsync(
        string email,
        CancellationToken cancellationToken = default);
}
