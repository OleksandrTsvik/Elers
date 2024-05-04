using Domain.Rules;
using FluentValidation;

namespace Application.Courses.UpdateCourseTitle;

public class UpdateCourseTitleCommandValidator : AbstractValidator<UpdateCourseTitleCommand>
{
    public UpdateCourseTitleCommandValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty();

        RuleFor(x => x.Title)
            .MinimumLength(CourseRules.MinTitleLength)
            .MaximumLength(CourseRules.MaxTitleLength);
    }
}
