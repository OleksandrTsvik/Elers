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

        RuleForEach(x => x.RoleIds).NotEmpty();
    }
}
