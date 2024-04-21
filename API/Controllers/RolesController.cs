using Application.Roles.CreateRole;
using Application.Roles.DeleteRole;
using Application.Roles.GetListRoles;
using Application.Roles.GetRoleById;
using Application.Roles.UpdateRole;
using Domain.Enums;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class RolesController : ApiControllerBase
{
    [HasPermission(PermissionType.ReadRole)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRoleById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetRoleByIdQuery(id);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HasPermission(PermissionType.ReadRole)]
    [HttpGet]
    public async Task<IActionResult> GetListRoles(CancellationToken cancellationToken)
    {
        var query = new GetListRolesQuery();

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HasPermission(PermissionType.CreateRole)]
    [HttpPost]
    public async Task<IActionResult> CreateRole(
        [FromBody] CreateRoleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateRoleCommand(request.Name);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasPermission(PermissionType.UpdateRole)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateRole(
        Guid id,
        [FromBody] UpdateRoleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateRoleCommand(id, request.Name, request.PermissionIds);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasPermission(PermissionType.DeleteRole)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRole(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteRoleCommand(id);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
