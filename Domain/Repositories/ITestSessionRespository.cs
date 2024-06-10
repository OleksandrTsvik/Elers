using Domain.Entities;

namespace Domain.Repositories;

public interface ITestSessionRespository
{
    Task<TestSession?> GetByIdAndUserIdAsync(
        Guid id,
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<TestSessionAnswer?> GetQuestionAnswerByIdAsync(
        Guid testSessionId,
        Guid testQuestionId,
        CancellationToken cancellationToken = default);

    Task<long> GetAttemptsCountAsync(
        Guid testId,
        Guid userId,
        CancellationToken cancellationToken = default);

    Task AddAsync(TestSession testSession, CancellationToken cancellationToken = default);

    Task UpdateAnswerAsync(
        Guid testSessionId,
        TestSessionAnswer answer,
        CancellationToken cancellationToken = default);

    Task UpdateFinishedAtAsync(
        Guid testSessionId,
        DateTime finishedAt,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByIdAndUserIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    Task<bool> ExistsActiveSessionAsync(
        Guid testId,
        Guid userId,
        int? timeLimitInMinutes,
        CancellationToken cancellationToken = default);
}
