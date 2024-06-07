using Domain.Constants;
using Domain.Shared;

namespace Domain.Errors;

public static class CourseErrors
{
    public static Error NotFound(Guid courseId) => Error.NotFound(
        ErrorCodes.Courses.NotFound,
        $"The course with the Id = '{courseId}' was not found.", courseId);

    public static Error NotFoundByTabId(Guid courseTabId) => Error.NotFound(
        ErrorCodes.Courses.NotFoundByTabId,
        $"Course by tab Id = '{courseTabId}' was not found.", courseTabId);
}
