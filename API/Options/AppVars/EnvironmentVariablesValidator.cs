using FluentValidation;

namespace Api.Options.AppVars;

public class AppVariablesValidator : AbstractValidator<AppVariables>
{
    public AppVariablesValidator()
    {
        RuleFor(x => x.MaxManualGrade)
            .GreaterThan(0)
            .LessThanOrEqualTo(200);

        RuleFor(x => x.MaxAssignmentGrade)
            .GreaterThan(0)
            .LessThanOrEqualTo(1000);

        RuleFor(x => x.MaxFilesStudentUploadAssignment)
            .GreaterThan(0)
            .LessThanOrEqualTo(25);
    }
}
