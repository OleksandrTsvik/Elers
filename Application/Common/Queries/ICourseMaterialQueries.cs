using Domain.Entities;

namespace Application.Common.Queries;

public interface ICourseMaterialQueries
{
    Task<List<CourseMaterial>> GetListCourseMaterialsAsync(CancellationToken cancellationToken = default);
}
