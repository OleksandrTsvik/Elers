using FluentValidation;

namespace Application.Tests.SendAnswerToTestQuestion;

public class SendAnswerToTestQuestionCommandValidator : AbstractValidator<SendAnswerToTestQuestionCommand>
{
    public SendAnswerToTestQuestionCommandValidator()
    {
        RuleFor(x => x.TestSessionId).NotEmpty();

        RuleFor(x => x.TestQuestionId).NotEmpty();
    }
}
