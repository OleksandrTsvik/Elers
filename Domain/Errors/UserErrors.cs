using Domain.Constants;
using Domain.Shared;

namespace Domain.Errors;

public static class UserErrors
{
    public static Error NotFound(Guid userId) => Error.NotFound(
        ErrorCodes.Users.NotFound,
        $"The user with the Id = '{userId}' was not found.", userId);

    public static Error NotFoundByEmail(string email) => Error.NotFound(
        ErrorCodes.Users.NotFoundByEmail,
        $"The user with the Email = '{email}' was not found.", email);

    public static Error EmailNotUnique() => Error.Conflict(
        ErrorCodes.Users.EmailNotUnique,
        "The provided email is not unique.");

    public static Error InvalidCredentials() => Error.Unauthorized(
        ErrorCodes.Users.InvalidCredentials,
        "Invalid email or password.");

    public static Error NotFoundByUserContext() => Error.Unauthorized(
        ErrorCodes.Users.NotFoundByUserContext,
        "User not found.");
}
