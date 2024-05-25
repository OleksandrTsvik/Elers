using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Services;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.DeleteCourseMaterial;

public class DeleteCourseMaterialCommandHandler : ICommandHandler<DeleteCourseMaterialCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly IFileService _fileService;

    public DeleteCourseMaterialCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseMaterialRepository courseMaterialRepository,
        IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _courseMaterialRepository = courseMaterialRepository;
        _fileService = fileService;
    }

    public async Task<Result> Handle(DeleteCourseMaterialCommand request, CancellationToken cancellationToken)
    {
        CourseMaterial? courseMaterial = await _courseMaterialRepository.GetByIdAsync(
            request.Id, cancellationToken);

        if (courseMaterial is null)
        {
            return CourseMaterialErrors.NotFound(request.Id);
        }

        if (courseMaterial is CourseMaterialFile courseMaterialFile)
        {
            await _fileService.RemoveAsync(courseMaterialFile.UniqueFileName, cancellationToken);
        }

        await _courseMaterialRepository.RemoveAsync(courseMaterial.Id, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
