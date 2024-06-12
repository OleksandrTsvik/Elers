using Domain.Entities;
using Domain.Repositories;
using MongoDB.Driver;
using Persistence.Constants;

namespace Persistence.Repositories;

internal class CourseMaterialRepository : MongoDbRepository<CourseMaterial>, ICourseMaterialRepository
{
    public CourseMaterialRepository(IMongoDatabase mongoDatabase)
        : base(mongoDatabase, CollectionNames.CourseMaterials)
    {
    }

    public async Task UpdateAsync(CourseMaterial courseMaterial, CancellationToken cancellationToken = default)
    {
        UpdateDefinition<CourseMaterial> update = Builders<CourseMaterial>.Update
            .Set(x => x.IsActive, courseMaterial.IsActive)
            .Set(x => x.Order, courseMaterial.Order);

        switch (courseMaterial)
        {
            case CourseMaterialContent content:
                update = update.Set(nameof(CourseMaterialContent.Content), content.Content);
                break;
            case CourseMaterialLink link:
                update = update
                    .Set(nameof(CourseMaterialLink.Title), link.Title)
                    .Set(nameof(CourseMaterialLink.Link), link.Link);
                break;
            case CourseMaterialFile file:
                update = update
                    .Set(nameof(CourseMaterialFile.Title), file.Title)
                    .Set(nameof(CourseMaterialFile.FileName), file.FileName)
                    .Set(nameof(CourseMaterialFile.UniqueFileName), file.UniqueFileName);
                break;
            case CourseMaterialAssignment assignment:
                update = update
                    .Set(nameof(CourseMaterialAssignment.Title), assignment.Title)
                    .Set(nameof(CourseMaterialAssignment.Description), assignment.Description)
                    .Set(nameof(CourseMaterialAssignment.Deadline), assignment.Deadline)
                    .Set(nameof(CourseMaterialAssignment.MaxFiles), assignment.MaxFiles)
                    .Set(nameof(CourseMaterialAssignment.MaxGrade), assignment.MaxGrade);
                break;
            case CourseMaterialTest test:
                update = update
                    .Set(nameof(CourseMaterialTest.Title), test.Title)
                    .Set(nameof(CourseMaterialTest.Description), test.Description)
                    .Set(nameof(CourseMaterialTest.NumberAttempts), test.NumberAttempts)
                    .Set(nameof(CourseMaterialTest.TimeLimitInMinutes), test.TimeLimitInMinutes)
                    .Set(nameof(CourseMaterialTest.Deadline), test.Deadline)
                    .Set(nameof(CourseMaterialTest.GradingMethod), test.GradingMethod)
                    .Set(nameof(CourseMaterialTest.ShuffleQuestions), test.ShuffleQuestions);
                break;
        }

        await Collection.UpdateOneAsync(
            x => x.Id == courseMaterial.Id,
            update,
            null,
            cancellationToken);
    }
}
