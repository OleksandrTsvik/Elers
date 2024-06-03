using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Rules;
using FluentValidation;

namespace Application.CourseMaterials.CreateCourseMaterialAssignment;

public class CreateCourseMaterialAssignmentCommandValidator
    : AbstractValidator<CreateCourseMaterialAssignmentCommand>
{
    public CreateCourseMaterialAssignmentCommandValidator(ITranslator translator, IAppVariables appVariables)
    {
        RuleFor(x => x.TabId).NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(CourseMaterialRules.MinTitleAssignmentLength)
            .MaximumLength(CourseMaterialRules.MaxTitleAssignmentLength)
            .TrimWhitespace(translator);

        RuleFor(x => x.Description).NotEmpty();

        RuleFor(x => x.MaxFiles)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(appVariables.MaxFilesStudentUploadAssignment);

        RuleFor(x => x.MaxGrade)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(appVariables.MaxAssignmentGrade);
    }
}
