using Domain.Entities;
using Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Persistence.Constants;

namespace Persistence.Repositories;

internal class TestSessionRespository : MongoDbRepository<TestSession>, ITestSessionRespository
{
    public TestSessionRespository(IMongoDatabase mongoDatabase)
        : base(mongoDatabase, CollectionNames.TestSessions)
    {
    }

    public async Task<TestSession?> GetByIdAndUserIdAsync(
        Guid id,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(x => x.Id == id && x.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TestSessionAnswer?> GetQuestionAnswerByIdAsync(
        Guid testSessionId,
        Guid testQuestionId,
        CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(x => x.Id == testSessionId)
            .Project(x => x.Answers.FirstOrDefault(answer => answer.QuestionId == testQuestionId))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<long> GetAttemptsCountAsync(
        Guid testId,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return Collection
            .Find(x => x.TestId == testId && x.UserId == userId)
            .CountDocumentsAsync(cancellationToken);
    }

    public async Task UpdateAnswerAsync(
        Guid testSessionId,
        TestSessionAnswer answer,
        CancellationToken cancellationToken = default)
    {
        FilterDefinition<TestSession> filter = Builders<TestSession>.Filter.Eq(x => x.Id, testSessionId);

        UpdateDefinition<TestSession> update = Builders<TestSession>.Update
            .Set("Answers.$[x]", answer);

        var questionId = new BsonBinaryData(answer.QuestionId, GuidRepresentation.Standard);

        var updateOptions = new UpdateOptions()
        {
            ArrayFilters = new List<ArrayFilterDefinition<BsonValue>>()
            {
                new BsonDocument("x.QuestionId",  new BsonDocument("$eq", questionId))
            }
        };

        await Collection.UpdateOneAsync(filter, update, updateOptions, cancellationToken);
    }

    public async Task UpdateFinishedAtAsync(
        Guid testSessionId,
        DateTime finishedAt,
        CancellationToken cancellationToken = default)
    {
        UpdateDefinition<TestSession> update = Builders<TestSession>.Update
            .Set(x => x.FinishedAt, finishedAt);

        await Collection.UpdateOneAsync(
            x => x.Id == testSessionId,
            update,
            null,
            cancellationToken);
    }

    public async Task RemoveQuestionAsync(Guid questionId, CancellationToken cancellationToken = default)
    {
        FilterDefinition<TestSession> filter = Builders<TestSession>.Filter.Empty;

        UpdateDefinition<TestSession> update = Builders<TestSession>.Update
            .PullFilter(x => x.Answers, answers => answers.QuestionId == questionId);

        await Collection.UpdateManyAsync(
            filter,
            update,
            null,
            cancellationToken);
    }

    public async Task RemoveRangeByTestIdAsync(Guid testId, CancellationToken cancellationToken = default)
    {
        await Collection.DeleteManyAsync(x => x.TestId == testId, cancellationToken);
    }

    public Task<bool> ExistsByIdAndUserIdAsync(
        Guid id,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return Collection.Find(x => x.Id == id && x.UserId == userId).AnyAsync(cancellationToken);
    }

    public Task<bool> ExistsActiveSessionAsync(
        Guid testId,
        Guid userId,
        int? timeLimitInMinutes,
        CancellationToken cancellationToken = default)
    {
        return Collection
            .Find(x => x.TestId == testId && x.UserId == userId &&
                x.FinishedAt == null && timeLimitInMinutes.HasValue &&
                x.StartedAt.AddMinutes(timeLimitInMinutes.Value) > DateTime.UtcNow)
            .AnyAsync(cancellationToken);
    }
}
