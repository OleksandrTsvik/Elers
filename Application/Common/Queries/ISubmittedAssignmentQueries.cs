namespace Application.Common.Queries;

public interface ISubmittedAssignmentQueries
{
    public Task<List<string>> GetSubmittedFilesByCourseTabIdsAsync(
        IEnumerable<Guid> tabIds,
        CancellationToken cancellationToken = default);
}
