using Domain.Constants;
using Domain.Shared;

namespace Domain.Errors;

public static class TestErrors
{
    public static Error NotFound(Guid testId) => Error.NotFound(
        ErrorCodes.Tests.NotFound,
        $"Test with Id = '{testId}' not found.", testId);
}
