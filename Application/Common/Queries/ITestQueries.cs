using Application.Tests.DTOs;

namespace Application.Common.Queries;

public interface ITestQueries
{
    Task<TestSessionDto?> GetTestSessionDtoById(Guid id, CancellationToken cancellationToken = default);

    Task<TestSessionDto?> GetTestSessionDtoByIdAndUserId(
        Guid id,
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<List<TestSessionDto>> GetTestSessionDtosByTestIdAndUserId(
        Guid testId,
        Guid userId,
        CancellationToken cancellationToken = default);
}
