using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Rules;
using FluentValidation;

namespace Application.Users.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.UserId).NotEmpty();

        RuleFor(x => x.Email)
            .EmailAddress()
            .MaximumLength(UserRules.MaxEmailLength)
            .TrimWhitespace(translator);

        RuleFor(x => x.Password)
            .Length(UserRules.MinPasswordLength, UserRules.MaxPasswordLength)
                .When(x => !string.IsNullOrEmpty(x.Password));

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
