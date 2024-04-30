using Application.Users.CreateUser;
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

    [HasPermission(PermissionType.CreateUser)]
    [HttpPost]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(
            request.Email,
            request.Password,
            request.FirstName,
            request.LastName,
            request.Patronymic,
            request.RoleIds);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
