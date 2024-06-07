namespace Domain.Constants;

public static class ErrorCodes
{
    public static class Error
    {
        public const string NullValue = "Error.NullValue";
        public const string NullResult = "Error.NullResult";
    }

    public static class Assignments
    {
        public const string NotFound = "Assignments.NotFound";
        public const string SubmittedNotFound = "Assignments.SubmittedNotFound";
        public const string Unavailable = "Assignments.Unavailable";
        public const string StudentsOnly = "Assignments.StudentsOnly";
        public const string EmptyFields = "Assignments.EmptyFields";
        public const string ManyFiles = "Assignments.ManyFiles";
        public const string AlreadyGraded = "Assignments.AlreadyGraded";
        public const string DeadlinePassed = "Assignments.DeadlinePassed";
        public const string FileNotFound = "Assignments.FileNotFound";
        public const string FileAccessDenied = "Assignments.FileAccessDenied";
        public const string SubmittedAccessDenied = "Assignments.SubmittedAccessDenied";
        public const string GradeAccessDenied = "Assignments.GradeAccessDenied";
        public const string GradeLimit = "Assignments.GradeLimit";
    }

    public static class Courses
    {
        public const string NotFound = "Courses.NotFound";
        public const string NotFoundByTabId = "Courses.NotFoundByTabId";
    }

    public static class CourseMaterials
    {
        public const string NotFound = "CourseMaterials.NotFound";
        public const string FileNotFound = "CourseMaterials.FileNotFound";
    }

    public static class CourseMembers
    {
        public const string NotFound = "CourseMembers.NotFound";
        public const string AlreadyEnrolled = "CourseMembers.AlreadyEnrolled";
        public const string NotEnrolled = "CourseMembers.NotEnrolled";
        public const string RoleFromAnotherCourse = "CourseMembers.RoleFromAnotherCourse";
    }

    public static class CourseRoles
    {
        public const string NotFound = "CourseRoles.NotFound";
        public const string NameNotUnique = "CourseRoles.NameNotUnique";
    }

    public static class CourseTabs
    {
        public const string NotFound = "CourseTabs.NotFound";
    }

    public static class Files
    {
        public const string Empty = "Files.Empty";
        public const string SizeLimit = "Files.SizeLimit";
        public const string InvalidImage = "Files.InvalidImage";
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

    public static class Students
    {
        public const string NotFound = "Students.NotFound";
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
