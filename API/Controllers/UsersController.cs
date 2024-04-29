using Application.Users.GetListUsers;
using Domain.Enums;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : ApiControllerBase
{
    [HasPermission(PermissionType.ReadUser)]
    [HttpGet]
    public async Task<IActionResult> GetListUsers(CancellationToken cancellationToken)
    {
        var query = new GetListUsersQuery();

        return HandleResult(await Sender.Send(query, cancellationToken));
    }
}
