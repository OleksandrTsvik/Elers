using Application.Common.Messaging;
using Application.Common.Queries;
using Application.Courses.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Domain.Shared;

namespace Application.Courses.GetCourseByIdToEdit;

public class GetCourseByIdToEditQueryHandler
    : IQueryHandler<GetCourseByIdToEditQuery, GetCourseByIdToEditResponse<CourseTabToEditResponse>>
{
    private readonly ICourseQueries _courseQueries;
    private readonly ICourseMaterialQueries _courseMaterialQueries;

    public GetCourseByIdToEditQueryHandler(
        ICourseQueries courseQueries,
        ICourseMaterialQueries courseMaterialQueries)
    {
        _courseQueries = courseQueries;
        _courseMaterialQueries = courseMaterialQueries;
    }

    public async Task<Result<GetCourseByIdToEditResponse<CourseTabToEditResponse>>> Handle(
        GetCourseByIdToEditQuery request,
        CancellationToken cancellationToken)
    {
        GetCourseByIdToEditResponseDto? courseDto = await _courseQueries
            .GetCourseByIdToEdit(request.Id, cancellationToken);

        if (courseDto is null)
        {
            return CourseErrors.NotFound(request.Id);
        }

        var course = new GetCourseByIdToEditResponse<CourseTabToEditResponse>
        {
            Id = courseDto.Id,
            Title = courseDto.Title,
            Description = courseDto.Description,
            PhotoUrl = courseDto.PhotoUrl,
            TabType = courseDto.TabType,
            CourseTabs = courseDto.CourseTabs
                .Select(courseTab => new CourseTabToEditResponse
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
            .GetListMaterialCountByCourseTabIdsToEdit(
                course.CourseTabs.Where(x => x.ShowMaterialsCount).Select(x => x.Id).ToArray(),
                cancellationToken);

        foreach (CourseTabToEditResponse tab in course.CourseTabs)
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
        GetCourseByIdToEditResponse<CourseTabToEditResponse> course,
        CancellationToken cancellationToken)
    {
        Guid[] tabIds = course.CourseTabs.Select(x => x.Id).ToArray();

        List<CourseMaterial> courseMaterials = await _courseMaterialQueries
            .GetListByTabIdsToEdit(tabIds, cancellationToken);

        foreach (CourseTabToEditResponse tab in course.CourseTabs)
        {
            tab.CourseMaterials = courseMaterials
                .Where(x => x.CourseTabId == tab.Id)
                .ToArray();
        }
    }
}
