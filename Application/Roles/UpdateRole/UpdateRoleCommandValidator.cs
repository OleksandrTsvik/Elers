using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Rules;
using FluentValidation;

namespace Application.Roles.UpdateRole;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.RoleId).NotEmpty();

        RuleFor(x => x.Name)
            .MinimumLength(RoleRules.MinNameLength)
            .MaximumLength(RoleRules.MaxNameLength)
            .TrimWhitespace(translator);

        RuleForEach(x => x.PermissionIds).NotEmpty();
    }
}
