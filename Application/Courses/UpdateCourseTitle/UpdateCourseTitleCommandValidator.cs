using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Rules;
using FluentValidation;

namespace Application.Courses.UpdateCourseTitle;

public class UpdateCourseTitleCommandValidator : AbstractValidator<UpdateCourseTitleCommand>
{
    public UpdateCourseTitleCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.CourseId).NotEmpty();

        RuleFor(x => x.Title)
            .MinimumLength(CourseRules.MinTitleLength)
            .MaximumLength(CourseRules.MaxTitleLength)
            .TrimWhitespace(translator);
    }
}
