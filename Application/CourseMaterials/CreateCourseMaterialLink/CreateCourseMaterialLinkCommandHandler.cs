using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.CreateCourseMaterialLink;

public class CreateCourseMaterialLinkCommandHandler
    : ICommandHandler<CreateCourseMaterialLinkCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly ICourseTabRepository _courseTabRepository;

    public CreateCourseMaterialLinkCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseMaterialRepository courseMaterialRepository,
        ICourseTabRepository courseTabRepository)
    {
        _unitOfWork = unitOfWork;
        _courseMaterialRepository = courseMaterialRepository;
        _courseTabRepository = courseTabRepository;
    }

    public async Task<Result> Handle(
        CreateCourseMaterialLinkCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _courseTabRepository.ExistsByIdAsync(request.TabId, cancellationToken))
        {
            return CourseTabErrors.NotFound(request.TabId);
        }

        var courseMaterial = new CourseMaterialLink
        {
            CourseTabId = request.TabId,
            Title = request.Title,
            Link = request.Link
        };

        await _courseMaterialRepository.AddAsync(courseMaterial, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
