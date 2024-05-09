using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.CourseTabs.UpdateCourseTabColor;

public class UpdateCourseTabColorCommandHandler : ICommandHandler<UpdateCourseTabColorCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCourseTabColorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateCourseTabColorCommand request, CancellationToken cancellationToken)
    {
        CourseTab? courseTab = await _context.CourseTabs
            .FirstOrDefaultAsync(x => x.Id == request.TabId, cancellationToken);

        if (courseTab is null)
        {
            return CourseTabErrors.NotFound(request.TabId);
        }

        courseTab.Color = request.Color;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
