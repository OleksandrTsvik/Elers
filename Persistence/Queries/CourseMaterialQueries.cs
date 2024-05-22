using Application.Common.Queries;
using Application.CourseMaterials.DTOs;
using Application.Courses.GetCourseById;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Persistence.Constants;

namespace Persistence.Queries;

public class CourseMaterialQueries : ICourseMaterialQueries
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMongoCollection<CourseMaterial> _courseMaterialCollection;

    public CourseMaterialQueries(ApplicationDbContext dbContext, IMongoDatabase mongoDatabase)
    {
        _dbContext = dbContext;

        _courseMaterialCollection = mongoDatabase.GetCollection<CourseMaterial>(
            CollectionNames.CourseMaterials);
    }

    public Task<List<CourseMaterial>> GetListCourseMaterials(
        CancellationToken cancellationToken = default)
    {
        return _courseMaterialCollection
            .Find(_ => true)
            .ToListAsync(cancellationToken);
    }

    public Task<List<CourseMaterial>> GetListByTabId(
        Guid tabId,
        CancellationToken cancellationToken = default)
    {
        return _courseMaterialCollection
            .Find(x => x.CourseTabId == tabId)
            .ToListAsync(cancellationToken);
    }

    public Task<List<CourseMaterial>> GetListByTabIds(
        IEnumerable<Guid> tabIds,
        CancellationToken cancellationToken = default)
    {
        return _courseMaterialCollection
            .Find(x => tabIds.Contains(x.CourseTabId))
            .ToListAsync(cancellationToken);
    }

    public Task<List<MaterialCountResponseDto>> GetListMaterialCountByCourseTabIdsAsync(
        IEnumerable<Guid> tabIds,
        CancellationToken cancellationToken = default)
    {
        return _courseMaterialCollection
            .Aggregate()
            .Group(
                courseMaterial => courseMaterial.CourseTabId,
                x => new MaterialCountResponseDto
                {
                    TabId = x.Key,
                    MaterialCount = x.Count(courseMaterial => tabIds.Contains(courseMaterial.CourseTabId))
                })
            .ToListAsync(cancellationToken);
    }

    public Task<CourseMaterialTabResponseDto?> GetCourseMaterialTabResponseDtoAsync(
        Guid tabId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.CourseTabs
            .Where(x => x.Id == tabId)
            .Select(x => new CourseMaterialTabResponseDto
            {
                TabId = x.Id,
                CourseId = x.CourseId,
                CourseTabType = x.Course != null ? x.Course.TabType : CourseTabType.Tabs,
                CourseTitle = x.Course != null ? x.Course.Title : ""
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
