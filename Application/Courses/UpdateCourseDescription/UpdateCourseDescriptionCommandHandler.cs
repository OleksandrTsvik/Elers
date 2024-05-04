using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Courses.UpdateCourseDescription;

public class UpdateCourseDescriptionCommandHandler : ICommandHandler<UpdateCourseDescriptionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCourseDescriptionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        UpdateCourseDescriptionCommand request,
        CancellationToken cancellationToken)
    {
        Course? course = await _context.Courses
            .FirstOrDefaultAsync(x => x.Id == request.CourseId, cancellationToken);

        if (course is null)
        {
            return CourseErrors.NotFound(request.CourseId);
        }

        course.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
