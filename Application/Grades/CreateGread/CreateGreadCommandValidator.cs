using FluentValidation;

namespace Application.Grades.CreateGread;

public class CreateGreadCommandValidator : AbstractValidator<CreateGreadCommand>
{
    public CreateGreadCommandValidator()
    {
        RuleFor(x => x.StudentId).NotEmpty();

        RuleFor(x => x.AssessmentId).NotEmpty();

        RuleFor(x => x.GradeType).IsInEnum();

        RuleFor(x => x.Value).GreaterThanOrEqualTo(0);
    }
}
