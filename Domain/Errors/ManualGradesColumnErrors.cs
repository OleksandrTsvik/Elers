using Domain.Constants;
using Domain.Shared;

namespace Domain.Errors;

public static class ManualGradesColumnErrors
{
    public static Error NotFound(Guid columnId) => Error.NotFound(
        ErrorCodes.ManualGradesColumns.NotFound,
        $"The grade column with the Id = '{columnId}' was not found.", columnId);
}
