using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.CourseTabs.DeleteCourseTab;

public class DeleteCourseTabCommandHandler : ICommandHandler<DeleteCourseTabCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCourseTabCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteCourseTabCommand request, CancellationToken cancellationToken)
    {
        CourseTab? courseTab = await _context.CourseTabs
            .FirstOrDefaultAsync(x => x.Id == request.CourseTabId, cancellationToken);

        if (courseTab is null)
        {
            return CourseTabErrors.NotFound(request.CourseTabId);
        }

        _context.CourseTabs.Remove(courseTab);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
