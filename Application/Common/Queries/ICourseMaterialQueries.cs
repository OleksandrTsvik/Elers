using Application.Courses.GetCourseById;
using Domain.Entities;

namespace Application.Common.Queries;

public interface ICourseMaterialQueries
{
    Task<List<CourseMaterial>> GetListCourseMaterials(CancellationToken cancellationToken = default);

    Task<List<CourseMaterial>> GetListByTabId(Guid tabId, CancellationToken cancellationToken = default);

    Task<List<CourseMaterial>> GetListByTabIds(
        IEnumerable<Guid> tabIds,
        CancellationToken cancellationToken = default);

    Task<List<MaterialCountResponseDto>> GetListMaterialCountByCourseTabIdsAsync(
        IEnumerable<Guid> tabIds,
        CancellationToken cancellationToken = default);
}
