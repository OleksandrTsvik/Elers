using System.Linq.Expressions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> query,
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

    public static IQueryable<TSource> SortBy<TSource, TKey>(
        this IQueryable<TSource> query,
        SortOrder? sortOrder,
        Expression<Func<TSource, TKey>> keySelector) =>
        sortOrder switch
        {
            SortOrder.Desc or SortOrder.Descend => query.OrderByDescending(keySelector),
            _ => query.OrderBy(keySelector),
        };

    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> query,
        bool condition,
        Expression<Func<T, bool>> predicate) =>
        condition ? query.Where(predicate) : query;
}
