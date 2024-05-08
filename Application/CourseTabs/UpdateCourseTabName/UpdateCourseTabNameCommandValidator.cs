using Domain.Rules;
using FluentValidation;

namespace Application.CourseTabs.UpdateCourseTabName;

public class UpdateCourseTabNameCommandValidator : AbstractValidator<UpdateCourseTabNameCommand>
{
    public UpdateCourseTabNameCommandValidator()
    {
        RuleFor(x => x.TabId).NotEmpty();

        RuleFor(x => x.Name)
            .MinimumLength(CourseTabRules.MinNameLength)
            .MaximumLength(CourseTabRules.MaxNameLength);
    }
}
