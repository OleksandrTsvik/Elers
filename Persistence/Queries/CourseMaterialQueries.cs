using Application.Common.Queries;
using Application.CourseMaterials.DownloadCourseMaterialFile;
using Application.CourseMaterials.DTOs;
using Application.Courses.DTOs;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Persistence.Constants;

namespace Persistence.Queries;

public class CourseMaterialQueries : ICourseMaterialQueries
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMongoCollection<CourseMaterial> _courseMaterialsCollection;

    public CourseMaterialQueries(ApplicationDbContext dbContext, IMongoDatabase mongoDatabase)
    {
        _dbContext = dbContext;

        _courseMaterialsCollection = mongoDatabase.GetCollection<CourseMaterial>(
            CollectionNames.CourseMaterials);
    }

    public Task<List<CourseMaterial>> GetListByTabId(
        Guid tabId,
        CancellationToken cancellationToken = default)
    {
        return _courseMaterialsCollection
            .Find(x => x.IsActive && x.CourseTabId == tabId)
            .SortBy(x => x.Order)
            .ToListAsync(cancellationToken);
    }

    public Task<List<CourseMaterial>> GetListByTabIdToEdit(
        Guid tabId,
        CancellationToken cancellationToken = default)
    {
        return _courseMaterialsCollection
            .Find(x => x.CourseTabId == tabId)
            .SortBy(x => x.Order)
            .ToListAsync(cancellationToken);
    }

    public Task<List<CourseMaterial>> GetListByTabIds(
        IEnumerable<Guid> tabIds,
        CancellationToken cancellationToken = default)
    {
        return _courseMaterialsCollection
            .Find(x => x.IsActive && tabIds.Contains(x.CourseTabId))
            .SortBy(x => x.Order)
            .ToListAsync(cancellationToken);
    }

    public Task<List<MaterialCountResponseDto>> GetListMaterialCountByCourseTabIds(
        IEnumerable<Guid> tabIds,
        CancellationToken cancellationToken = default)
    {
        return _courseMaterialsCollection
            .Aggregate()
            .Group(
                courseMaterial => courseMaterial.CourseTabId,
                x => new MaterialCountResponseDto
                {
                    TabId = x.Key,
                    MaterialCount = x.Count(courseMaterial =>
                        courseMaterial.IsActive && tabIds.Contains(courseMaterial.CourseTabId))
                })
            .ToListAsync(cancellationToken);
    }

    public Task<CourseMaterialTabResponseDto?> GetCourseMaterialTabResponseDto(
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

    public async Task<GetCourseMaterialFileInfoDto?> GetCourseMaterialFileInfo(
        string uniqueFileName,
        CancellationToken cancellationToken = default)
    {
        return await _courseMaterialsCollection
            .OfType<CourseMaterialFile>()
            .Find(x => x.UniqueFileName == uniqueFileName)
            .Project(x => new GetCourseMaterialFileInfoDto
            {
                FileName = x.FileName,
                UniqueFileName = x.UniqueFileName
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<CourseMaterial>> GetListByTabIdsToEdit(
        IEnumerable<Guid> tabIds,
        CancellationToken cancellationToken = default)
    {
        return _courseMaterialsCollection
            .Find(x => tabIds.Contains(x.CourseTabId))
            .SortBy(x => x.Order)
            .ToListAsync(cancellationToken);
    }

    public Task<List<MaterialCountResponseDto>> GetListMaterialCountByCourseTabIdsToEdit(
        IEnumerable<Guid> tabIds,
        CancellationToken cancellationToken = default)
    {
        return _courseMaterialsCollection
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

    public Task<Guid?> GetCourseTabId(Guid materialId, CancellationToken cancellationToken = default)
    {
        return _courseMaterialsCollection
            .Find(x => x.Id == materialId)
            .Project(x => (Guid?)x.CourseTabId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<Guid>> GetCourseMaterialIdsByTabId(
        Guid courseTabId,
        CancellationToken cancellationToken = default)
    {
        return _courseMaterialsCollection
            .Find(x => x.CourseTabId == courseTabId)
            .Project(x => x.Id)
            .ToListAsync(cancellationToken);
    }
}
