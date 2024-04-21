using Domain.Rules;
using FluentValidation;

namespace Application.Roles.UpdateRole;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(x => x.RoleId).NotEmpty();

        RuleFor(x => x.Name)
            .MinimumLength(RoleRules.MinNameLength)
            .MaximumLength(RoleRules.MaxNameLength);

        RuleForEach(x => x.PermissionIds).NotEmpty();
    }
}
