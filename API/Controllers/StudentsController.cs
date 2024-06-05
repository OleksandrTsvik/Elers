using Application.Students.GetCourseStudents;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class StudentsController : ApiControllerBase
{
    [HttpGet("course/{courseId:guid}")]
    public async Task<IActionResult> GetCourseStudents(
        Guid courseId,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseStudentsQuery(courseId);

        return HandleResult(await Sender.Send(query, cancellationToken));
    }
}
