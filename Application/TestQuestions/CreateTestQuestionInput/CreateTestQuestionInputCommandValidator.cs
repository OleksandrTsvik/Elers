using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Rules;
using FluentValidation;

namespace Application.TestQuestions.CreateTestQuestionInput;

public class CreateTestQuestionInputCommandValidator : AbstractValidator<CreateTestQuestionInputCommand>
{
    public CreateTestQuestionInputCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.TestId).NotEmpty();

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
