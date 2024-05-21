using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseTabs.UpdateCourseTabColor;

public class UpdateCourseTabColorCommandHandler : ICommandHandler<UpdateCourseTabColorCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseTabRepository _courseTabRepository;

    public UpdateCourseTabColorCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseTabRepository courseTabRepository)
    {
        _unitOfWork = unitOfWork;
        _courseTabRepository = courseTabRepository;
    }

    public async Task<Result> Handle(UpdateCourseTabColorCommand request, CancellationToken cancellationToken)
    {
        CourseTab? courseTab = await _courseTabRepository.GetByIdAsync(request.TabId, cancellationToken);

        if (courseTab is null)
        {
            return CourseTabErrors.NotFound(request.TabId);
        }

        courseTab.Color = request.Color;

        _courseTabRepository.Update(courseTab);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
