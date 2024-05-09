using Domain.Rules;
using FluentValidation;

namespace Application.CourseTabs.UpdateCourseTabColor;

public class UpdateCourseTabColorCommandValidator : AbstractValidator<UpdateCourseTabColorCommand>
{
    public UpdateCourseTabColorCommandValidator()
    {
        RuleFor(x => x.TabId).NotEmpty();

        RuleFor(x => x.Color)
            .MaximumLength(CourseTabRules.MaxColorLength)
                .When(x => !string.IsNullOrEmpty(x.Color));
    }
}
