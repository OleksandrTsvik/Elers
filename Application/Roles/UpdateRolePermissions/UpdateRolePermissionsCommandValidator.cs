using FluentValidation;

namespace Application.Roles.UpdateRolePermissions;

public class UpdateRolePermissionsCommandValidator : AbstractValidator<UpdateRolePermissionsCommand>
{
    public UpdateRolePermissionsCommandValidator()
    {
        RuleFor(x => x.RoleId).NotEmpty();

        RuleForEach(x => x.PermissionIds).NotEmpty();
    }
}
