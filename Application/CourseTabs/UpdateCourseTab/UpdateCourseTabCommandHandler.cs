using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseTabs.UpdateCourseTab;

public class UpdateCourseTabCommandHandler : ICommandHandler<UpdateCourseTabCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseTabRepository _courseTabRepository;

    public UpdateCourseTabCommandHandler(IUnitOfWork unitOfWork, ICourseTabRepository courseTabRepository)
    {
        _unitOfWork = unitOfWork;
        _courseTabRepository = courseTabRepository;
    }

    public async Task<Result> Handle(UpdateCourseTabCommand request, CancellationToken cancellationToken)
    {
        CourseTab? courseTab = await _courseTabRepository.GetByIdAsync(request.TabId, cancellationToken);

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

        _courseTabRepository.Update(courseTab);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
