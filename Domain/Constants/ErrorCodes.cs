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
        public const string NotActive = "Assignments.NotActive";
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
        public const string AccessDenied = "CourseMaterials.AccessDenied";
        public const string CrossCourseMove = "CourseMaterials.CrossCourseMove";
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
        public const string AccessDenied = "CourseTabs.AccessDenied";
    }

    public static class Files
    {
        public const string Empty = "Files.Empty";
        public const string SizeLimit = "Files.SizeLimit";
        public const string InvalidImage = "Files.InvalidImage";
    }

    public static class Grades
    {
        public const string NotFound = "Grades.NotFound";
        public const string GradeLimit = "Grades.GradeLimit";
        public const string AccessDenied = "Grades.AccessDenied";
        public const string StudentsOnly = "Grades.StudentsOnly";
        public const string AlreadyExists = "Grades.AlreadyExists";
    }

    public static class ManualGradesColumns
    {
        public const string NotFound = "ManualGradesColumns.NotFound";
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

    public static class Tests
    {
        public const string NotFound = "Tests.NotFound";
        public const string NoQuestions = "Tests.NoQuestions";
        public const string NotFoundAnswer = "Tests.NotFoundAnswer";
        public const string InvalidSessionAnswer = "Tests.InvalidSessionAnswer";
        public const string UserSessionNotFound = "Tests.UserSessionNotFound";
        public const string StudentsOnly = "Tests.StudentsOnly";
        public const string NotActive = "Tests.NotActive";
        public const string DeadlinePassed = "Tests.DeadlinePassed";
        public const string AttemptsExceeded = "Tests.AttemptsExceeded";
        public const string UnfinishedAttempts = "Tests.UnfinishedAttempts";
        public const string AttemptExpired = "Tests.AttemptExpired";
        public const string AttemptAlreadyCompleted = "Tests.AttemptAlreadyCompleted";
    }

    public static class TestQuestions
    {
        public const string NotFound = "TestQuestions.NotFound";
    }

    public static class Users
    {
        public const string NotFound = "Users.NotFound";
        public const string NotFoundByEmail = "Users.NotFoundByEmail";
        public const string EmailNotUnique = "Users.EmailNotUnique";
        public const string InvalidCredentials = "Users.InvalidCredentials";
        public const string NotFoundByUserContext = "Users.NotFoundByUserContext";
        public const string Unauthorized = "Users.Unauthorized";
        public const string InvalidPassword = "Users.InvalidPassword";
    }
}
