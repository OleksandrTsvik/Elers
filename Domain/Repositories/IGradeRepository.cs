namespace Domain.Repositories;

public interface IGradeRepository
{
    Task<double?> GetValueByAssignmentIdAndStudentIdAsync(
        Guid assignmentId,
        Guid studentId,
        CancellationToken cancellationToken = default);
}
