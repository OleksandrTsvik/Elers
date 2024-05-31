using Application.CourseMembers.EnrollToCourse;
using Application.CourseMembers.UnenrollFromCourse;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CourseMembersController : ApiControllerBase
{
    [HttpPost("enroll/{courseId:guid}")]
    public async Task<IActionResult> EnrollToCourse(
        Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new EnrollToCourseCommand(courseId);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }

    [HttpPost("unenroll/{courseId:guid}")]
    public async Task<IActionResult> UnenrollFromCourse(
        Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new UnenrollFromCourseCommand(courseId);

        return HandleResult(await Sender.Send(command, cancellationToken));
    }
}
