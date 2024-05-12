using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Rules;
using FluentValidation;

namespace Application.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .MaximumLength(UserRules.MaxEmailLength)
            .TrimWhitespace(translator);

        RuleFor(x => x.Password)
            .MinimumLength(UserRules.MinPasswordLength)
            .MaximumLength(UserRules.MaxPasswordLength);

        RuleFor(x => x.FirstName)
            .Length(UserRules.MinFirstNameLength, UserRules.MaxFirstNameLength)
                .When(x => !string.IsNullOrEmpty(x.FirstName))
            .TrimWhitespace(translator);

        RuleFor(x => x.LastName)
            .Length(UserRules.MinLastNameLength, UserRules.MaxLastNameLength)
                .When(x => !string.IsNullOrEmpty(x.LastName))
            .TrimWhitespace(translator);

        RuleFor(x => x.Patronymic)
            .Length(UserRules.MinPatronymicLength, UserRules.MaxPatronymicLength)
                .When(x => !string.IsNullOrEmpty(x.Patronymic))
            .TrimWhitespace(translator);

        RuleForEach(x => x.RoleIds).NotEmpty();
    }
}
