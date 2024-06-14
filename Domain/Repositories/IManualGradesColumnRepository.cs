using Domain.Entities;

namespace Domain.Repositories;

public interface IManualGradesColumnRepository
{
    Task<ManualGradesColumn?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(ManualGradesColumn column, CancellationToken cancellationToken = default);

    Task UpdateAsync(ManualGradesColumn column, CancellationToken cancellationToken = default);

    Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);

    Task RemoveRangeByCourseIdAsync(Guid courseId, CancellationToken cancellationToken = default);
}
