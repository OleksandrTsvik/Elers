namespace Application.Common.Services;

public interface ICourseMemberService
{
    Task<bool> IsCourseMemberByCourseTabIdAsync(
        Guid userId,
        Guid courseTabId,
        CancellationToken cancellationToken = default);
}
