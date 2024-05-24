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
}
