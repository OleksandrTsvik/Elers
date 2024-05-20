using Domain.Entities;

namespace Domain.Repositories;

public interface ICourseMaterialRepository
{
    Task AddAsync(CourseMaterial courseMaterial, CancellationToken cancellationToken = default);
}
