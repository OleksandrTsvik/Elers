using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.CourseTabs.UpdateCourseTabName;

public class UpdateCourseTabNameCommandHandler : ICommandHandler<UpdateCourseTabNameCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCourseTabNameCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateCourseTabNameCommand request, CancellationToken cancellationToken)
    {
        CourseTab? courseTab = await _context.CourseTabs
            .FirstOrDefaultAsync(x => x.Id == request.TabId, cancellationToken);

        if (courseTab is null)
        {
            return CourseTabErrors.NotFound(request.TabId);
        }

        courseTab.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
