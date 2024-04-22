using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Permissions.GetListPermissions;

public class GetListPermissionsQueryHandler
    : IQueryHandler<GetListPermissionsQuery, GetListPermissionItemResponse[]>
{
    private readonly IApplicationDbContext _context;
    private readonly ITranslator _translator;

    public GetListPermissionsQueryHandler(
        IApplicationDbContext context,
        ITranslator translator)
    {
        _context = context;
        _translator = translator;
    }

    public async Task<Result<GetListPermissionItemResponse[]>> Handle(
        GetListPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        GetListPermissionItemResponse[] permissions = await _context.Permissions
            .Select(x => new GetListPermissionItemResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = _translator.GetString(x.Name)
            })
            .OrderBy(x => x.Name)
            .ToArrayAsync(cancellationToken);

        return permissions;
    }
}
