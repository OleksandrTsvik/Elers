using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Application.Common.Services;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.CreateCourseMaterialFile;

public class CreateCourseMaterialFileCommandHandler : ICommandHandler<CreateCourseMaterialFileCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly ICourseTabRepository _courseTabRepository;
    private readonly IFileService _fileService;

    public CreateCourseMaterialFileCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseMaterialRepository courseMaterialRepository,
        ICourseTabRepository courseTabRepository,
        IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _courseMaterialRepository = courseMaterialRepository;
        _courseTabRepository = courseTabRepository;
        _fileService = fileService;
    }

    public async Task<Result> Handle(
        CreateCourseMaterialFileCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _courseTabRepository.ExistsByIdAsync(request.TabId, cancellationToken))
        {
            return CourseTabErrors.NotFound(request.TabId);
        }

        Result<FileUploadResult> addFileResult = await _fileService.AddAsync(request.File, cancellationToken);

        if (addFileResult.IsFailure || addFileResult.Value is null)
        {
            return addFileResult.Error;
        }

        var courseMaterial = new CourseMaterialFile
        {
            CourseTabId = request.TabId,
            Title = request.Title,
            FileName = addFileResult.Value.FileName,
            UniqueFileName = addFileResult.Value.UniqueFileName,
        };

        await _courseMaterialRepository.AddAsync(courseMaterial, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
