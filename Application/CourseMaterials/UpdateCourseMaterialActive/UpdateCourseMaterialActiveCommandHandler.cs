using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.UpdateCourseMaterialActive;

public class UpdateCourseMaterialActiveCommandHandler : ICommandHandler<UpdateCourseMaterialActiveCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseMaterialRepository _courseMaterialRepository;

    public UpdateCourseMaterialActiveCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseMaterialRepository courseMaterialRepository)
    {
        _unitOfWork = unitOfWork;
        _courseMaterialRepository = courseMaterialRepository;
    }

    public async Task<Result> Handle(UpdateCourseMaterialActiveCommand request, CancellationToken cancellationToken)
    {
        CourseMaterial? courseMaterial = await _courseMaterialRepository.GetByIdAsync(
            request.Id, cancellationToken);

        if (courseMaterial is null)
        {
            return CourseMaterialErrors.NotFound(request.Id);
        }

        courseMaterial.IsActive = request.IsActive;

        await _courseMaterialRepository.UpdateAsync(courseMaterial, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
