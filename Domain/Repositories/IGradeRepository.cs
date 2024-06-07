using Domain.Entities;

namespace Domain.Repositories;

public interface IGradeRepository
{
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
}
