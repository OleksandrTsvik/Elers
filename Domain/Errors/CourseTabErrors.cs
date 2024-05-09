using Domain.Constants;
using Domain.Shared;

namespace Domain.Errors;

public static class CourseTabErrors
{
    public static Error NotFound(Guid courseTabId) => Error.NotFound(
        ErrorCodes.CourseTabs.NotFound,
        $"The course tab with the Id = '{courseTabId}' was not found.", courseTabId);
}