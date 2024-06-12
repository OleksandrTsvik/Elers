using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Rules;
using FluentValidation;

namespace Application.TestQuestions.CreateTestQuestionMatching;

public class CreateTestQuestionMatchingCommandValidator : AbstractValidator<CreateTestQuestionMatchingCommand>
{
    public CreateTestQuestionMatchingCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.TestId).NotEmpty();

        RuleFor(x => x.Text)
            .NotEmpty()
            .TrimWhitespace(translator);

        RuleFor(x => x.Points)
            .NotEmpty()
            .GreaterThanOrEqualTo(TestQuestionRules.MinPoints);

        RuleForEach(x => x.Options).ChildRules(option =>
        {
            option.RuleFor(x => x.Answer).NotEmpty();
        });

        RuleFor(x => x.Options)
            .Must(x => x.Count >= 2 &&
                x.Where(option => !string.IsNullOrWhiteSpace(option.Question)).Count() >= 2)
            .WithMessage(translator.GetString(ValidationMessages.TestQuestions.AddTwoMatching));
    }
}
