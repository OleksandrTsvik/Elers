using Domain.Entities;
using Domain.Repositories;
using MongoDB.Driver;
using Persistence.Constants;

namespace Persistence.Repositories;

internal class SubmittedAssignmentRepository
    : MongoDbRepository<SubmittedAssignment>, ISubmittedAssignmentRepository
{
    public SubmittedAssignmentRepository(IMongoDatabase mongoDatabase)
        : base(mongoDatabase, CollectionNames.SubmittedAssignments)
    {
    }

    public async Task<SubmittedAssignment?> GetByAssignmentIdAndStudentIdAsync(
        Guid assignmentId,
        Guid studentId,
        CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(x => x.AssignmentId == assignmentId && x.StudentId == studentId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<SubmittedAssignment?> GetByUniqueFileNameAsync(
        string uniqueFileName,
        CancellationToken cancellationToken = default)
    {
        return await Collection
            .Find(x => x.Files.Any(file => file.UniqueFileName == uniqueFileName))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        SubmittedAssignment submittedAssignment,
        CancellationToken cancellationToken = default)
    {
        UpdateDefinition<SubmittedAssignment> update = Builders<SubmittedAssignment>.Update
            .Set(x => x.TeacherId, submittedAssignment.TeacherId)
            .Set(x => x.Status, submittedAssignment.Status)
            .Set(x => x.AttemptNumber, submittedAssignment.AttemptNumber)
            .Set(x => x.Text, submittedAssignment.Text)
            .Set(x => x.Files, submittedAssignment.Files)
            .Set(x => x.TeacherComment, submittedAssignment.TeacherComment)
            .Set(x => x.SubmittedAt, submittedAssignment.SubmittedAt);

        await Collection.UpdateOneAsync(
            x => x.Id == submittedAssignment.Id,
            update,
            null,
            cancellationToken);
    }
}
