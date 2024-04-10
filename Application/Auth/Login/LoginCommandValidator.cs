using FluentValidation;

namespace Application.Auth.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress();

        RuleFor(x => x.Password).MinimumLength(6).MaximumLength(32);
    }
}
