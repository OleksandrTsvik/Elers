using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.UpdateCourseMaterialAssignment;

public class UpdateCourseMaterialAssignmentCommandHandler
    : ICommandHandler<UpdateCourseMaterialAssignmentCommand>
{
    private readonly ICourseMaterialRepository _courseMaterialRepository;

    public UpdateCourseMaterialAssignmentCommandHandler(ICourseMaterialRepository courseMaterialRepository)
    {
        _courseMaterialRepository = courseMaterialRepository;
    }

    public async Task<Result> Handle(
        UpdateCourseMaterialAssignmentCommand request,
        CancellationToken cancellationToken)
    {
        CourseMaterialAssignment? courseMaterial = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialAssignment>(request.MaterialId, cancellationToken);

        if (courseMaterial is null)
        {
            return CourseMaterialErrors.NotFound(request.MaterialId);
        }

        courseMaterial.Title = request.Title;
        courseMaterial.Description = request.Description;
        courseMaterial.Deadline = request.Deadline;
        courseMaterial.MaxFiles = request.MaxFiles;
        courseMaterial.MaxGrade = request.MaxGrade;

        await _courseMaterialRepository.UpdateAsync(courseMaterial, cancellationToken);

        return Result.Success();
    }
}
