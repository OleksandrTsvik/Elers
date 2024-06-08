using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.GetCourseMaterialTest;

public class GetCourseMaterialTestQueryHandler
    : IQueryHandler<GetCourseMaterialTestQuery, CourseMaterialTest>
{
    private readonly ICourseMaterialRepository _courseMaterialRepository;

    public GetCourseMaterialTestQueryHandler(ICourseMaterialRepository courseMaterialRepository)
    {
        _courseMaterialRepository = courseMaterialRepository;
    }

    public async Task<Result<CourseMaterialTest>> Handle(
        GetCourseMaterialTestQuery request,
        CancellationToken cancellationToken)
    {
        CourseMaterialTest? courseMaterial = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialTest>(request.MaterialId, cancellationToken);

        if (courseMaterial is null)
        {
            return CourseMaterialErrors.NotFound(request.MaterialId);
        }

        return courseMaterial;
    }
}
