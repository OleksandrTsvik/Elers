using Domain.Rules;
using FluentValidation;

namespace Application.Profile.ChangeCurrentUserPassword;

public class ChangeCurrentUserPasswordCommandValidator : AbstractValidator<ChangeCurrentUserPasswordCommand>
{
    public ChangeCurrentUserPasswordCommandValidator()
    {
        RuleFor(x => x.NewPassword)
            .MinimumLength(UserRules.MinPasswordLength)
            .MaximumLength(UserRules.MaxPasswordLength);

        RuleFor(x => x.NewPassword).Equal(x => x.ConfirmPassword);
    }
}
