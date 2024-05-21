using Domain.Entities;

namespace Domain.Repositories;

public interface ICourseTabRepository
{
    Task<CourseTab?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(CourseTab courseTab);

    void Update(CourseTab courseTab);

    void Remove(CourseTab courseTab);
}
