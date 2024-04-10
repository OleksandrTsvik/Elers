using Domain.Shared;

namespace Domain.Errors;

public interface IUserErrors
{
    Error NotFound(Guid userId);
    Error NotFoundByEmail(string email);
    Error EmailNotUnique();
    Error InvalidCredentials();
    Error NotFoundByUserContext();
}
