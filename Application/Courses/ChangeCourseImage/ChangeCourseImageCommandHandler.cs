using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Application.Common.Services;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Courses.ChangeCourseImage;

public class ChangeCourseImageCommandHandler : ICommandHandler<ChangeCourseImageCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseRepository _courseRepository;
    private readonly IImageService _imageService;

    public ChangeCourseImageCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseRepository courseRepository,
        IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _courseRepository = courseRepository;
        _imageService = imageService;
    }

    public async Task<Result> Handle(ChangeCourseImageCommand request, CancellationToken cancellationToken)
    {
        Course? course = await _courseRepository.GetByIdWithCourseTabsAsync(request.Id, cancellationToken);

        if (course is null)
        {
            return CourseErrors.NotFound(request.Id);
        }

        if (course.ImageName is not null)
        {
            await _imageService.RemoveAsync(course.ImageName, cancellationToken);
        }

        Result<ImageUploadResult> addImageResult = await _imageService.AddAsync(
            request.Image, cancellationToken);

        if (addImageResult.IsFailure || addImageResult.Value is null)
        {
            return addImageResult.Error;
        }

        course.ImageUrl = addImageResult.Value.Url;
        course.ImageName = addImageResult.Value.UniqueName;

        _courseRepository.Update(course);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
