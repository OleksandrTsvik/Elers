using FluentValidation;
using Persistence.Common;

namespace API.Options.ConnectionStrings;

public class ConnectionStringsOptionsValidator : AbstractValidator<ConnectionStringsOptions>
{
    public ConnectionStringsOptionsValidator()
    {
        RuleFor(x => x.DefaultConnection).NotEmpty();
    }
}
