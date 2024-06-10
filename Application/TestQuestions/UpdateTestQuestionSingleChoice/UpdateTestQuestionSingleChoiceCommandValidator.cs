using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Rules;
using FluentValidation;

namespace Application.TestQuestions.UpdateTestQuestionSingleChoice;

public class UpdateTestQuestionSingleChoiceCommandValidator
    : AbstractValidator<UpdateTestQuestionSingleChoiceCommand>
{
    public UpdateTestQuestionSingleChoiceCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.TestQuestionId).NotEmpty();

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
            .Must(x => x.Count() >= 2)
            .WithMessage(translator.GetString(ValidationMessages.TestQuestions.AddTwoAnswers));

        RuleFor(x => x.Options)
            .Must(x => x.Where(option => option.IsCorrect).Count() == 1)
            .WithMessage(translator.GetString(ValidationMessages.TestQuestions.ChooseOneCorrectAnswer));
    }
}
