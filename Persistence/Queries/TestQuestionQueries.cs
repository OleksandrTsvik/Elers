using Application.Common.Queries;
using Application.TestQuestions.GetTestQuestionIdsAndTypes;
using Domain.Entities;
using MongoDB.Driver;
using Persistence.Constants;

namespace Persistence.Queries;

public class TestQuestionQueries : ITestQuestionQueries
{
    private readonly IMongoCollection<TestQuestion> _testQuestionsCollection;

    public TestQuestionQueries(IMongoDatabase mongoDatabase)
    {
        _testQuestionsCollection = mongoDatabase.GetCollection<TestQuestion>(
            CollectionNames.TestQuestions);
    }

    public Task<List<GetTestQuestionIdsAndTypesResponse>> GetTestQuestionIdsByTestId(
        Guid testId,
        CancellationToken cancellationToken = default)
    {
        return _testQuestionsCollection
            .Find(x => x.TestId == testId)
            .SortBy(x => x.CreatedAt)
            .Project(x => new GetTestQuestionIdsAndTypesResponse
            {
                Id = x.Id,
                Type = x.Type
            })
            .ToListAsync(cancellationToken);
    }
}
