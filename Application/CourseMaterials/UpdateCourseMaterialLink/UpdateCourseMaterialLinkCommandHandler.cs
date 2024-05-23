using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.UpdateCourseMaterialLink;

public class UpdateCourseMaterialLinkCommandHandler : ICommandHandler<UpdateCourseMaterialLinkCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseMaterialRepository _courseMaterialRepository;

    public UpdateCourseMaterialLinkCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseMaterialRepository courseMaterialRepository)
    {
        _unitOfWork = unitOfWork;
        _courseMaterialRepository = courseMaterialRepository;
    }

    public async Task<Result> Handle(
        UpdateCourseMaterialLinkCommand request,
        CancellationToken cancellationToken)
    {
        CourseMaterialLink? courseMaterial = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialLink>(request.Id, cancellationToken);

        if (courseMaterial is null)
        {
            return CourseMaterialErrors.NotFound(request.Id);
        }

        courseMaterial.Title = request.Title;
        courseMaterial.Link = request.Link;

        await _courseMaterialRepository.UpdateAsync(courseMaterial, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
