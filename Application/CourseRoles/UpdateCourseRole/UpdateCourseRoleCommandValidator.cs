using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Rules;
using FluentValidation;

namespace Application.CourseRoles.UpdateCourseRole;

public class UpdateCourseRoleCommandValidator : AbstractValidator<UpdateCourseRoleCommand>
{
    public UpdateCourseRoleCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.RoleId).NotEmpty();

        RuleFor(x => x.Name)
            .MinimumLength(CourseRoleRules.MinNameLength)
            .MaximumLength(CourseRoleRules.MaxNameLength)
            .TrimWhitespace(translator);

        RuleForEach(x => x.PermissionIds).NotEmpty();
    }
}
