using Domain.Entities;

namespace Domain.Repositories;

public interface ISubmittedAssignmentRepository
{
    Task<SubmittedAssignment?> GetByAssignmentIdAndStudentIdAsync(
        Guid assignmentId,
        Guid studentId,
        CancellationToken cancellationToken = default);

    Task<SubmittedAssignment?> GetByUniqueFileNameAsync(
        string uniqueFileName,
        CancellationToken cancellationToken = default);

    Task AddAsync(SubmittedAssignment submittedAssignment, CancellationToken cancellationToken = default);

    Task UpdateAsync(SubmittedAssignment submittedAssignment, CancellationToken cancellationToken = default);
}
