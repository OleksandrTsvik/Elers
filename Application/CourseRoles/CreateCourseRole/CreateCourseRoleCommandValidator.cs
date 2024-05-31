using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Rules;
using FluentValidation;

namespace Application.CourseRoles.CreateCourseRole;

public class CreateCourseRoleCommandValidator : AbstractValidator<CreateCourseRoleCommand>
{
    public CreateCourseRoleCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.CourseId).NotEmpty();

        RuleFor(x => x.Name)
            .MinimumLength(CourseRoleRules.MinNameLength)
            .MaximumLength(CourseRoleRules.MaxNameLength)
            .TrimWhitespace(translator);

        RuleForEach(x => x.PermissionIds).NotEmpty();
    }
}
