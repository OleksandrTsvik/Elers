using Application.Common.Interfaces;
using Domain.Constants;
using FluentValidation;

namespace Application.Common.Extensions;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, string?> TrimWhitespace<T>(
        this IRuleBuilder<T, string?> ruleBuilder,
        ITranslator translator)
    {
        return ruleBuilder
            .Must(value => value is null || value.Length == value.Trim().Length)
            .WithMessage(translator.GetString(ValidationMessages.TrimWhitespace));
    }
}
