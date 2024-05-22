using Application.Common.Queries;
using Application.Courses.GetCourseById;
using Domain.Entities;
using MongoDB.Driver;
using Persistence.Constants;

namespace Persistence.Queries;

public class CourseMaterialQueries : ICourseMaterialQueries
{
    private readonly IMongoCollection<CourseMaterial> _courseMaterialCollection;

    public CourseMaterialQueries(IMongoDatabase mongoDatabase)
    {
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
}
