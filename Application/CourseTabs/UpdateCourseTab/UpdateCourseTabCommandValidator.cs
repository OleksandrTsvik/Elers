using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Rules;
using FluentValidation;

namespace Application.CourseTabs.UpdateCourseTab;

public class UpdateCourseTabCommandValidator : AbstractValidator<UpdateCourseTabCommand>
{
    public UpdateCourseTabCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.TabId).NotEmpty();

        RuleFor(x => x.Name)
            .Length(CourseTabRules.MinNameLength, CourseTabRules.MaxNameLength)
                .When(x => !string.IsNullOrEmpty(x.Name))
            .TrimWhitespace(translator);
    }
}
