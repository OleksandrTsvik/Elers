using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Errors;
using Domain.Shared;

namespace Application.Common.Errors;

public class DefaultErrors : IDefaultErrors
{
    private readonly IErrorGenerator _errorGenerator;

    public DefaultErrors(IErrorGenerator errorGenerator)
    {
        _errorGenerator = errorGenerator;
    }

    public Error NullValue() => _errorGenerator.GetErrorByCode(
        ErrorType.Failure, ErrorCodes.Default.NullValue);

    public Error NullResult() => _errorGenerator.GetErrorByCode(
        ErrorType.Failure, ErrorCodes.Default.NullResult);
}
