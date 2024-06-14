using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Services;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Courses.DeleteCourse;

public class DeleteCourseCommandHandler : ICommandHandler<DeleteCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseRepository _courseRepository;
    private readonly IManualGradesColumnRepository _manualGradesColumnRepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly IImageService _imageService;
    private readonly ICourseService _courseService;

    public DeleteCourseCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseRepository courseRepository,
        IManualGradesColumnRepository manualGradesColumnRepository,
        IGradeRepository gradeRepository,
        IImageService imageService,
        ICourseService courseService)
    {
        _unitOfWork = unitOfWork;
        _courseRepository = courseRepository;
        _manualGradesColumnRepository = manualGradesColumnRepository;
        _gradeRepository = gradeRepository;
        _imageService = imageService;
        _courseService = courseService;
    }

    public async Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        Course? course = await _courseRepository.GetByIdWithCourseTabsAsync(request.Id, cancellationToken);

        if (course is null)
        {
            return CourseErrors.NotFound(request.Id);
        }

        IEnumerable<Guid> courseTabIds = course.CourseTabs.Select(x => x.Id);

        await _manualGradesColumnRepository.RemoveRangeByCourseIdAsync(course.Id, cancellationToken);
        await _gradeRepository.RemoveRangeByCourseIdAsync(course.Id, cancellationToken);
        await _courseService.RemoveMaterialsByCourseTabIdsAsync(courseTabIds, false, cancellationToken);

        if (course.ImageName is not null)
        {
            await _imageService.RemoveAsync(course.ImageName, cancellationToken);
        }

        _courseRepository.Remove(course);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
