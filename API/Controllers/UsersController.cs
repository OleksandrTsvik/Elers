using Application.Users.CreateUser;
using Application.Users.DeleteUser;
using Application.Users.GetListUsers;
using Application.Users.GetUserById;
using Application.Users.UpdateUser;
using Domain.Enums;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : ApiControllerBase
{
    [HasPermission(PermissionType.ReadUser)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

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

    [HasPermission(PermissionType.UpdateUser)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser(
        Guid id,
        [FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateUserCommand(
            id,
            request.Email,
            request.Password,
            request.FirstName,
            request.LastName,
            request.Patronymic,
            request.RoleIds);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasPermission(PermissionType.DeleteUser)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(id);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
