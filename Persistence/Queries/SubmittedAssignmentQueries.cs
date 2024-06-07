using Application.Common.Queries;
using Domain.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Persistence.Constants;

namespace Persistence.Queries;

public class SubmittedAssignmentQueries : ISubmittedAssignmentQueries
{
    private readonly IMongoCollection<SubmittedAssignment> _submittedAssignmentsCollection;
    private readonly IMongoCollection<CourseMaterial> _courseMaterialsCollection;

    public SubmittedAssignmentQueries(IMongoDatabase mongoDatabase)
    {
        _submittedAssignmentsCollection = mongoDatabase.GetCollection<SubmittedAssignment>(
            CollectionNames.SubmittedAssignments);

        _courseMaterialsCollection = mongoDatabase.GetCollection<CourseMaterial>(
            CollectionNames.CourseMaterials);
    }

    public Task<List<string>> GetSubmittedFilesByCourseTabIdsAsync(
        IEnumerable<Guid> tabIds,
        CancellationToken cancellationToken = default)
    {
        return _courseMaterialsCollection.OfType<CourseMaterialAssignment>().AsQueryable()
            .Join(
                _submittedAssignmentsCollection.AsQueryable(),
                material => material.Id,
                submitted => submitted.AssignmentId,
                (material, submitted) => new
                {
                    CourseTabId = material.CourseTabId,
                    UniqueFileNames = submitted.Files.Select(x => x.UniqueFileName)
                })
            .Where(x => tabIds.Contains(x.CourseTabId))
            .SelectMany(x => x.UniqueFileNames)
            .ToListAsync(cancellationToken);
    }
}
