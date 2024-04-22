using Application.Permissions.GetListPermissions;
using Domain.Enums;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PermissionsController : ApiControllerBase
{
    [HasPermission(PermissionType.ReadPermission)]
    [HttpGet]
    public async Task<IActionResult> GetListPermissions(CancellationToken cancellationToken)
    {
        var query = new GetListPermissionsQuery();

        return HandleResult(await Sender.Send(query, cancellationToken));
    }
}
