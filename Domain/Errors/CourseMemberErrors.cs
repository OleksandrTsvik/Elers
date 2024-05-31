using Domain.Constants;
using Domain.Shared;

namespace Domain.Errors;

public static class CourseMemberErrors
{
    public static Error AlreadyEnrolled() => Error.Conflict(
        ErrorCodes.CourseMembers.AlreadyEnrolled,
        $"You are already enrolled in the course.");

    public static Error NotEnrolled() => Error.Conflict(
        ErrorCodes.CourseMembers.NotEnrolled,
        $"You cannot unenroll from a course that you have not enrolled in.");
}
