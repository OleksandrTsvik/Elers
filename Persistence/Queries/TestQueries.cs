using Application.Common.Queries;
using Application.Grades.GetCourseMyGrades;
using Application.Tests.DTOs;
using Domain.Entities;
using MongoDB.Driver;
using Persistence.Constants;

namespace Persistence.Queries;

public class TestQueries : ITestQueries
{
    private readonly IMongoCollection<TestSession> _testSessionsCollection;
    private readonly IMongoCollection<CourseMaterial> _courseMaterialsCollection;

    public TestQueries(IMongoDatabase mongoDatabase)
    {
        _testSessionsCollection = mongoDatabase.GetCollection<TestSession>(
            CollectionNames.TestSessions);

        _courseMaterialsCollection = mongoDatabase.GetCollection<CourseMaterial>(
            CollectionNames.CourseMaterials);
    }

    public Task<List<CourseTestMyGrade>> GetCourseTestMyGradesByIds(
        IEnumerable<Guid> testIds,
        CancellationToken cancellationToken = default)
    {
        return _courseMaterialsCollection.OfType<CourseMaterialTest>()
            .Find(x => testIds.Contains(x.Id))
            .Project(x => new CourseTestMyGrade
            {
                TestId = x.Id,
                TimeLimitInMinutes = x.TimeLimitInMinutes
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<TestSessionDto?> GetTestSessionDtoById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _testSessionsCollection
            .Find(x => x.Id == id)
            .Project(x => new TestSessionDto
            {
                Id = x.Id,
                TestId = x.TestId,
                UserId = x.UserId,
                StartedAt = x.StartedAt,
                FinishedAt = x.FinishedAt,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TestSessionDto?> GetTestSessionDtoByIdAndUserId(
        Guid id,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _testSessionsCollection
            .Find(x => x.Id == id && x.UserId == userId)
            .Project(x => new TestSessionDto
            {
                Id = x.Id,
                TestId = x.TestId,
                UserId = x.UserId,
                StartedAt = x.StartedAt,
                FinishedAt = x.FinishedAt,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<TestSessionDto>> GetTestSessionDtosByIds(
        IEnumerable<Guid> testSessionIds,
        CancellationToken cancellationToken = default)
    {
        return _testSessionsCollection
            .Find(x => testSessionIds.Contains(x.Id))
            .Project(x => new TestSessionDto
            {
                Id = x.Id,
                TestId = x.TestId,
                UserId = x.UserId,
                StartedAt = x.StartedAt,
                FinishedAt = x.FinishedAt,
            })
            .ToListAsync(cancellationToken);
    }

    public Task<List<TestSessionDto>> GetTestSessionDtosByTestIdAndUserId(
        Guid testId,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return _testSessionsCollection
            .Find(x => x.TestId == testId && x.UserId == userId)
            .Project(x => new TestSessionDto
            {
                Id = x.Id,
                TestId = x.TestId,
                UserId = x.UserId,
                StartedAt = x.StartedAt,
                FinishedAt = x.FinishedAt,
            })
            .ToListAsync(cancellationToken);
    }
}
