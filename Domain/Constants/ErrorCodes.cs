namespace Domain.Constants;

public static class ErrorCodes
{
    public static class Error
    {
        public const string NullValue = "Error.NullValue";
        public const string NullResult = "Error.NullResult";
    }

    public static class Courses
    {
        public const string NotFound = "Courses.NotFound";
    }

    public static class CourseMaterials
    {
        public const string NotFound = "CourseMaterials.NotFound";
    }

    public static class CourseTabs
    {
        public const string NotFound = "CourseTabs.NotFound";
    }

    public static class Files
    {
        public const string Empty = "Files.Empty";
        public const string SizeLimit = "Files.SizeLimit";
    }

    public static class RefreshTokens
    {
        public const string InvalidToken = "RefreshTokens.InvalidToken";
    }

    public static class Roles
    {
        public const string NotFound = "Roles.NotFound";
        public const string NameNotUnique = "Roles.NameNotUnique";
    }

    public static class Users
    {
        public const string NotFound = "Users.NotFound";
        public const string NotFoundByEmail = "Users.NotFoundByEmail";
        public const string EmailNotUnique = "Users.EmailNotUnique";
        public const string InvalidCredentials = "Users.InvalidCredentials";
        public const string NotFoundByUserContext = "Users.NotFoundByUserContext";
        public const string Unauthorized = "Users.Unauthorized";
    }
}
