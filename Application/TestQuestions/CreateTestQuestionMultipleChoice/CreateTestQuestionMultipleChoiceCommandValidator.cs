using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Rules;
using FluentValidation;

namespace Application.TestQuestions.CreateTestQuestionMultipleChoice;

public class CreateTestQuestionMultipleChoiceCommandValidator
    : AbstractValidator<CreateTestQuestionMultipleChoiceCommand>
{
    public CreateTestQuestionMultipleChoiceCommandValidator(ITranslator translator)
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
            option.RuleFor(x => x.Option).NotEmpty();
        });

        RuleFor(x => x.Options)
            .Must(x => x.Count() >= 3)
            .WithMessage(translator.GetString(ValidationMessages.TestQuestions.AddThreeAnswers));

        RuleFor(x => x.Options)
            .Must(x => x.Where(option => option.IsCorrect).Count() > 1)
            .WithMessage(translator.GetString(
                ValidationMessages.TestQuestions.ChooseMoreThanOneCorrectAnswer));
    }
}
