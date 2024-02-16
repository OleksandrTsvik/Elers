using Domain.Shared;

namespace Domain.Errors;

public static class UserErrors
{
    public static Error NotFound(Guid userId) => Error.NotFound(
        "Users.NotFound",
        $"The user with the Id = '{userId}' was not found");

    public static Error NotFoundByEmail(string email) => Error.NotFound(
        "Users.NotFoundByEmail",
        $"The user with the Email = '{email}' was not found");

    public static Error EmailNotUnique() => Error.Conflict(
        "Users.EmailNotUnique",
        "The provided email is not unique");

    public static Error InvalidCredentials() => Error.Unauthorized(
        "Users.InvalidCredentials",
        "Invalid email or password");

    public static Error NotFoundByUserContext() => Error.Unauthorized(
        "Users.NotFoundByUserContext",
        "User not found");
}
