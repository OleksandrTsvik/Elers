using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Rules;
using FluentValidation;

namespace Application.ManualGradesColumns.UpdateManualGradesColumn;

public class UpdateManualGradesColumnCommandValidator : AbstractValidator<UpdateManualGradesColumnCommand>
{
    public UpdateManualGradesColumnCommandValidator(ITranslator translator, IAppVariables appVariables)
    {
        RuleFor(x => x.Id).NotEmpty();

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
