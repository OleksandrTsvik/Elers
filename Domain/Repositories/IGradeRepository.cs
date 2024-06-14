using Domain.Entities;
using Domain.Enums;

namespace Domain.Repositories;

public interface IGradeRepository
{
    Task<Grade?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<GradeAssignment?> GetByAssignmentIdAndStudentIdAsync(
        Guid assignmentId,
        Guid studentId,
        CancellationToken cancellationToken = default);

    Task<GradeTest?> GetByTestIdAndStudentIdAsync(
        Guid testId,
        Guid studentId,
        CancellationToken cancellationToken = default);

    Task<double?> GetValueByAssignmentIdAndStudentIdAsync(
        Guid assignmentId,
        Guid studentId,
        CancellationToken cancellationToken = default);

    Task<List<Grade>> GetByCourseIdAsync(
        Guid courseId,
        CancellationToken cancellationToken = default);

    Task<List<Grade>> GetByCourseIdAndStudentIdAsync(
        Guid courseId,
        Guid studentId,
        CancellationToken cancellationToken = default);

    Task AddAsync(Grade grade, CancellationToken cancellationToken = default);

    Task UpdateAsync(Grade grade, CancellationToken cancellationToken = default);

    Task UpdateTestGradingMethodAsync(
        Guid testId,
        GradingMethod gradingMethod,
        CancellationToken cancellationToken = default);

    Task RemoveRangeByCourseIdAsync(Guid courseId, CancellationToken cancellationToken = default);

    Task RemoveRangeByAssignmentIdAsync(Guid assignmentId, CancellationToken cancellationToken = default);

    Task RemoveRangeByTestIdAsync(Guid testId, CancellationToken cancellationToken = default);

    Task RemoveRangeByColumnIdAsync(Guid columnId, CancellationToken cancellationToken = default);

    Task<bool> ExistsByStudentIdAndColumnIdAsync(
        Guid studentId,
        Guid columnId,
        CancellationToken cancellationToken = default);
}
