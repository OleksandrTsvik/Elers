using Domain.Entities;
using Domain.Repositories;
using MongoDB.Driver;
using Persistence.Constants;

namespace Persistence.Repositories;

internal class TestQuestionRepository : MongoDbRepository<TestQuestion>, ITestQuestionRepository
{
    public TestQuestionRepository(IMongoDatabase mongoDatabase)
        : base(mongoDatabase, CollectionNames.TestQuestions)
    {
    }

    public async Task UpdateAsync(TestQuestion testQuestion, CancellationToken cancellationToken = default)
    {
        UpdateDefinition<TestQuestion> update = Builders<TestQuestion>.Update
            .Set(x => x.Text, testQuestion.Text)
            .Set(x => x.Points, testQuestion.Points);

        switch (testQuestion)
        {
            case TestQuestionInput input:
                update = update.Set(nameof(TestQuestionInput.Answer), input.Answer);
                break;
            case TestQuestionSingleChoice singleChoice:
                update = update.Set(nameof(TestQuestionSingleChoice.Options), singleChoice.Options);
                break;
            case TestQuestionMultipleChoice multipleChoice:
                update = update.Set(nameof(TestQuestionMultipleChoice.Options), multipleChoice.Options);
                break;
        }

        await Collection.UpdateOneAsync(
            x => x.Id == testQuestion.Id,
            update,
            null,
            cancellationToken);
    }

    public async Task RemoveRangeByTestIdAsync(Guid testId, CancellationToken cancellationToken = default)
    {
        await Collection.DeleteManyAsync(x => x.TestId == testId, cancellationToken);
    }
}
