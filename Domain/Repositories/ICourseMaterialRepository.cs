using Domain.Entities;

namespace Domain.Repositories;

public interface ICourseMaterialRepository
{
    Task AddAsync(CourseMaterial courseMaterial, CancellationToken cancellationToken = default);

    Task RemoveRangeByCourseTabIdAsync(Guid tabId, CancellationToken cancellationToken = default);

    Task RemoveRangeByCourseTabIdsAsync(List<Guid> tabIds, CancellationToken cancellationToken = default);
}
