using FluentValidation;

namespace API.Options.AppVars;

public class AppVariablesValidator : AbstractValidator<AppVariables>
{
    public AppVariablesValidator()
    {
        RuleFor(x => x.MaxAssignmentGrade)
            .GreaterThan(0)
            .LessThanOrEqualTo(1000);

        RuleFor(x => x.MaxFilesStudentUploadAssignment)
            .GreaterThan(0)
            .LessThanOrEqualTo(25);
    }
}
