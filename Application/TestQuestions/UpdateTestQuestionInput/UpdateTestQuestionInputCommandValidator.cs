using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Rules;
using FluentValidation;

namespace Application.TestQuestions.UpdateTestQuestionInput;

public class UpdateTestQuestionInputCommandValidator : AbstractValidator<UpdateTestQuestionInputCommand>
{
    public UpdateTestQuestionInputCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.TestQuestionId).NotEmpty();

        RuleFor(x => x.Text)
            .NotEmpty()
            .TrimWhitespace(translator);

        RuleFor(x => x.Points)
            .NotEmpty()
            .GreaterThanOrEqualTo(TestQuestionRules.MinPoints);

        RuleFor(x => x.Answer)
            .NotEmpty()
            .TrimWhitespace(translator);
    }
}
