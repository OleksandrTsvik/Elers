using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Courses.GetListCourses;

public class GetListCoursesQueryHandler : IQueryHandler<GetListCoursesQuery, GetListCourseItemResponse[]>
{
    private readonly IApplicationDbContext _context;

    public GetListCoursesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetListCourseItemResponse[]>> Handle(
        GetListCoursesQuery request,
        CancellationToken cancellationToken)
    {
        GetListCourseItemResponse[] courses = await _context.Courses
            .Select(x => new GetListCourseItemResponse
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                PhotoUrl = x.PhotoUrl
            })
            .OrderBy(x => x.Title)
            .ToArrayAsync(cancellationToken);

        return courses;
    }
}
