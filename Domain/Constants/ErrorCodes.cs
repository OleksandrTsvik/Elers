namespace Domain.Constants;

public class ErrorCodes
{
    public static class Default
    {
        public const string NullValue = "Error.NullValue";
        public const string NullResult = "Error.NullResult";
    }

    public static class RefreshToken
    {
        public const string InvalidToken = "RefreshTokens.InvalidToken";
    }

    public static class User
    {
        public const string NotFound = "Users.NotFound";
        public const string NotFoundByEmail = "Users.NotFoundByEmail";
        public const string EmailNotUnique = "Users.EmailNotUnique";
        public const string InvalidCredentials = "Users.InvalidCredentials";
        public const string NotFoundByUserContext = "Users.NotFoundByUserContext";
    }
}
