using Domain.Rules;
using FluentValidation;

namespace Application.Assignments.GradeAssignment;

public class GradeAssignmentCommandValidator : AbstractValidator<GradeAssignmentCommand>
{
    public GradeAssignmentCommandValidator()
    {
        RuleFor(x => x.SubmittedAssignmentId).NotEmpty();

        RuleFor(x => x.Status).IsInEnum();

        RuleFor(x => x.Grade).GreaterThanOrEqualTo(0);

        RuleFor(x => x.Comment)
            .MaximumLength(AssignmentRules.MaxCommentLength)
                .When(x => !string.IsNullOrEmpty(x.Comment));
    }
}
