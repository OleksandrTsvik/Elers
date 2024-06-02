using Domain.Constants;
using Domain.Shared;

namespace Domain.Errors;

public static class CourseMemberErrors
{
    public static Error NotFound() => Error.NotFound(
        ErrorCodes.CourseMembers.NotFound,
        "The member is absent.");

    public static Error AlreadyEnrolled() => Error.Conflict(
        ErrorCodes.CourseMembers.AlreadyEnrolled,
        "You are already enrolled in the course.");

    public static Error NotEnrolled() => Error.Conflict(
        ErrorCodes.CourseMembers.NotEnrolled,
        "You cannot unenroll from a course that you have not enrolled in.");

    public static Error RoleFromAnotherCourse() => Error.Conflict(
        ErrorCodes.CourseMembers.RoleFromAnotherCourse,
        "You are trying to assign a role that belongs to another course.");
}
