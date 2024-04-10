using Application.Common.Interfaces;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Localization;

public class Translator : ITranslator
{
    private readonly IStringLocalizer _localizer;

    public Translator(IStringLocalizer<Translator> localizer)
    {
        _localizer = localizer;
    }

    public string GetString(string name) => _localizer[name];

    public string GetString(string name, params object[] arguments)
        => _localizer[name, arguments];
}
