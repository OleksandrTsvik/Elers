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
            .Select(x => new GetCourseByIdResponse
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                PhotoUrl = x.PhotoUrl
            })
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (course is null)
        {
            return CourseErrors.NotFound(request.Id);
        }

        return course;
    }
}
