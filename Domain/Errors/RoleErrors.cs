using Domain.Constants;
using Domain.Shared;

namespace Domain.Errors;

public static class RoleErrors
{
    public static Error NotFound(Guid roleId) => Error.NotFound(
        ErrorCodes.Roles.NotFound,
        $"The role with the Id = '{roleId}' was not found.", roleId);

    public static Error NameNotUnique(string name) => Error.Conflict(
        ErrorCodes.Roles.NameNotUnique,
        $"The role with the Name = '{name}' already exists.", name);
}
