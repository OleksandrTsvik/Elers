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

    public async Task<TEntity?> GetByIdAsync<TEntity>(
        Guid id,
        CancellationToken cancellationToken = default)
        where TEntity : CourseMaterial
    {
        return await Collection
            .OfType<TEntity>()
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<string>> GetUniqueFileNamesByCourseTabIdAsync(
        Guid tabId,
        CancellationToken cancellationToken = default)
    {
        return Collection
            .OfType<CourseMaterialFile>()
            .Find(x => x.CourseTabId == tabId)
            .Project(x => x.UniqueFileName)
            .ToListAsync(cancellationToken);
    }

    public Task<List<string>> GetUniqueFileNamesByCourseTabIdsAsync(
        IEnumerable<Guid> tabIds,
        CancellationToken cancellationToken = default)
    {
        return Collection
            .OfType<CourseMaterialFile>()
            .Find(x => tabIds.Contains(x.CourseTabId))
            .Project(x => x.UniqueFileName)
            .ToListAsync(cancellationToken);
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
        }

        await Collection.UpdateOneAsync(
            x => x.Id == courseMaterial.Id,
            update,
            null,
            cancellationToken);
    }

    public async Task RemoveRangeByCourseTabIdAsync(Guid tabId, CancellationToken cancellationToken = default)
    {
        await Collection.DeleteManyAsync(x => x.CourseTabId == tabId, cancellationToken);
    }

    public async Task RemoveRangeByCourseTabIdsAsync(
        IEnumerable<Guid> tabIds,
        CancellationToken cancellationToken = default)
    {
        await Collection.DeleteManyAsync(x => tabIds.Contains(x.CourseTabId), cancellationToken);
    }
}
