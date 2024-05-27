using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Services;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Courses.DeleteCourseImage;

public class DeleteCourseImageCommandHandler : ICommandHandler<DeleteCourseImageCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseRepository _courseRepository;
    private readonly IImageService _imageService;

    public DeleteCourseImageCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseRepository courseRepository,
        IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _courseRepository = courseRepository;
        _imageService = imageService;
    }

    public async Task<Result> Handle(DeleteCourseImageCommand request, CancellationToken cancellationToken)
    {
        Course? course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);

        if (course is null)
        {
            return CourseErrors.NotFound(request.Id);
        }

        if (course.ImageName is not null)
        {
            await _imageService.RemoveAsync(course.ImageName, cancellationToken);
        }

        course.ImageUrl = null;
        course.ImageName = null;

        _courseRepository.Update(course);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
