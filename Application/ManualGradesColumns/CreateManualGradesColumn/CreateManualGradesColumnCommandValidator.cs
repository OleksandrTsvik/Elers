using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Rules;
using FluentValidation;

namespace Application.ManualGradesColumns.CreateManualGradesColumn;

public class CreateManualGradesColumnCommandValidator : AbstractValidator<CreateManualGradesColumnCommand>
{
    public CreateManualGradesColumnCommandValidator(ITranslator translator, IAppVariables appVariables)
    {
        RuleFor(x => x.CourseId).NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(ManualGradesColumnRules.MinTitleLength)
            .MaximumLength(ManualGradesColumnRules.MaxTitleLength)
            .TrimWhitespace(translator);

        RuleFor(x => x.MaxGrade)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(appVariables.MaxManualGrade);
    }
}
