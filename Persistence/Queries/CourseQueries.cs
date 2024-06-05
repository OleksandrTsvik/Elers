using Application.Common.Models;
using Application.Common.Queries;
using Application.Courses.GetCourseById;
using Application.Courses.GetCourseByIdToEdit;
using Application.Courses.GetCourseByTabId;
using Application.Courses.GetListCourses;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Persistence.Constants;
using Persistence.Extensions;

namespace Persistence.Queries;

internal class CourseQueries : ICourseQueries
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMongoCollection<CourseMaterial> _courseMaterialsCollection;

    public CourseQueries(ApplicationDbContext dbContext, IMongoDatabase mongoDatabase)
    {
        _dbContext = dbContext;

        _courseMaterialsCollection = mongoDatabase.GetCollection<CourseMaterial>(
            CollectionNames.CourseMaterials);
    }

    public Task<GetCourseByIdResponseDto?> GetCourseById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Courses
            .Include(x => x.CourseTabs)
            .Where(x => x.Id == id)
            .Select(x => new GetCourseByIdResponseDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                TabType = x.TabType,
                CourseTabs = x.CourseTabs
                    .Where(courseTab => courseTab.IsActive)
                    .Select(courseTab => new CourseTabResponseDto
                    {
                        Id = courseTab.Id,
                        CourseId = courseTab.CourseId,
                        Name = courseTab.Name,
                        Order = courseTab.Order,
                        Color = courseTab.Color,
                        ShowMaterialsCount = courseTab.ShowMaterialsCount,
                    })
                    .OrderBy(courseTab => courseTab.Order)
                        .ThenBy(courseTab => courseTab.Name)
                    .ToArray()
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<GetCourseByIdToEditResponseDto?> GetCourseByIdToEdit(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Courses
            .Include(x => x.CourseTabs)
            .Where(x => x.Id == id)
            .Select(x => new GetCourseByIdToEditResponseDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                TabType = x.TabType,
                CourseTabs = x.CourseTabs
                    .Select(courseTab => new CourseTabToEditResponseDto
                    {
                        Id = courseTab.Id,
                        CourseId = courseTab.CourseId,
                        Name = courseTab.Name,
                        IsActive = courseTab.IsActive,
                        Order = courseTab.Order,
                        Color = courseTab.Color,
                        ShowMaterialsCount = courseTab.ShowMaterialsCount,
                    })
                    .OrderBy(courseTab => courseTab.Order)
                        .ThenBy(courseTab => courseTab.Name)
                    .ToArray()
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<GetCourseByTabIdResponse?> GetCourseByTabId(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.CourseTabs
            .Include(x => x.Course)
            .Select(x => new GetCourseByTabIdResponse
            {
                TabId = x.Id,
                CourseId = x.Course != null ? x.Course.Id : default,
                Title = x.Course != null ? x.Course.Title : "",
                CourseTabType = x.Course != null ? x.Course.TabType : CourseTabType.Tabs
            })
            .FirstOrDefaultAsync(x => x.TabId == id, cancellationToken);
    }

    public async Task<PagedList<GetListCourseItemResponse>> GetListCourses(
        GetListCoursesQueryParams queryParams,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Course> coursesQuery = _dbContext.Courses
            .OrderBy(x => x.Title)
            .WhereIf(
                !string.IsNullOrWhiteSpace(queryParams.Search),
                x => EF.Functions.ILike(x.Title, $"%{queryParams.Search}%"));

        PagedList<CourseListItemDto> courses = await coursesQuery
            .Select(x => new CourseListItemDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                CountMembers = x.CourseMembers.Count,
                TabIds = x.CourseTabs.Where(x => x.IsActive).Select(x => x.Id)
            })
            .ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize, cancellationToken);

        Guid[] tabIds = courses.Items.SelectMany(x => x.TabIds).ToArray();

        List<CountMaterialsDto> countMaterialsByTabs = await _courseMaterialsCollection
            .Aggregate()
            .Group(
                courseMaterial => courseMaterial.CourseTabId,
                x => new CountMaterialsDto
                {
                    TabId = x.Key,
                    CountMaterials = x.Count(courseMaterial =>
                        courseMaterial.IsActive && tabIds.Contains(courseMaterial.CourseTabId) &&
                        courseMaterial.Type != CourseMaterialType.Assignment &&
                        courseMaterial.Type != CourseMaterialType.Test),
                    CountAssignments = x.Count(courseMaterial =>
                        courseMaterial.IsActive && tabIds.Contains(courseMaterial.CourseTabId) &&
                        courseMaterial.Type == CourseMaterialType.Assignment),
                    CountTests = x.Count(courseMaterial =>
                        courseMaterial.IsActive && tabIds.Contains(courseMaterial.CourseTabId) &&
                        courseMaterial.Type == CourseMaterialType.Test)
                })
            .ToListAsync(cancellationToken);

        var coursesResponse = courses.Items
            .Select(course => new GetListCourseItemResponse
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                ImageUrl = course.ImageUrl,
                CountMembers = course.CountMembers,
                CountMaterials = countMaterialsByTabs
                    .Where(x => course.TabIds.Contains(x.TabId))
                    .Sum(x => x.CountMaterials),
                CountAssignments = countMaterialsByTabs
                    .Where(x => course.TabIds.Contains(x.TabId))
                    .Sum(x => x.CountAssignments),
                CountTests = countMaterialsByTabs
                    .Where(x => course.TabIds.Contains(x.TabId))
                    .Sum(x => x.CountTests)
            })
            .ToList();

        return new PagedList<GetListCourseItemResponse>(
            coursesResponse,
            courses.TotalCount,
            courses.CurrentPage,
            courses.PageSize);
    }

    public Task<Guid[]> GetCourseTabIds(Guid courseId, CancellationToken cancellationToken = default)
    {
        return _dbContext.CourseTabs
            .Where(x => x.CourseId == courseId)
            .Select(x => x.Id)
            .ToArrayAsync(cancellationToken);
    }
}
