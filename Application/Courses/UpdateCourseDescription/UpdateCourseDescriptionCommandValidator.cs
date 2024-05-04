using Domain.Rules;
using FluentValidation;

namespace Application.Courses.UpdateCourseDescription;

public class UpdateCourseDescriptionCommandValidator : AbstractValidator<UpdateCourseDescriptionCommand>
{
    public UpdateCourseDescriptionCommandValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(CourseRules.MaxDescriptionLength)
                .When(x => !string.IsNullOrEmpty(x.Description));
    }
}
