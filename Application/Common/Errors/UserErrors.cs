using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Errors;
using Domain.Shared;

namespace Application.Common.Errors;

public class UserErrors : IUserErrors
{
    private readonly IErrorGenerator _errorGenerator;

    public UserErrors(IErrorGenerator errorGenerator)
    {
        _errorGenerator = errorGenerator;
    }

    public Error NotFound(Guid userId) => _errorGenerator.GetErrorByCode(
        ErrorType.NotFound, ErrorCodes.User.NotFound, userId);

    public Error NotFoundByEmail(string email) => _errorGenerator.GetErrorByCode(
        ErrorType.NotFound, ErrorCodes.User.NotFoundByEmail, email);

    public Error EmailNotUnique() => _errorGenerator.GetErrorByCode(
        ErrorType.Conflict, ErrorCodes.User.EmailNotUnique);

    public Error InvalidCredentials() => _errorGenerator.GetErrorByCode(
        ErrorType.Unauthorized, ErrorCodes.User.InvalidCredentials);

    public Error NotFoundByUserContext() => _errorGenerator.GetErrorByCode(
        ErrorType.Unauthorized, ErrorCodes.User.NotFoundByUserContext);
}
