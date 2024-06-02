using Domain.Entities;

namespace Domain.Repositories;

public interface ICoursePermissionRepository
{
    Task<List<CoursePermission>> GetListAsync(
        IEnumerable<Guid> permissionIds,
        CancellationToken cancellationToken = default);
}
