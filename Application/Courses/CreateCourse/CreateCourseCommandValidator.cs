using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Rules;
using FluentValidation;

namespace Application.Courses.CreateCourse;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.Title)
            .MinimumLength(CourseRules.MinTitleLength)
            .MaximumLength(CourseRules.MaxTitleLength)
            .TrimWhitespace(translator);

        RuleFor(x => x.Description)
            .MaximumLength(CourseRules.MaxDescriptionLength)
                .When(x => !string.IsNullOrEmpty(x.Description))
            .TrimWhitespace(translator);
    }
}
