namespace Application.Common.Services;

public interface ICourseMemberService
{
    Task<bool> IsCourseStudentAsync(
        Guid studentId,
        Guid courseId,
        CancellationToken cancellationToken = default);

    Task<bool> IsCourseMemberByCourseTabIdAsync(
        Guid userId,
        Guid courseTabId,
        CancellationToken cancellationToken = default);
}
