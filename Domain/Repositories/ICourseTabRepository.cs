using Domain.Entities;
using Domain.Shared;

namespace Domain.Repositories;

public interface ICourseTabRepository
{
    Task<CourseTab?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(CourseTab courseTab);

    void Update(CourseTab courseTab);

    Task ReorderAsync(IEnumerable<ReorderItem> reorders, CancellationToken cancellationToken = default);

    void Remove(CourseTab courseTab);

    Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
