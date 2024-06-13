using Domain.Constants;
using Domain.Shared;

namespace Domain.Errors;

public static class CourseMaterialErrors
{
    public static Error NotFound(Guid courseMaterialId) => Error.NotFound(
        ErrorCodes.CourseMaterials.NotFound,
        $"The course material with the Id = '{courseMaterialId}' was not found.", courseMaterialId);

    public static Error FileNotFound() => Error.NotFound(
        ErrorCodes.CourseMaterials.FileNotFound,
        "File not found.");

    public static Error AccessDenied() => Error.Forbidden(
        ErrorCodes.CourseMaterials.AccessDenied,
        "You do not have permission to change the course materials.");

    public static Error CrossCourseMove() => Error.Validation(
        ErrorCodes.CourseMaterials.CrossCourseMove,
        "You cannot move course materials to another course.");
}
