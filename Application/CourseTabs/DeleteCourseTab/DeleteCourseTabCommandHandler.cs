using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Services;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseTabs.DeleteCourseTab;

public class DeleteCourseTabCommandHandler : ICommandHandler<DeleteCourseTabCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseTabRepository _courseTabRepository;
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly IFileService _fileService;

    public DeleteCourseTabCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseTabRepository courseTabRepository,
        ICourseMaterialRepository courseMaterialRepository,
        IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _courseTabRepository = courseTabRepository;
        _courseMaterialRepository = courseMaterialRepository;
        _fileService = fileService;
    }

    public async Task<Result> Handle(DeleteCourseTabCommand request, CancellationToken cancellationToken)
    {
        CourseTab? courseTab = await _courseTabRepository.GetByIdAsync(request.TabId, cancellationToken);

        if (courseTab is null)
        {
            return CourseTabErrors.NotFound(request.TabId);
        }

        List<string> uniqueFileNames = await _courseMaterialRepository
            .GetUniqueFileNamesByCourseTabIdAsync(courseTab.Id, cancellationToken);

        if (uniqueFileNames.Count != 0)
        {
            await _fileService.RemoveRangeAsync(uniqueFileNames, cancellationToken);
        }

        await _courseMaterialRepository.RemoveRangeByCourseTabIdAsync(courseTab.Id, cancellationToken);

        _courseTabRepository.Remove(courseTab);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
