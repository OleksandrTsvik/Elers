using Application.Common.Queries;
using Application.Courses.GetCourseById;
using Application.Courses.GetCourseByIdToEdit;
using Application.Courses.GetCourseByTabId;
using Application.Courses.GetListCourses;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Queries;

internal class CourseQueries : ICourseQueries
{
    private readonly ApplicationDbContext _dbContext;

    public CourseQueries(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
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
                PhotoUrl = x.PhotoUrl,
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
                PhotoUrl = x.PhotoUrl,
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

    public Task<GetListCourseItemResponse[]> GetListCourses(CancellationToken cancellationToken = default)
    {
        return _dbContext.Courses
            .Select(x => new GetListCourseItemResponse
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                PhotoUrl = x.PhotoUrl
            })
            .OrderBy(x => x.Title)
            .ToArrayAsync(cancellationToken);
    }
}
