using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Application.Common.Services;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.UpdateCourseMaterialFile;

public class UpdateCourseMaterialFileCommandHandler : ICommandHandler<UpdateCourseMaterialFileCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly IFileService _fileService;

    public UpdateCourseMaterialFileCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseMaterialRepository courseMaterialRepository,
        IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _courseMaterialRepository = courseMaterialRepository;
        _fileService = fileService;
    }

    public async Task<Result> Handle(
        UpdateCourseMaterialFileCommand request,
        CancellationToken cancellationToken)
    {
        CourseMaterialFile? courseMaterial = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialFile>(request.Id, cancellationToken);

        if (courseMaterial is null)
        {
            return CourseMaterialErrors.NotFound(request.Id);
        }

        courseMaterial.Title = request.Title;

        if (request.File is not null)
        {
            await _fileService.RemoveAsync(courseMaterial.UniqueFileName, cancellationToken);

            Result<FileUploadResult> addFileResult = await _fileService.AddAsync(
                request.File, cancellationToken);

            if (addFileResult.IsFailure || addFileResult.Value is null)
            {
                return addFileResult.Error;
            }

            courseMaterial.FileName = addFileResult.Value.FileName;
            courseMaterial.UniqueFileName = addFileResult.Value.UniqueFileName;
        }

        await _courseMaterialRepository.UpdateAsync(courseMaterial, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
