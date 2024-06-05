using Application.Assignments.GetListAssignmentTitles;
using Application.Assignments.GetListSubmittedAssignments;
using Application.Common.Models;
using Application.Common.Queries;
using Domain.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Persistence.Constants;
using Persistence.Extensions;

namespace Persistence.Queries;

public class AssignmentQueries : IAssignmentQueries
{
    private readonly IMongoCollection<SubmittedAssignment> _submittedAssignmentsCollection;
    private readonly IMongoCollection<CourseMaterial> _courseMaterialsCollection;

    public AssignmentQueries(IMongoDatabase mongoDatabase)
    {
        _submittedAssignmentsCollection = mongoDatabase.GetCollection<SubmittedAssignment>(
            CollectionNames.SubmittedAssignments);

        _courseMaterialsCollection = mongoDatabase.GetCollection<CourseMaterial>(
            CollectionNames.CourseMaterials);
    }

    public Task<PagedList<SubmittedAssignmentListItemDto>> GetListSubmittedAssignments(
        IEnumerable<Guid> tabIds,
        GetListSubmittedAssignmentsQueryParams queryParams,
        CancellationToken cancellationToken = default)
    {
        return _submittedAssignmentsCollection.AsQueryable()
            .Join(
                _courseMaterialsCollection.OfType<CourseMaterialAssignment>().AsQueryable(),
                submitted => submitted.AssignmentId,
                material => material.Id,
                (submitted, material) => new
                {
                    CourseTabId = material.CourseTabId,
                    SubmittedAssignmentId = submitted.Id,
                    Status = submitted.Status,
                    StudentId = submitted.StudentId,
                    AssignmentId = material.Id,
                    AssignmentTitle = material.Title,
                    SubmittedDate = submitted.SubmittedAt,
                })
            .Where(x => tabIds.Contains(x.CourseTabId))
            .OrderBy(x => x.SubmittedDate)
            .WhereIf(queryParams.Status is not null, x => queryParams.Status == x.Status)
            .WhereIf(queryParams.AssignmentId is not null, x => queryParams.AssignmentId == x.AssignmentId)
            .WhereIf(queryParams.StudentId is not null, x => queryParams.StudentId == x.StudentId)
            .Select(x => new SubmittedAssignmentListItemDto
            {
                SubmittedAssignmentId = x.SubmittedAssignmentId,
                StudentId = x.StudentId,
                AssignmentId = x.AssignmentId,
                AssignmentTitle = x.AssignmentTitle,
                SubmittedDate = x.SubmittedDate,
            })
            .ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize, cancellationToken);
    }

    public Task<List<GetListAssignmentTitleItemResponse>> GetListAssignmentTitles(
        IEnumerable<Guid> tabIds,
        CancellationToken cancellationToken = default)
    {
        return _courseMaterialsCollection
            .OfType<CourseMaterialAssignment>()
            .Find(x => x.IsActive && tabIds.Contains(x.CourseTabId))
            .Project(x => new GetListAssignmentTitleItemResponse
            {
                AssignmentId = x.Id,
                Title = x.Title
            })
            .ToListAsync(cancellationToken);
    }
}
