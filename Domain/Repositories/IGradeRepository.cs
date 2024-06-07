using Domain.Entities;

namespace Domain.Repositories;

public interface IGradeRepository
{
    Task<List<Grade>> GetByCourseIdAsync(
        Guid courseId,
        CancellationToken cancellationToken = default);

    Task<List<Grade>> GetByCourseIdAndStudentIdAsync(
        Guid courseId,
        Guid studentId,
        CancellationToken cancellationToken = default);

    Task<GradeAssignment?> GetByAssignmentIdAndStudentIdAsync(
        Guid assignmentId,
        Guid studentId,
        CancellationToken cancellationToken = default);

    Task<double?> GetValueByAssignmentIdAndStudentIdAsync(
        Guid assignmentId,
        Guid studentId,
        CancellationToken cancellationToken = default);

    Task AddAsync(Grade grade, CancellationToken cancellationToken = default);

    Task UpdateAsync(Grade grade, CancellationToken cancellationToken = default);

    Task RemoveRangeByCourseIdAsync(Guid courseId, CancellationToken cancellationToken = default);

    Task RemoveRangeByAssignmentIdAsync(Guid assignmentId, CancellationToken cancellationToken = default);

    Task RemoveRangeByCourseTabIdAsync(Guid courseTabId, CancellationToken cancellationToken = default);
}
