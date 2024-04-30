using Domain.Rules;
using FluentValidation;

namespace Application.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .MaximumLength(UserRules.MaxEmailLength);

        RuleFor(x => x.Password)
            .MinimumLength(UserRules.MinPasswordLength)
            .MaximumLength(UserRules.MaxPasswordLength);

        RuleFor(x => x.FirstName)
            .Length(UserRules.MinFirstNameLength, UserRules.MaxFirstNameLength)
                .When(x => !string.IsNullOrEmpty(x.FirstName));

        RuleFor(x => x.LastName)
            .Length(UserRules.MinLastNameLength, UserRules.MaxLastNameLength)
                .When(x => !string.IsNullOrEmpty(x.LastName));

        RuleFor(x => x.Patronymic)
            .Length(UserRules.MinPatronymicLength, UserRules.MaxPatronymicLength)
                .When(x => !string.IsNullOrEmpty(x.Patronymic));

        RuleForEach(x => x.RoleIds).NotEmpty();
    }
}
