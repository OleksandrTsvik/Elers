using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.CourseTabs.CreateCourseTab;

public class CreateCourseTabCommandHandler : ICommandHandler<CreateCourseTabCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateCourseTabCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateCourseTabCommand request, CancellationToken cancellationToken)
    {
        bool courseById = await _context.Courses
            .AnyAsync(x => x.Id == request.CourseId, cancellationToken);

        if (!courseById)
        {
            return CourseErrors.NotFound(request.CourseId);
        }

        var courseTab = new CourseTab
        {
            CourseId = request.CourseId,
            Name = request.Name,
        };

        _context.CourseTabs.Add(courseTab);

        await _context.SaveChangesAsync(cancellationToken);

        return courseTab.Id;
    }
}
