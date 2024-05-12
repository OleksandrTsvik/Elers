using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Rules;
using FluentValidation;

namespace Application.Courses.UpdateCourseDescription;

public class UpdateCourseDescriptionCommandValidator : AbstractValidator<UpdateCourseDescriptionCommand>
{
    public UpdateCourseDescriptionCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.CourseId).NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(CourseRules.MaxDescriptionLength)
                .When(x => !string.IsNullOrEmpty(x.Description))
            .TrimWhitespace(translator);
    }
}
