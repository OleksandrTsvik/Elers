using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Persistence.Constants;

namespace Persistence.Repositories;

internal class GradeRepository : MongoDbRepository<Grade>, IGradeRepository
{
    public GradeRepository(IMongoDatabase mongoDatabase)
        : base(mongoDatabase, CollectionNames.Grades)
    {
    }

    public Task<List<Grade>> GetByCourseIdAsync(
        Guid courseId,
        CancellationToken cancellationToken = default)
    {
        return Collection
            .Find(x => x.CourseId == courseId)
            .ToListAsync(cancellationToken);
    }

    public Task<List<Grade>> GetByCourseIdAndStudentIdAsync(
        Guid courseId,
        Guid studentId,
        CancellationToken cancellationToken = default)
    {
        return Collection
            .Find(x => x.CourseId == courseId && x.StudentId == studentId)
            .ToListAsync(cancellationToken);
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

    public async Task<GradeTest?> GetByTestIdAndStudentIdAsync(
        Guid testId,
        Guid studentId,
        CancellationToken cancellationToken = default)
    {
        return await Collection
            .OfType<GradeTest>()
            .Find(x => x.TestId == testId && x.StudentId == studentId)
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
            .Set(x => x.CreatedAt, grade.CreatedAt);

        switch (grade)
        {
            case GradeAssignment assignment:
                update = update
                    .Set(nameof(GradeAssignment.TeacherId), assignment.TeacherId)
                    .Set(nameof(GradeAssignment.AssignmentId), assignment.AssignmentId)
                    .Set(nameof(GradeAssignment.Value), assignment.Value);
                break;
            case GradeTest test:
                update = update
                    .Set(nameof(GradeTest.TestId), test.TestId)
                    .Set(nameof(GradeTest.GradingMethod), test.GradingMethod)
                    .Set(nameof(GradeTest.Values), test.Values);
                break;
            case GradeManual manual:
                update = update
                    .Set(nameof(GradeManual.TeacherId), manual.TeacherId)
                    .Set(nameof(GradeManual.Value), manual.Value);
                break;
        }

        await Collection.UpdateOneAsync(
            x => x.Id == grade.Id,
            update,
            null,
            cancellationToken);
    }

    public async Task UpdateTestGradingMethodAsync(
        Guid testId,
        GradingMethod gradingMethod,
        CancellationToken cancellationToken = default)
    {
        UpdateDefinition<GradeTest> update = Builders<GradeTest>.Update
            .Set(x => x.GradingMethod, gradingMethod);

        await Collection.OfType<GradeTest>().UpdateManyAsync(
            x => x.TestId == testId,
            update,
            null,
            cancellationToken);
    }

    public async Task RemoveRangeByCourseIdAsync(Guid courseId, CancellationToken cancellationToken = default)
    {
        await Collection.DeleteManyAsync(x => x.CourseId == courseId, cancellationToken);
    }

    public async Task RemoveRangeByAssignmentIdAsync(
        Guid assignmentId,
        CancellationToken cancellationToken = default)
    {
        await Collection.OfType<GradeAssignment>()
            .DeleteManyAsync(x => x.AssignmentId == assignmentId, cancellationToken);
    }

    public async Task RemoveRangeByTestIdAsync(Guid testId, CancellationToken cancellationToken = default)
    {
        await Collection.OfType<GradeTest>()
            .DeleteManyAsync(x => x.TestId == testId, cancellationToken);
    }

    public async Task RemoveRangeByColumnIdAsync(Guid columnId, CancellationToken cancellationToken = default)
    {
        await Collection.OfType<GradeManual>()
            .DeleteManyAsync(x => x.ManualGradesColumnId == columnId, cancellationToken);
    }

    public Task<bool> ExistsByStudentIdAndColumnIdAsync(
        Guid studentId,
        Guid columnId,
        CancellationToken cancellationToken = default)
    {
        return Collection.OfType<GradeManual>()
            .Find(x => x.StudentId == studentId && x.ManualGradesColumnId == columnId)
            .AnyAsync(cancellationToken);
    }
}
