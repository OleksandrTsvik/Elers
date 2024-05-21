using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseTabs.CreateCourseTab;

public class CreateCourseTabCommandHandler : ICommandHandler<CreateCourseTabCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseTabRepository _courseTabRepository;

    public CreateCourseTabCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseRepository courseRepository,
        ICourseTabRepository courseTabRepository)
    {
        _unitOfWork = unitOfWork;
        _courseRepository = courseRepository;
        _courseTabRepository = courseTabRepository;
    }

    public async Task<Result<Guid>> Handle(CreateCourseTabCommand request, CancellationToken cancellationToken)
    {
        if (!await _courseRepository.ExistsByIdAsync(request.CourseId, cancellationToken))
        {
            return CourseErrors.NotFound(request.CourseId);
        }

        var courseTab = new CourseTab
        {
            CourseId = request.CourseId,
            Name = request.Name,
        };

        _courseTabRepository.Add(courseTab);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return courseTab.Id;
    }
}
