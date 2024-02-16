using Domain.Shared;

namespace Domain.Errors;

public static class RefreshTokenErrors
{
    public static Error InvalidToken() => Error.Unauthorized(
        "RefreshTokens.InvalidToken",
        "Invalid refresh token");
}
