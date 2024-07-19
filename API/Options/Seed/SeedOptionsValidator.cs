using Domain.Rules;
using FluentValidation;
using Persistence.Options;

namespace Api.Options.Seed;

public class SeedOptionsValidator : AbstractValidator<SeedOptions>
{
    public SeedOptionsValidator()
    {
        RuleFor(x => x.AdminEmail).EmailAddress();

        RuleFor(x => x.AdminPassword)
            .MinimumLength(UserRules.MinPasswordLength)
            .MaximumLength(UserRules.MaxPasswordLength);
    }
}
