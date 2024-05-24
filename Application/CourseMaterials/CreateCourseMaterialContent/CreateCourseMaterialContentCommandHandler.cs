using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.CreateCourseMaterialContent;

public class CreateCourseMaterialContentCommandHandler
    : ICommandHandler<CreateCourseMaterialContentCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly ICourseTabRepository _courseTabRepository;

    public CreateCourseMaterialContentCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseMaterialRepository courseMaterialRepository,
        ICourseTabRepository courseTabRepository)
    {
        _unitOfWork = unitOfWork;
        _courseMaterialRepository = courseMaterialRepository;
        _courseTabRepository = courseTabRepository;
    }

    public async Task<Result> Handle(
        CreateCourseMaterialContentCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _courseTabRepository.ExistsByIdAsync(request.TabId, cancellationToken))
        {
            return CourseTabErrors.NotFound(request.TabId);
        }

        var courseMaterial = new CourseMaterialContent
        {
            CourseTabId = request.TabId,
            Content = request.Content
        };

        await _courseMaterialRepository.AddAsync(courseMaterial, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
