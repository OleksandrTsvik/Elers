using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Domain.Shared;

namespace Application.Courses.GetCourseById;

public class GetCourseByIdQueryHandler
    : IQueryHandler<GetCourseByIdQuery, GetCourseByIdResponse<CourseTabResponse>>
{
    private readonly ICourseQueries _courseQueries;
    private readonly ICourseMaterialQueries _courseMaterialQueries;

    public GetCourseByIdQueryHandler(
        ICourseQueries courseQueries,
        ICourseMaterialQueries courseMaterialQueries)
    {
        _courseQueries = courseQueries;
        _courseMaterialQueries = courseMaterialQueries;
    }

    public async Task<Result<GetCourseByIdResponse<CourseTabResponse>>> Handle(
        GetCourseByIdQuery request,
        CancellationToken cancellationToken)
    {
        GetCourseByIdResponseDto? courseDto = await _courseQueries
            .GetCourseById(request.Id, cancellationToken);

        if (courseDto is null)
        {
            return CourseErrors.NotFound(request.Id);
        }

        var course = new GetCourseByIdResponse<CourseTabResponse>
        {
            Id = courseDto.Id,
            Title = courseDto.Title,
            Description = courseDto.Description,
            PhotoUrl = courseDto.PhotoUrl,
            TabType = courseDto.TabType,
            CourseTabs = courseDto.CourseTabs
                .Select(courseTab => new CourseTabResponse
                {
                    Id = courseTab.Id,
                    CourseId = courseTab.CourseId,
                    Name = courseTab.Name,
                    IsActive = courseTab.IsActive,
                    Order = courseTab.Order,
                    Color = courseTab.Color,
                    ShowMaterialsCount = courseTab.ShowMaterialsCount,
                })
                .ToArray()
        };

        List<MaterialCountResponseDto> materialCounts = await _courseMaterialQueries
            .GetListMaterialCountByCourseTabIds(
                course.CourseTabs.Select(x => x.Id).ToArray(),
                cancellationToken);

        foreach (CourseTabResponse tab in course.CourseTabs)
        {
            tab.MaterialCount = materialCounts
                .Where(x => x.TabId == tab.Id)
                .Select(x => x.MaterialCount)
                .FirstOrDefault();
        }

        switch (course.TabType)
        {
            case CourseTabType.Sections:
                await SetCourseMaterialsForSectionsType(course, cancellationToken);
                break;
        }

        return course;
    }

    private async Task SetCourseMaterialsForSectionsType(
        GetCourseByIdResponse<CourseTabResponse> course,
        CancellationToken cancellationToken)
    {
        Guid[] tabIds = course.CourseTabs.Select(x => x.Id).ToArray();

        List<CourseMaterial> courseMaterials = await _courseMaterialQueries
            .GetListByTabIds(tabIds, cancellationToken);

        foreach (CourseTabResponse tab in course.CourseTabs)
        {
            tab.CourseMaterials = courseMaterials
                .Where(x => x.CourseTabId == tab.Id)
                .ToArray();
        }
    }
}
