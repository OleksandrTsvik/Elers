using Domain.Primitives;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Persistence.Repositories;

internal abstract class MongoDbRepository<TDocument>
    where TDocument : Entity
{
    protected readonly IMongoCollection<TDocument> Collection;

    protected MongoDbRepository(IMongoDatabase mongoDatabase, string collectionName)
    {
        Collection = mongoDatabase.GetCollection<TDocument>(collectionName);
    }

    public virtual async Task<TDocument?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync<TEntity>(
        Guid id,
        CancellationToken cancellationToken = default)
        where TEntity : TDocument
    {
        return await Collection
            .OfType<TEntity>()
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync(TDocument document, CancellationToken cancellationToken = default)
    {
        await Collection.InsertOneAsync(document, null, cancellationToken);
    }

    public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await Collection.DeleteOneAsync(x => x.Id == id, cancellationToken);
    }

    public async Task RemoveAsync(TDocument document, CancellationToken cancellationToken = default)
    {
        await Collection.DeleteOneAsync(x => x.Id == document.Id, cancellationToken);
    }

    public async Task RemoveRangeAsync(
        IEnumerable<TDocument> documents,
        CancellationToken cancellationToken = default)
    {
        await Collection.DeleteManyAsync(
            x => documents.Select(document => document.Id).Contains(x.Id),
            cancellationToken);
    }

    public Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Collection.Find(x => x.Id == id).AnyAsync(cancellationToken);
    }
}
