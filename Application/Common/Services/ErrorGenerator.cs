using Application.Common.Interfaces;
using Domain.Shared;

namespace Application.Common.Services;

public class ErrorGenerator : IErrorGenerator
{
    private readonly ITranslator _translator;

    public ErrorGenerator(ITranslator translator)
    {
        _translator = translator;
    }

    public Error GetError(ErrorType type, string code, string description)
        => Error.Create(type, code, description);

    public Error GetErrorByCode(ErrorType type, string code)
        => Error.Create(type, code, _translator.GetString(code));

    public Error GetErrorByCode(ErrorType type, string code, params object[] arguments)
        => Error.Create(type, code, _translator.GetString(code, arguments));
}
