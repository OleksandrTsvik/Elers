using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Rules;
using FluentValidation;

namespace Application.CourseTabs.CreateCourseTab;

public class CreateCourseTabCommandValidator : AbstractValidator<CreateCourseTabCommand>
{
    public CreateCourseTabCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.CourseId).NotEmpty();

        RuleFor(x => x.Name)
            .MinimumLength(CourseTabRules.MinNameLength)
            .MaximumLength(CourseTabRules.MaxNameLength)
            .TrimWhitespace(translator);
    }
}
