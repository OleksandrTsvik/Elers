using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.CourseTabs.UpdateCourseTab;

public class UpdateCourseTabCommandHandler : ICommandHandler<UpdateCourseTabCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCourseTabCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateCourseTabCommand request, CancellationToken cancellationToken)
    {
        CourseTab? courseTab = await _context.CourseTabs
            .FirstOrDefaultAsync(x => x.Id == request.TabId, cancellationToken);

        if (courseTab is null)
        {
            return CourseTabErrors.NotFound(request.TabId);
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            courseTab.Name = request.Name;
        }

        if (request.IsActive.HasValue)
        {
            courseTab.IsActive = request.IsActive.Value;
        }

        if (request.ShowMaterialsCount.HasValue)
        {
            courseTab.ShowMaterialsCount = request.ShowMaterialsCount.Value;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
