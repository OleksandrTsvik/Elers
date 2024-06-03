using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.CreateCourseMaterialAssignment;

public class CreateCourseMaterialAssignmentCommandHandler
    : ICommandHandler<CreateCourseMaterialAssignmentCommand>
{
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly ICourseTabRepository _courseTabRepository;

    public CreateCourseMaterialAssignmentCommandHandler(
        ICourseMaterialRepository courseMaterialRepository,
        ICourseTabRepository courseTabRepository)
    {
        _courseMaterialRepository = courseMaterialRepository;
        _courseTabRepository = courseTabRepository;
    }

    public async Task<Result> Handle(
        CreateCourseMaterialAssignmentCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _courseTabRepository.ExistsByIdAsync(request.TabId, cancellationToken))
        {
            return CourseTabErrors.NotFound(request.TabId);
        }

        var courseMaterial = new CourseMaterialAssignment
        {
            CourseTabId = request.TabId,
            Title = request.Title,
            Description = request.Description,
            Deadline = request.Deadline,
            MaxFiles = request.MaxFiles,
            MaxGrade = request.MaxGrade
        };

        await _courseMaterialRepository.AddAsync(courseMaterial, cancellationToken);

        return Result.Success();
    }
}
