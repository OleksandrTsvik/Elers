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

    public async Task RemoveRangeByCourseTabIdAsync(Guid tabId, CancellationToken cancellationToken = default)
    {
        await Collection.DeleteManyAsync(x => x.CourseTabId == tabId, cancellationToken);
    }

    public async Task RemoveRangeByCourseTabIdsAsync(
        List<Guid> tabIds,
        CancellationToken cancellationToken = default)
    {
        await Collection.DeleteManyAsync(x => tabIds.Contains(x.CourseTabId), cancellationToken);
    }
}
