using Domain.Constants;
using Domain.Shared;

namespace Domain.Exceptions;

public class AppValidationException : Exception
{
    public IReadOnlyCollection<ValidationError> Errors { get; }

    public AppValidationException(IReadOnlyCollection<ValidationError> errors)
        : base(ExceptionMessages.Validation)
    {
        Errors = errors;
    }
}
