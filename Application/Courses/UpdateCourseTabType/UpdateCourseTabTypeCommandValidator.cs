using FluentValidation;

namespace Application.Courses.UpdateCourseTabType;

public class UpdateCourseTabTypeCommandValidator : AbstractValidator<UpdateCourseTabTypeCommand>
{
    public UpdateCourseTabTypeCommandValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty();

        RuleFor(x => x.TabType).IsInEnum();
    }
}
