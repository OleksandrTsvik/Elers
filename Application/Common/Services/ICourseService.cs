namespace Application.Common.Services;

public interface ICourseService
{
    Task RemoveMaterialsByCourseTabIdsAsync(
        IEnumerable<Guid> tabIds,
        bool deleteGrades = true,
        CancellationToken cancellationToken = default);
}
