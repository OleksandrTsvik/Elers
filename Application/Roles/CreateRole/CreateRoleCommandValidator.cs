using Domain.Rules;
using FluentValidation;

namespace Application.Roles.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(RoleRules.MinNameLength)
            .MaximumLength(RoleRules.MaxNameLength);

        RuleForEach(x => x.PermissionIds).NotEmpty();
    }
}
