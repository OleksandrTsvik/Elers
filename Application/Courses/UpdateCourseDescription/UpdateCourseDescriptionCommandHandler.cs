using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Courses.UpdateCourseDescription;

public class UpdateCourseDescriptionCommandHandler : ICommandHandler<UpdateCourseDescriptionCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseRepository _courseRepository;

    public UpdateCourseDescriptionCommandHandler(IUnitOfWork unitOfWork, ICourseRepository courseRepository)
    {
        _unitOfWork = unitOfWork;
        _courseRepository = courseRepository;
    }

    public async Task<Result> Handle(
        UpdateCourseDescriptionCommand request,
        CancellationToken cancellationToken)
    {
        Course? course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);

        if (course is null)
        {
            return CourseErrors.NotFound(request.CourseId);
        }

        course.Description = request.Description;

        _courseRepository.Update(course);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
