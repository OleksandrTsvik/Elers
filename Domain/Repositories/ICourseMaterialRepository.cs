using Domain.Entities;
using Domain.Shared;

namespace Domain.Repositories;

public interface ICourseMaterialRepository
{
    Task<CourseMaterial?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync<TEntity>(Guid id, CancellationToken cancellationToken = default)
        where TEntity : CourseMaterial;

    Task AddAsync(CourseMaterial courseMaterial, CancellationToken cancellationToken = default);

    Task UpdateAsync(CourseMaterial courseMaterial, CancellationToken cancellationToken = default);

    Task ReorderAsync(IEnumerable<ReorderItem> reorders, CancellationToken cancellationToken = default);

    Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
