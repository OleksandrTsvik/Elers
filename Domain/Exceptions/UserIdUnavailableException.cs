using Domain.Constants;

namespace Domain.Exceptions;

public class UserIdUnavailableException : Exception
{
    public UserIdUnavailableException()
        : base(ExceptionMessages.UserIdUnavailable)
    {
    }
}
