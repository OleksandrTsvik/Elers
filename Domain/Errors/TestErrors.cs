using Domain.Constants;
using Domain.Shared;

namespace Domain.Errors;

public static class TestErrors
{
    public static Error NotFound(Guid testId) => Error.NotFound(
        ErrorCodes.Tests.NotFound,
        $"Test with Id = '{testId}' not found.", testId);

    public static Error NoQuestions(Guid testId) => Error.Validation(
        ErrorCodes.Tests.NoQuestions,
        $"There are no questions in the test with Id = '{testId}'.", testId);

    public static Error NotFoundAnswer() => Error.NotFound(
        ErrorCodes.Tests.NotFoundAnswer,
        "The answer to the question was not found.");

    public static Error InvalidSessionAnswer() => Error.Failure(
        ErrorCodes.Tests.InvalidSessionAnswer,
        "The question type does not match the answer type.");

    public static Error UserSessionNotFound() => Error.NotFound(
        ErrorCodes.Tests.UserSessionNotFound,
        "Test session not found for current user.");

    public static Error StudentsOnly() => Error.Forbidden(
        ErrorCodes.Tests.StudentsOnly,
        "Only students of the course can take the tests!");

    public static Error NotActive() => Error.Forbidden(
        ErrorCodes.Tests.NotActive,
        "The test is not active.");

    public static Error DeadlinePassed() => Error.Forbidden(
        ErrorCodes.Tests.DeadlinePassed,
        "The test deadline has passed.");

    public static Error AttemptsExceeded() => Error.Forbidden(
        ErrorCodes.Tests.AttemptsExceeded,
        "Attempt limit exceeded.");

    public static Error UnfinishedAttempts() => Error.Forbidden(
        ErrorCodes.Tests.UnfinishedAttempts,
        "You cannot start a new test attempt until the previous attempt is completed.");

    public static Error AttemptExpired() => Error.Forbidden(
        ErrorCodes.Tests.AttemptExpired,
        "Unable to submit the answer. Your attempt has expired.");

    public static Error AttemptAlreadyCompleted() => Error.Conflict(
        ErrorCodes.Tests.AttemptAlreadyCompleted,
        "You cannot complete the test attempt because it has already been completed.");
}
