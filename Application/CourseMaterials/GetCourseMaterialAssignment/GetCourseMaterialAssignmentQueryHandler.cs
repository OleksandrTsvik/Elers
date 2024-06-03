using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.GetCourseMaterialAssignment;

public class GetCourseMaterialAssignmentQueryHandler
    : IQueryHandler<GetCourseMaterialAssignmentQuery, CourseMaterialAssignment>
{
    private readonly ICourseMaterialRepository _courseMaterialRepository;

    public GetCourseMaterialAssignmentQueryHandler(ICourseMaterialRepository courseMaterialRepository)
    {
        _courseMaterialRepository = courseMaterialRepository;
    }

    public async Task<Result<CourseMaterialAssignment>> Handle(
        GetCourseMaterialAssignmentQuery request,
        CancellationToken cancellationToken)
    {
        CourseMaterialAssignment? courseMaterial = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialAssignment>(request.MaterialId, cancellationToken);

        if (courseMaterial is null)
        {
            return CourseMaterialErrors.NotFound(request.MaterialId);
        }

        return courseMaterial;
    }
}
