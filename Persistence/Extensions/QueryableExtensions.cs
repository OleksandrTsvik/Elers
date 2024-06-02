using System.Linq.Expressions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        int count = await source.CountAsync(cancellationToken);

        List<T> items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }

    public static IQueryable<TSource> SortBy<TSource, TKey>(
        this IQueryable<TSource> source,
        SortOrder? sortOrder,
        Expression<Func<TSource, TKey>> keySelector) =>
        sortOrder switch
        {
            SortOrder.Desc or SortOrder.Descend => source.OrderByDescending(keySelector),
            _ => source.OrderBy(keySelector),
        };
}
