using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Errors;
using Domain.Shared;

namespace Application.Common.Errors;

public class RefreshTokenErrors : IRefreshTokenErrors
{
    private readonly IErrorGenerator _errorGenerator;

    public RefreshTokenErrors(IErrorGenerator errorGenerator)
    {
        _errorGenerator = errorGenerator;
    }

    public Error InvalidToken() => _errorGenerator.GetErrorByCode(
        ErrorType.Unauthorized, ErrorCodes.RefreshToken.InvalidToken);
}
