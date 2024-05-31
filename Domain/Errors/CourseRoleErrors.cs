using Domain.Constants;
using Domain.Shared;

namespace Domain.Errors;

public static class CourseRoleErrors
{
    public static Error NotFound(Guid roleId) => Error.NotFound(
        ErrorCodes.CourseRoles.NotFound,
        $"The course role with the Id = '{roleId}' was not found.", roleId);

    public static Error NameNotUnique(string name) => Error.Conflict(
        ErrorCodes.CourseRoles.NameNotUnique,
        $"The course role with the Name = '{name}' already exists.", name);
}
