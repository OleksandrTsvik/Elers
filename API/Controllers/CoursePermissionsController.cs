using Application.CoursePermissions.GetCourseMemberPermissions;
using Application.CoursePermissions.GetListCoursePermissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class CoursePermissionsController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpGet("{courseId:guid}")]
    public async Task<IActionResult> GetCourseMemberPermissions(
        Guid courseId,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseMemberPermissionsQuery(courseId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

    [HttpGet]
    public async Task<IActionResult> GetListCoursePermissions(CancellationToken cancellationToken)
    {
        var query = new GetListCoursePermissionsQuery();

        return HandleResult(await Sender.Send(query, cancellationToken));
    }
}
