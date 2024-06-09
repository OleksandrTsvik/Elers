using Domain.Shared;

namespace Domain.Errors;

public static class TestQuestionErrors
{
    public static Error NotFound(Guid testQuestionId) => Error.NotFound(
        Constants.ErrorCodes.TestQuestions.NotFound,
        $"Test question with Id = '{testQuestionId}' not found.", testQuestionId);
}
