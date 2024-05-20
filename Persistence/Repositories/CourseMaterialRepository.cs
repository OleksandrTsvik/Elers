using Domain.Entities;
using Domain.Repositories;
using MongoDB.Driver;
using Persistence.Constants;

namespace Persistence.Repositories;

public class CourseMaterialRepository : ICourseMaterialRepository
{
    private readonly IMongoCollection<CourseMaterial> _courseMaterialsCollection;

    public CourseMaterialRepository(IMongoDatabase mongoDatabase)
    {
        _courseMaterialsCollection = mongoDatabase.GetCollection<CourseMaterial>(
            CollectionNames.CourseMaterials);
    }

    public async Task AddAsync(CourseMaterial courseMaterial, CancellationToken cancellationToken = default)
    {
        await _courseMaterialsCollection.InsertOneAsync(courseMaterial, null, cancellationToken);
    }
}
