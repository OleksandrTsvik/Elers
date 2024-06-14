using FluentValidation;

namespace Application.Grades.UpdateGread;

public class UpdateGreadCommandValidator : AbstractValidator<UpdateGreadCommand>
{
    public UpdateGreadCommandValidator()
    {
        RuleFor(x => x.GradeId).NotEmpty();

        RuleFor(x => x.Value).GreaterThanOrEqualTo(0);
    }
}
