using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Entities;
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

        List<CourseMaterial> courseMaterials = await _courseMaterialQueries
            .GetListCourseMaterialsAsync(cancellationToken);

        foreach (CourseTabResponse tab in course.CourseTabs)
        {
            tab.CourseMaterials = courseMaterials
                .Where(x => x.CourseTabId == tab.Id)
                .ToArray();
        }

        return course;
    }
}
