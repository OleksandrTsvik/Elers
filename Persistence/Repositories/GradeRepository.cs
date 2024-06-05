using Domain.Entities;
using Domain.Repositories;
using MongoDB.Driver;
using Persistence.Constants;

namespace Persistence.Repositories;

internal class GradeRepository : MongoDbRepository<Grade>, IGradeRepository
{
    public GradeRepository(IMongoDatabase mongoDatabase)
        : base(mongoDatabase, CollectionNames.Grades)
    {
    }

    public async Task<double?> GetValueByAssignmentIdAndStudentIdAsync(
        Guid assignmentId,
        Guid studentId,
        CancellationToken cancellationToken = default)
    {
        return await Collection
            .OfType<GradeAssignment>()
            .Find(x => x.AssignmentId == assignmentId && x.StudentId == studentId)
            .Project(x => x.Value)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
