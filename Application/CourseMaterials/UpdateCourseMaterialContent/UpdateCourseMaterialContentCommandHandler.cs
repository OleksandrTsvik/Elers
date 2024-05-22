using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.UpdateCourseMaterialContent;

public class UpdateCourseMaterialContentCommandHandler
    : ICommandHandler<UpdateCourseMaterialContentCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseMaterialRepository _courseMaterialRepository;

    public UpdateCourseMaterialContentCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseMaterialRepository courseMaterialRepository)
    {
        _unitOfWork = unitOfWork;
        _courseMaterialRepository = courseMaterialRepository;
    }

    public async Task<Result> Handle(
        UpdateCourseMaterialContentCommand request,
        CancellationToken cancellationToken)
    {
        CourseMaterialContent? courseMaterial = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialContent>(request.Id, cancellationToken);

        if (courseMaterial is null)
        {
            return CourseMaterialErrors.NotFound(request.Id);
        }

        courseMaterial.Content = request.Content;

        await _courseMaterialRepository.UpdateAsync(courseMaterial, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
