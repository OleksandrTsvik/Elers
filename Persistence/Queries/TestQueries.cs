using Application.Common.Queries;
using Application.Tests.DTOs;
using Domain.Entities;
using MongoDB.Driver;
using Persistence.Constants;

namespace Persistence.Queries;

public class TestQueries : ITestQueries
{
    private readonly IMongoCollection<TestSession> _testSessionsCollection;

    public TestQueries(IMongoDatabase mongoDatabase)
    {
        _testSessionsCollection = mongoDatabase.GetCollection<TestSession>(
            CollectionNames.TestSessions);
    }

    public async Task<TestSessionDto?> GetTestSessionDtoById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _testSessionsCollection
            .Find(x => x.Id == id)
            .Project(x => new TestSessionDto
            {
                Id = x.Id,
                TestId = x.TestId,
                UserId = x.UserId,
                StartedAt = x.StartedAt,
                FinishedAt = x.FinishedAt,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TestSessionDto?> GetTestSessionDtoByIdAndUserId(
        Guid id,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _testSessionsCollection
            .Find(x => x.Id == id && x.UserId == userId)
            .Project(x => new TestSessionDto
            {
                Id = x.Id,
                TestId = x.TestId,
                UserId = x.UserId,
                StartedAt = x.StartedAt,
                FinishedAt = x.FinishedAt,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<TestSessionDto>> GetTestSessionDtosByTestIdAndUserId(
        Guid testId,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return _testSessionsCollection
            .Find(x => x.TestId == testId && x.UserId == userId)
            .Project(x => new TestSessionDto
            {
                Id = x.Id,
                TestId = x.TestId,
                UserId = x.UserId,
                StartedAt = x.StartedAt,
                FinishedAt = x.FinishedAt,
            })
            .ToListAsync(cancellationToken);
    }
}
