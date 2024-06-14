using Application.Common.Queries;
using Application.Grades.DTOs;
using Domain.Entities;
using Domain.Enums;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Persistence.Constants;
using Persistence.Extensions;

namespace Persistence.Queries;

public class GradeQueries : IGradeQueries
{
    private readonly ICourseQueries _courseQueries;
    private readonly IMongoCollection<CourseMaterial> _courseMaterialsCollection;
    private readonly IMongoCollection<ManualGradesColumn> _manualGradesColumnsCollection;

    public GradeQueries(ICourseQueries courseQueries, IMongoDatabase mongoDatabase)
    {
        _courseQueries = courseQueries;

        _courseMaterialsCollection = mongoDatabase.GetCollection<CourseMaterial>(
            CollectionNames.CourseMaterials);

        _manualGradesColumnsCollection = mongoDatabase.GetCollection<ManualGradesColumn>(
            CollectionNames.ManualGradesColumns);
    }

    public async Task<List<AssessmentItem>> GetAssessments(
        Guid courseId,
        bool onlyActive = true,
        CancellationToken cancellationToken = default)
    {
        Guid[] tabIds = await _courseQueries.GetCourseTabIds(courseId, cancellationToken);

        if (tabIds.Length == 0)
        {
            return [];
        }

        List<AssessmentAssignmentItem> assignmentAssessments = await _courseMaterialsCollection
            .OfType<CourseMaterialAssignment>()
            .AsQueryable()
            .Where(x => tabIds.Contains(x.CourseTabId))
            .WhereIf(onlyActive, x => x.IsActive)
            .OrderBy(x => x.CreatedAt)
            .Select(x => new AssessmentAssignmentItem
            {
                Id = x.Id,
                Title = x.Title,
                Type = GradeType.Assignment,
                MaxGrade = x.MaxGrade
            })
            .ToListAsync(cancellationToken);

        var assessments = new List<AssessmentItem>(assignmentAssessments);

        List<AssessmentItem> testAssessments = await _courseMaterialsCollection
            .OfType<CourseMaterialTest>()
            .AsQueryable()
            .Where(x => tabIds.Contains(x.CourseTabId))
            .WhereIf(onlyActive, x => x.IsActive)
            .OrderBy(x => x.CreatedAt)
            .Select(x => new AssessmentItem
            {
                Id = x.Id,
                Title = x.Title,
                Type = GradeType.Test
            })
            .ToListAsync(cancellationToken);

        List<AssessmentManualItem> columnAssessments = await _manualGradesColumnsCollection
            .Find(x => x.CourseId == courseId)
            .SortBy(x => x.Date)
            .Project(x => new AssessmentManualItem
            {
                Id = x.Id,
                Title = x.Title,
                Type = GradeType.Manual,
                Date = x.Date,
                MaxGrade = x.MaxGrade
            })
            .ToListAsync(cancellationToken);

        assessments.AddRange(testAssessments);
        assessments.AddRange(columnAssessments);

        return assessments;
    }
}
