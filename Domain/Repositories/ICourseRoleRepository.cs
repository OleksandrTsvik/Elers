using Domain.Entities;

namespace Domain.Repositories;

public interface ICourseRoleRepository
{
    Task<CourseRole?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<CourseRole?> GetByIdWithCoursePermissionsAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(CourseRole courseRole);

    void Update(CourseRole courseRole);

    void Remove(CourseRole courseRole);

    Task<bool> ExistsByNameAsync(Guid courseId, string name, CancellationToken cancellationToken = default);
}
