using Application.Common.Services;
using Domain.Entities;
using Domain.Enums;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Persistence.Constants;

namespace Persistence.Services;

public class CourseService : ICourseService
{
    private readonly IMongoCollection<CourseMaterial> _courseMaterialsCollection;
    private readonly IMongoCollection<Grade> _gradesCollection;
    private readonly IMongoCollection<TestSession> _testSessionsCollection;
    private readonly IMongoCollection<TestQuestion> _testQuestionsCollection;
    private readonly IMongoCollection<SubmittedAssignment> _submittedAssignmentsCollection;
    private readonly IFileService _fileService;

    public CourseService(IMongoDatabase mongoDatabase, IFileService fileService)
    {
        _courseMaterialsCollection = mongoDatabase.GetCollection<CourseMaterial>(
            CollectionNames.CourseMaterials);

        _gradesCollection = mongoDatabase.GetCollection<Grade>(CollectionNames.Grades);

        _testSessionsCollection = mongoDatabase.GetCollection<TestSession>(CollectionNames.TestSessions);

        _testQuestionsCollection = mongoDatabase.GetCollection<TestQuestion>(CollectionNames.TestQuestions);

        _submittedAssignmentsCollection = mongoDatabase.GetCollection<SubmittedAssignment>(
            CollectionNames.SubmittedAssignments);

        _fileService = fileService;
    }

    public async Task RemoveMaterialsByCourseTabIdsAsync(
        IEnumerable<Guid> tabIds,
        bool deleteGrades = true,
        CancellationToken cancellationToken = default)
    {
        var materials = await _courseMaterialsCollection
            .Find(x => tabIds.Contains(x.CourseTabId))
            .Project(x => new { x.Id, x.Type })
            .ToListAsync(cancellationToken);

        IEnumerable<Guid> materialIds = materials.Select(x => x.Id);

        IEnumerable<Guid> assignmentIds = materials
            .Where(x => x.Type == CourseMaterialType.Assignment)
            .Select(x => x.Id);

        IEnumerable<Guid> testIds = materials
            .Where(x => x.Type == CourseMaterialType.Test)
            .Select(x => x.Id);

        await RemoveFilesAsync(tabIds, assignmentIds, cancellationToken);

        if (deleteGrades)
        {
            await _gradesCollection.OfType<GradeAssignment>()
                .DeleteManyAsync(x => assignmentIds.Contains(x.AssignmentId), cancellationToken);

            await _gradesCollection.OfType<GradeTest>()
                .DeleteManyAsync(x => testIds.Contains(x.TestId), cancellationToken);
        }

        await _testSessionsCollection.DeleteManyAsync(x => testIds.Contains(x.TestId), cancellationToken);

        await _testQuestionsCollection.DeleteManyAsync(x => testIds.Contains(x.TestId), cancellationToken);

        await _submittedAssignmentsCollection.DeleteManyAsync(
            x => assignmentIds.Contains(x.AssignmentId), cancellationToken);

        await _courseMaterialsCollection.DeleteManyAsync(x => materialIds.Contains(x.Id), cancellationToken);
    }

    private async Task RemoveFilesAsync(
        IEnumerable<Guid> tabIds,
        IEnumerable<Guid> assignmentIds,
        CancellationToken cancellationToken)
    {
        List<string> uniqueFileNames = await _courseMaterialsCollection
           .OfType<CourseMaterialFile>()
           .Find(x => tabIds.Contains(x.CourseTabId))
           .Project(x => x.UniqueFileName)
           .ToListAsync(cancellationToken);

        List<string> submittedAssignmentFiles = await _submittedAssignmentsCollection.AsQueryable()
            .Where(x => assignmentIds.Contains(x.AssignmentId))
            .SelectMany(x => x.Files.Select(file => file.UniqueFileName))
            .ToListAsync(cancellationToken);

        uniqueFileNames.AddRange(submittedAssignmentFiles);

        if (uniqueFileNames.Count != 0)
        {
            await _fileService.RemoveRangeAsync(uniqueFileNames, cancellationToken);
        }
    }
}
