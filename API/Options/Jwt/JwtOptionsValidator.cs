using FluentValidation;
using Infrastructure.Authentication;

namespace API.Options.Jwt;

public class JwtOptionsValidator : AbstractValidator<JwtOptions>
{
    public JwtOptionsValidator()
    {
        RuleFor(x => x.Issuer).NotEmpty();

        RuleFor(x => x.Audience).NotEmpty();

        RuleFor(x => x.ExpirationInMinutes)
            .GreaterThan(0)
            .LessThan(60 * 24 * 30);

        RuleFor(x => x.SecretKey)
            .NotEmpty()
            .MinimumLength(32);
    }
}
