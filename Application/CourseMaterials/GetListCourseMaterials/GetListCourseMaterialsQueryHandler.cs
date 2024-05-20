using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Entities;
using Domain.Shared;

namespace Application.CourseMaterials.GetListCourseMaterials;

public class GetListCourseMaterialsQueryHandler
    : IQueryHandler<GetListCourseMaterialsQuery, List<CourseMaterial>>
{
    private readonly ICourseMaterialQueries _courseMaterialQueries;

    public GetListCourseMaterialsQueryHandler(ICourseMaterialQueries courseMaterialQueries)
    {
        _courseMaterialQueries = courseMaterialQueries;
    }

    public async Task<Result<List<CourseMaterial>>> Handle(
        GetListCourseMaterialsQuery request,
        CancellationToken cancellationToken)
    {
        return await _courseMaterialQueries.GetListCourseMaterialsAsync(cancellationToken);
    }
}
