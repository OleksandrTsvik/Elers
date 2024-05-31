using Application.CourseMembers.EnrollToCourse;
using Application.CourseMembers.GetListCourseMembers;
using Application.CourseMembers.UnenrollFromCourse;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CourseMembersController : ApiControllerBase
{
    [HttpGet("{courseId:guid}")]
    public async Task<IActionResult> GetListCourseMembers(
        Guid courseId,
        CancellationToken cancellationToken)
    {
        var query = new GetListCourseMembersQuery(courseId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }

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
