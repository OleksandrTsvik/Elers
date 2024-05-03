using Domain.Constants;
using Domain.Shared;

namespace Domain.Errors;

public static class CourseErrors
{
    public static Error NotFound(Guid courseId) => Error.NotFound(
        ErrorCodes.Courses.NotFound,
        $"The course with the Id = '{courseId}' was not found.", courseId);
}
