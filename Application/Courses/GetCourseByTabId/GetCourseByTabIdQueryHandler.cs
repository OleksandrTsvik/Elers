using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Courses.GetCourseByTabId;

public class GetCourseByTabIdQueryHandler : IQueryHandler<GetCourseByTabIdQuery, GetCourseByTabIdResponse>
{
    private readonly IApplicationDbContext _context;

    public GetCourseByTabIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetCourseByTabIdResponse>> Handle(
        GetCourseByTabIdQuery request,
        CancellationToken cancellationToken)
    {
        GetCourseByTabIdResponse? courseByTabId = await _context.CourseTabs
            .Include(x => x.Course)
            .Select(x => new GetCourseByTabIdResponse
            {
                TabId = x.Id,
                CourseId = x.Course == null ? null : x.Course.Id,
                Title = x.Course == null ? null : x.Course.Title
            })
            .FirstOrDefaultAsync(x => x.TabId == request.TabId, cancellationToken);

        if (courseByTabId is null)
        {
            return CourseTabErrors.NotFound(request.TabId);
        }

        return courseByTabId;
    }
}
