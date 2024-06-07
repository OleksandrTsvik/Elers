using Application.Common.Queries;
using Application.Grades.DTOs;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Persistence.Constants;

namespace Persistence.Queries;

public class GradeQueries : IGradeQueries
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMongoCollection<CourseMaterial> _courseMaterialsCollection;

    public GradeQueries(ApplicationDbContext dbContext, IMongoDatabase mongoDatabase)
    {
        _dbContext = dbContext;

        _courseMaterialsCollection = mongoDatabase.GetCollection<CourseMaterial>(
            CollectionNames.CourseMaterials);
    }

    public async Task<List<AssessmentItem>> GetAssessments(
        Guid courseId,
        CancellationToken cancellationToken = default)
    {
        Guid[] tabIds = await _dbContext.CourseTabs
            .Where(x => x.CourseId == courseId)
            .Select(x => x.Id)
            .ToArrayAsync(cancellationToken);

        if (tabIds.Length == 0)
        {
            return [];
        }

        List<AssessmentItem> assignmentAssessments = await _courseMaterialsCollection
            .OfType<CourseMaterialAssignment>()
            .Find(x => tabIds.Contains(x.CourseTabId))
            .SortBy(x => x.CreatedAt)
            .Project(x => new AssessmentItem
            {
                Id = x.Id,
                Title = x.Title,
                Type = GradeType.Assignment
            })
            .ToListAsync(cancellationToken);

        var assessments = new List<AssessmentItem>(assignmentAssessments);

        return assessments;
    }
}
