using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Entities;
using Domain.Shared;

namespace Application.CourseMaterials.GetListCourseMaterialsByTabId;

public class GetListCourseMaterialsByTabIdQueryHandler
    : IQueryHandler<GetListCourseMaterialsByTabIdQuery, List<CourseMaterial>>
{
    private readonly ICourseMaterialQueries _courseMaterialQueries;

    public GetListCourseMaterialsByTabIdQueryHandler(ICourseMaterialQueries courseMaterialQueries)
    {
        _courseMaterialQueries = courseMaterialQueries;
    }

    public async Task<Result<List<CourseMaterial>>> Handle(
        GetListCourseMaterialsByTabIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _courseMaterialQueries.GetListByTabId(request.TabId, cancellationToken);
    }
}
