using Application.Permissions.GetListPermissions;
using Domain.Enums;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class PermissionsController : ApiControllerBase
{
    [HasPermission(PermissionType.ReadPermission, PermissionType.CreateRole)]
    [HttpGet]
    public async Task<IActionResult> GetListPermissions(CancellationToken cancellationToken)
    {
        var query = new GetListPermissionsQuery();

        return HandleResult(await Sender.Send(query, cancellationToken));
    }
}
