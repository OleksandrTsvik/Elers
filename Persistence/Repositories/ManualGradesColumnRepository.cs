using Domain.Entities;
using Domain.Repositories;
using MongoDB.Driver;
using Persistence.Constants;

namespace Persistence.Repositories;

internal class ManualGradesColumnRepository
    : MongoDbRepository<ManualGradesColumn>, IManualGradesColumnRepository
{
    public ManualGradesColumnRepository(IMongoDatabase mongoDatabase)
        : base(mongoDatabase, CollectionNames.ManualGradesColumns)
    {
    }

    public async Task UpdateAsync(ManualGradesColumn column, CancellationToken cancellationToken = default)
    {
        UpdateDefinition<ManualGradesColumn> update = Builders<ManualGradesColumn>.Update
            .Set(x => x.Title, column.Title)
            .Set(x => x.Date, column.Date)
            .Set(x => x.MaxGrade, column.MaxGrade);

        await Collection.UpdateOneAsync(
            x => x.Id == column.Id,
            update,
            null,
            cancellationToken);
    }

    public async Task RemoveRangeByCourseIdAsync(Guid courseId, CancellationToken cancellationToken = default)
    {
        await Collection.DeleteManyAsync(x => x.CourseId == courseId, cancellationToken);
    }
}
