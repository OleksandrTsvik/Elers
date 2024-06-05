namespace Application.Common.Services;

public interface ICourseService
{
    Task<bool> IsCourseMemberByCourseTabIdAsync(
        Guid userId,
        Guid courseTabId,
        CancellationToken cancellationToken = default);
}
