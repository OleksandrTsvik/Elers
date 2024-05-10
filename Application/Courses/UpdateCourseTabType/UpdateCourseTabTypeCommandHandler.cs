using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Courses.UpdateCourseTabType;

public class UpdateCourseTabTypeCommandHandler : ICommandHandler<UpdateCourseTabTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCourseTabTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateCourseTabTypeCommand request, CancellationToken cancellationToken)
    {
        Course? course = await _context.Courses
            .FirstOrDefaultAsync(x => x.Id == request.CourseId, cancellationToken);

        if (course is null)
        {
            return CourseErrors.NotFound(request.CourseId);
        }

        course.TabType = request.TabType;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
