using Domain.Constants;
using Domain.Shared;

namespace Domain.Errors;

public static class AssignmentErrors
{
    public static Error NotFound(Guid assignmentId) => Error.NotFound(
        ErrorCodes.Assignments.NotFound,
        $"The assignment with the Id = '{assignmentId}' was not found.", assignmentId);

    public static Error SubmittedNotFound(Guid submittedAssignmentId) => Error.NotFound(
        ErrorCodes.Assignments.SubmittedNotFound,
        $"The submitted completed assignment with the Id = '{submittedAssignmentId}' was not found.",
        submittedAssignmentId);

    public static Error Unavailable(Guid assignmentId) => Error.Forbidden(
        ErrorCodes.Assignments.Unavailable,
        $"The assignment with the Id = '{assignmentId}' is not available.", assignmentId);

    public static Error StudentsOnly() => Error.Forbidden(
        ErrorCodes.Assignments.StudentsOnly,
        "Only students of the course can submit assignments!");

    public static Error EmptyFields() => Error.Validation(
        ErrorCodes.Assignments.EmptyFields,
        "Add text or files!");

    public static Error ManyFiles(int maxNumberFiles) => Error.Validation(
        ErrorCodes.Assignments.ManyFiles,
        $"You can upload a maximum of {maxNumberFiles} files.", maxNumberFiles);

    public static Error AlreadyGraded() => Error.Forbidden(
        ErrorCodes.Assignments.AlreadyGraded,
        "This assignment has already been graded and cannot be resubmitted.");

    public static Error DeadlinePassed() => Error.Forbidden(
        ErrorCodes.Assignments.DeadlinePassed,
        "The submission deadline has passed and your assignment cannot be accepted.");

    public static Error FileNotFound() => Error.NotFound(
        ErrorCodes.Assignments.FileNotFound,
        "File not found.");

    public static Error FileAccessDenied() => Error.Forbidden(
        ErrorCodes.Assignments.FileAccessDenied,
        "You do not have permission to download this file.");

    public static Error SubmittedAccessDenied() => Error.Forbidden(
        ErrorCodes.Assignments.SubmittedAccessDenied,
        "You do not have permission to view this submitted assignment.");

    public static Error GradeAccessDenied() => Error.Forbidden(
        ErrorCodes.Assignments.GradeAccessDenied,
        "You do not have permission to grade this assignment.");

    public static Error NotActive() => Error.Forbidden(
        ErrorCodes.Assignments.NotActive,
        "The assignment is not active.");
}
