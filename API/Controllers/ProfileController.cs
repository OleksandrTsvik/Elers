using API.Contracts;
using Application.Profile.ChangeAvatar;
using Application.Profile.ChangeCurrentUserPassword;
using Application.Profile.DeleteAvatar;
using Application.Profile.GetCurrentProfile;
using Application.Profile.GetMyEnrolledCourses;
using Application.Profile.UpdateCurrentProfile;
using Domain.Enums;
using Infrastructure.Authentication;
using Infrastructure.Files;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProfileController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCurrentProfile(CancellationToken cancellationToken)
    {
        var query = new GetCurrentProfileQuery();

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HttpGet("enrolled-courses")]
    public async Task<IActionResult> GetMyEnrolledCourses(CancellationToken cancellationToken)
    {
        var query = new GetMyEnrolledCoursesQuery();

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HasPermission(PermissionType.UpdateOwnProfile)]
    [HttpPut]
    public async Task<IActionResult> UpdateCurrentProfile(
        [FromBody] UpdateCurrentProfileRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCurrentProfileCommand(
            request.Email,
            request.FirstName,
            request.LastName,
            request.Patronymic,
            request.BirthDate);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HasPermission(PermissionType.UpdateOwnPassword)]
    [HttpPatch("password")]
    public async Task<IActionResult> ChangeCurrentUserPassword(
        [FromBody] ChangeCurrentUserPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ChangeCurrentUserPasswordCommand(
            request.CurrentPassword,
            request.NewPassword,
            request.ConfirmPassword);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HttpPatch("avatar")]
    public async Task<IActionResult> ChangeAvatar(
        [FromForm] ChangeAvatarRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ChangeAvatarCommand(new FormFileProxy(request.Avatar));

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HttpDelete("avatar")]
    public async Task<IActionResult> DeleteAvatar(CancellationToken cancellationToken)
    {
        var command = new DeleteAvatarCommand();

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
