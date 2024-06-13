using Application.Profile.GetMyEnrolledCourses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProfileController : ApiControllerBase
{
    [HttpGet("enrolled-courses")]
    public async Task<IActionResult> GetMyEnrolledCourses(CancellationToken cancellationToken)
    {
        var query = new GetMyEnrolledCoursesQuery();

        return HandleResult(await Sender.Send(query, cancellationToken));
    }
}
