using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Queries;
using Domain.Shared;

namespace Application.Permissions.GetListPermissions;

public class GetListPermissionsQueryHandler
    : IQueryHandler<GetListPermissionsQuery, GetListPermissionItemResponse[]>
{
    private readonly IPermissionQueries _permissionQueries;
    private readonly ITranslator _translator;

    public GetListPermissionsQueryHandler(
        IPermissionQueries permissionQueries,
        ITranslator translator)
    {
        _permissionQueries = permissionQueries;
        _translator = translator;
    }

    public async Task<Result<GetListPermissionItemResponse[]>> Handle(
        GetListPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        GetListPermissionItemResponseDto[] permissionDtos = await _permissionQueries
            .GetListPermissions(cancellationToken);

        GetListPermissionItemResponse[] permissions = permissionDtos
            .Select(x => new GetListPermissionItemResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = _translator.GetString(x.Name)
            })
            .ToArray();

        return permissions;
    }
}
