using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseTabs.DeleteCourseTab;

public class DeleteCourseTabCommandHandler : ICommandHandler<DeleteCourseTabCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseTabRepository _courseTabRepository;

    public DeleteCourseTabCommandHandler(IUnitOfWork unitOfWork, ICourseTabRepository courseTabRepository)
    {
        _unitOfWork = unitOfWork;
        _courseTabRepository = courseTabRepository;
    }

    public async Task<Result> Handle(DeleteCourseTabCommand request, CancellationToken cancellationToken)
    {
        CourseTab? courseTab = await _courseTabRepository.GetByIdAsync(request.TabId, cancellationToken);

        if (courseTab is null)
        {
            return CourseTabErrors.NotFound(request.TabId);
        }

        _courseTabRepository.Remove(courseTab);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
