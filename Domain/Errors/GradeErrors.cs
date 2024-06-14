using Domain.Constants;
using Domain.Shared;

namespace Domain.Errors;

public static class GradeErrors
{
    public static Error NotFound(Guid gradeId) => Error.NotFound(
        ErrorCodes.Grades.NotFound,
        $"Grade with the Id = '{gradeId}' was not found.", gradeId);

    public static Error GradeLimit(int maxGrade) => Error.Validation(
        ErrorCodes.Grades.GradeLimit,
        $"You can't give a grade greater than the maximum: {maxGrade}.", maxGrade);

    public static Error AccessDenied() => Error.Forbidden(
        ErrorCodes.Grades.AccessDenied,
        "You do not have permission to grade.");

    public static Error StudentsOnly() => Error.Validation(
        ErrorCodes.Grades.StudentsOnly,
        "Only students of the course can be given a grade!");

    public static Error AlreadyExists() => Error.Validation(
        ErrorCodes.Grades.AlreadyExists,
        "The grade has already been created.");
}
