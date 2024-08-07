using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Rules;
using FluentValidation;

namespace Application.CourseMaterials.UpdateCourseMaterialAssignment;

public class UpdateCourseMaterialAssignmentCommandValidator
    : AbstractValidator<UpdateCourseMaterialAssignmentCommand>
{
    public UpdateCourseMaterialAssignmentCommandValidator(ITranslator translator, IAppVariables appVariables)
    {
        RuleFor(x => x.MaterialId).NotEmpty();

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
