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

    public async Task<GradeAssignment?> GetByAssignmentIdAndStudentIdAsync(
        Guid assignmentId,
        Guid studentId,
        CancellationToken cancellationToken = default)
    {
        return await Collection
            .OfType<GradeAssignment>()
            .Find(x => x.AssignmentId == assignmentId && x.StudentId == studentId)
            .FirstOrDefaultAsync(cancellationToken);
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

    public async Task UpdateAsync(Grade grade, CancellationToken cancellationToken = default)
    {
        UpdateDefinition<Grade> update = Builders<Grade>.Update
            .Set(x => x.CourseId, grade.CourseId)
            .Set(x => x.StudentId, grade.StudentId)
            .Set(x => x.Value, grade.Value)
            .Set(x => x.CreatedAt, grade.CreatedAt);

        switch (grade)
        {
            case GradeAssignment assignment:
                update = update
                    .Set(nameof(GradeAssignment.TeacherId), assignment.TeacherId)
                    .Set(nameof(GradeAssignment.AssignmentId), assignment.AssignmentId);
                break;
            case GradeTest test:
                update = update.Set(nameof(GradeTest.TestId), test.TestId);
                break;
        }

        await Collection.UpdateOneAsync(
            x => x.Id == grade.Id,
            update,
            null,
            cancellationToken);
    }
}
