using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Rules;
using FluentValidation;

namespace Application.Profile.UpdateCurrentProfile;

public class UpdateCurrentProfileCommandValidator : AbstractValidator<UpdateCurrentProfileCommand>
{
    public UpdateCurrentProfileCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .MaximumLength(UserRules.MaxEmailLength)
            .TrimWhitespace(translator);

        RuleFor(x => x.FirstName)
            .MinimumLength(UserRules.MinFirstNameLength)
            .MaximumLength(UserRules.MaxFirstNameLength)
            .TrimWhitespace(translator);

        RuleFor(x => x.LastName)
            .MinimumLength(UserRules.MinLastNameLength)
            .MaximumLength(UserRules.MaxLastNameLength)
            .TrimWhitespace(translator);

        RuleFor(x => x.Patronymic)
            .MinimumLength(UserRules.MinPatronymicLength)
            .MaximumLength(UserRules.MaxPatronymicLength)
            .TrimWhitespace(translator);
    }
}
