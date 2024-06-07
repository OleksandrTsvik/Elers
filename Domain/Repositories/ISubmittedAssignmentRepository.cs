using Domain.Entities;

namespace Domain.Repositories;

public interface ISubmittedAssignmentRepository
{
    Task<SubmittedAssignment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<SubmittedAssignment?> GetByAssignmentIdAndStudentIdAsync(
        Guid assignmentId,
        Guid studentId,
        CancellationToken cancellationToken = default);

    Task<SubmittedAssignment?> GetByUniqueFileNameAsync(
        string uniqueFileName,
        CancellationToken cancellationToken = default);

    Task<List<string>> GetSubmittedFilesByAssignmentIdAsync(
        Guid assignmentId,
        CancellationToken cancellationToken = default);

    Task AddAsync(SubmittedAssignment submittedAssignment, CancellationToken cancellationToken = default);

    Task UpdateAsync(SubmittedAssignment submittedAssignment, CancellationToken cancellationToken = default);

    Task RemoveRangeByAssignmentIdAsync(Guid assignmentId, CancellationToken cancellationToken = default);

    Task RemoveRangeByCourseTabIdsAsync(
        IEnumerable<Guid> tabIds,
        CancellationToken cancellationToken = default);
}
