using System.Linq.Expressions;
using Application.Common.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Persistence.Extensions;

public static class MongoQueryableExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IMongoQueryable<T> query,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        int count = await query.CountAsync(cancellationToken);

        List<T> items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }

    public static IMongoQueryable<T> WhereIf<T>(
        this IMongoQueryable<T> query,
        bool condition,
        Expression<Func<T, bool>> predicate) =>
        condition ? query.Where(predicate) : query;
}
