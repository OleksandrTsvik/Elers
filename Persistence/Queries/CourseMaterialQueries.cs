using Application.Common.Queries;
using Domain.Entities;
using MongoDB.Driver;
using Persistence.Constants;

namespace Persistence.Queries;

public class CourseMaterialQueries : ICourseMaterialQueries
{
    private readonly IMongoCollection<CourseMaterial> _courseMaterialCollection;

    public CourseMaterialQueries(IMongoDatabase mongoDatabase)
    {
        _courseMaterialCollection = mongoDatabase.GetCollection<CourseMaterial>(
            CollectionNames.CourseMaterials);
    }

    public async Task<List<CourseMaterial>> GetListCourseMaterialsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _courseMaterialCollection
            .Find(_ => true)
            .ToListAsync(cancellationToken);
    }
}
