using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Courses.GetCourseById;

public class GetCourseByIdQueryHandler : IQueryHandler<GetCourseByIdQuery, GetCourseByIdResponse>
{
    private readonly IApplicationDbContext _context;

    public GetCourseByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetCourseByIdResponse>> Handle(
        GetCourseByIdQuery request,
        CancellationToken cancellationToken)
    {
        GetCourseByIdResponse? course = await _context.Courses
            .Include(x => x.CourseTabs)
            .Select(x => new GetCourseByIdResponse
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                PhotoUrl = x.PhotoUrl,
                CourseTabs = x.CourseTabs.Select(courseTab => new CourseTabResponse
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
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (course is null)
        {
            return CourseErrors.NotFound(request.Id);
        }

        return course;
    }
}
