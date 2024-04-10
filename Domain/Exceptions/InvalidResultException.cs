using Domain.Constants;

namespace Domain.Exceptions;

public class InvalidResultException : ArgumentException
{
    public InvalidResultException(string paramName)
        : base(ExceptionMessages.InvalidResult, paramName)
    {
    }
}
