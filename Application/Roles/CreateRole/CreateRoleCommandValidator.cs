using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Rules;
using FluentValidation;

namespace Application.Roles.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.Name)
            .MinimumLength(RoleRules.MinNameLength)
            .MaximumLength(RoleRules.MaxNameLength)
            .TrimWhitespace(translator);

        RuleForEach(x => x.PermissionIds).NotEmpty();
    }
}
