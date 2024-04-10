using Domain.Constants;

namespace Domain.Exceptions;

public class ResultValueAccessException : InvalidOperationException
{
    public ResultValueAccessException()
        : base(ExceptionMessages.ResultValueAccess)
    {
    }
}
