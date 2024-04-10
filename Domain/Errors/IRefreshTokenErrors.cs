using Domain.Shared;

namespace Domain.Errors;

public interface IRefreshTokenErrors
{
    Error InvalidToken();
}
