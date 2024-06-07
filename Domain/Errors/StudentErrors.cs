using Domain.Constants;
using Domain.Shared;

namespace Domain.Errors;

public static class StudentErrors
{
    public static Error NotFound(Guid studentId) => Error.NotFound(
        ErrorCodes.Students.NotFound,
        $"The student with the Id = '{studentId}' was not found.", studentId);
}
