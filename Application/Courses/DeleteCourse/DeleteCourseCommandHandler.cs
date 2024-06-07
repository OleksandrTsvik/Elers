using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
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
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly ISubmittedAssignmentRepository _submittedAssignmentRepository;
    private readonly ISubmittedAssignmentQueries _submittedAssignmentQueries;
    private readonly IFileService _fileService;
    private readonly IImageService _imageService;

    public DeleteCourseCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseRepository courseRepository,
        ICourseMaterialRepository courseMaterialRepository,
        IGradeRepository gradeRepository,
        ISubmittedAssignmentRepository submittedAssignmentRepository,
        ISubmittedAssignmentQueries submittedAssignmentQueries,
        IFileService fileService,
        IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _courseRepository = courseRepository;
        _courseMaterialRepository = courseMaterialRepository;
        _gradeRepository = gradeRepository;
        _submittedAssignmentRepository = submittedAssignmentRepository;
        _submittedAssignmentQueries = submittedAssignmentQueries;
        _fileService = fileService;
        _imageService = imageService;
    }

    public async Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        Course? course = await _courseRepository.GetByIdWithCourseTabsAsync(request.Id, cancellationToken);

        if (course is null)
        {
            return CourseErrors.NotFound(request.Id);
        }

        IEnumerable<Guid> courseTabIds = course.CourseTabs.Select(x => x.Id);

        List<string> uniqueFileNames = await _courseMaterialRepository.GetUniqueFileNamesByCourseTabIdsAsync(
            courseTabIds, cancellationToken);

        uniqueFileNames.AddRange(await _submittedAssignmentQueries.GetSubmittedFilesByCourseTabIdsAsync(
            courseTabIds, cancellationToken));

        if (uniqueFileNames.Count != 0)
        {
            await _fileService.RemoveRangeAsync(uniqueFileNames, cancellationToken);
        }

        await _gradeRepository.RemoveRangeByCourseIdAsync(course.Id, cancellationToken);
        await _submittedAssignmentRepository.RemoveRangeByCourseTabIdsAsync(courseTabIds, cancellationToken);
        await _courseMaterialRepository.RemoveRangeByCourseTabIdsAsync(courseTabIds, cancellationToken);

        if (course.ImageName is not null)
        {
            await _imageService.RemoveAsync(course.ImageName, cancellationToken);
        }

        _courseRepository.Remove(course);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
