using Domain.Shared;

namespace Application.Common.Interfaces;

public interface IErrorGenerator
{
    Error GetError(ErrorType type, string code, string description);
    Error GetErrorByCode(ErrorType type, string code);
    Error GetErrorByCode(ErrorType type, string code, params object[] arguments);
}
