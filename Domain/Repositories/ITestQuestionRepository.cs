using Domain.Entities;

namespace Domain.Repositories;

public interface ITestQuestionRepository
{
    Task<TestQuestion?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync<TEntity>(Guid id, CancellationToken cancellationToken = default)
        where TEntity : TestQuestion;

    Task AddAsync(TestQuestion testQuestion, CancellationToken cancellationToken = default);

    Task UpdateAsync(TestQuestion testQuestion, CancellationToken cancellationToken = default);

    Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
