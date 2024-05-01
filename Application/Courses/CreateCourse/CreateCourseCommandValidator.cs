using Domain.Rules;
using FluentValidation;

namespace Application.Courses.CreateCourse;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(CourseRules.MinTitleLength)
            .MaximumLength(CourseRules.MaxTitleLength);

        RuleFor(x => x.Description)
            .MaximumLength(CourseRules.MaxDescriptionLength)
                .When(x => !string.IsNullOrEmpty(x.Description));
    }
}
