using Domain.Entities;

namespace Domain.Repositories;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Course?> GetByIdWithCourseTabsAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(Course course);

    void Update(Course course);

    void Remove(Course course);

    Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
