using FluentValidation;

namespace Application.Auth.UpdateToken;

public class UpdateTokenCommandValidator : AbstractValidator<UpdateTokenCommand>
{
    public UpdateTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}
