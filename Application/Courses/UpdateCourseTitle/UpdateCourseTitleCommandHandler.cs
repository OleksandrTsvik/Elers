using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Courses.UpdateCourseTitle;

public class UpdateCourseTitleCommandHandler : ICommandHandler<UpdateCourseTitleCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCourseTitleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateCourseTitleCommand request, CancellationToken cancellationToken)
    {
        Course? course = await _context.Courses
            .FirstOrDefaultAsync(x => x.Id == request.CourseId, cancellationToken);

        if (course is null)
        {
            return CourseErrors.NotFound(request.CourseId);
        }

        course.Title = request.Title;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
