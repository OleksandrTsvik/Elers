using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.MoveMaterialToAnotherTab;

public class MoveMaterialToAnotherTabCommandHandler : ICommandHandler<MoveMaterialToAnotherTabCommand>
{
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly ICourseTabRepository _courseTabRepository;
    private readonly ICourseRepository _courseRepository;

    public MoveMaterialToAnotherTabCommandHandler(
        ICourseMaterialRepository courseMaterialRepository,
        ICourseTabRepository courseTabRepository,
        ICourseRepository courseRepository)
    {
        _courseMaterialRepository = courseMaterialRepository;
        _courseTabRepository = courseTabRepository;
        _courseRepository = courseRepository;
    }

    public async Task<Result> Handle(
        MoveMaterialToAnotherTabCommand request,
        CancellationToken cancellationToken)
    {
        CourseMaterial? courseMaterial = await _courseMaterialRepository.GetByIdAsync(
            request.MaterialId, cancellationToken);

        if (courseMaterial is null)
        {
            return CourseMaterialErrors.NotFound(request.MaterialId);
        }

        if (!await _courseTabRepository.ExistsByIdAsync(request.NewCourseTabId, cancellationToken))
        {
            return CourseTabErrors.NotFound(request.NewCourseTabId);
        }

        if (!await _courseRepository.CourseTabsInSameCourseAsync(
            [courseMaterial.CourseTabId, request.NewCourseTabId], cancellationToken))
        {
            return CourseMaterialErrors.CrossCourseMove();
        }

        courseMaterial.CourseTabId = request.NewCourseTabId;

        await _courseMaterialRepository.UpdateAsync(courseMaterial, cancellationToken);

        return Result.Success();
    }
}
