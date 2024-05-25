using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Entities;
using Domain.Shared;

namespace Application.CourseMaterials.GetListCourseMaterialsByTabIdToEdit;

public class GetListCourseMaterialsByTabIdToEditQueryHandler
    : IQueryHandler<GetListCourseMaterialsByTabIdToEditQuery, List<CourseMaterial>>
{
    private readonly ICourseMaterialQueries _courseMaterialQueries;

    public GetListCourseMaterialsByTabIdToEditQueryHandler(ICourseMaterialQueries courseMaterialQueries)
    {
        _courseMaterialQueries = courseMaterialQueries;
    }

    public async Task<Result<List<CourseMaterial>>> Handle(
        GetListCourseMaterialsByTabIdToEditQuery request,
        CancellationToken cancellationToken)
    {
        return await _courseMaterialQueries.GetListByTabIdToEdit(request.TabId, cancellationToken);
    }
}
