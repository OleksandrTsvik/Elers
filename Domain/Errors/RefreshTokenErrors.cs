using Domain.Constants;
using Domain.Shared;

namespace Domain.Errors;

public static class RefreshTokenErrors
{
    public static Error InvalidToken() => Error.Unauthorized(
        ErrorCodes.RefreshTokens.InvalidToken,
        "Invalid refresh token.");
}
