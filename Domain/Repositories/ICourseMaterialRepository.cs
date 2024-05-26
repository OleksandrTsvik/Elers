using Domain.Entities;

namespace Domain.Repositories;

public interface ICourseMaterialRepository
{
    Task<CourseMaterial?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync<TEntity>(Guid id, CancellationToken cancellationToken = default)
        where TEntity : CourseMaterial;

    Task<List<string>> GetUniqueFileNamesByCourseTabIdAsync(
        Guid tabId,
        CancellationToken cancellationToken = default);

    Task<List<string>> GetUniqueFileNamesByCourseTabIdsAsync(
        IEnumerable<Guid> tabIds,
        CancellationToken cancellationToken = default);

    Task AddAsync(CourseMaterial courseMaterial, CancellationToken cancellationToken = default);

    Task UpdateAsync(CourseMaterial courseMaterial, CancellationToken cancellationToken = default);

    Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);

    Task RemoveRangeByCourseTabIdAsync(Guid tabId, CancellationToken cancellationToken = default);

    Task RemoveRangeByCourseTabIdsAsync(
        IEnumerable<Guid> tabIds,
        CancellationToken cancellationToken = default);
}
