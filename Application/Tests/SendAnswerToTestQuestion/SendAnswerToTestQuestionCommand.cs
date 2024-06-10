using Application.Common.Messaging;

namespace Application.Tests.SendAnswerToTestQuestion;

public record SendAnswerToTestQuestionCommand(
    Guid TestSessionId,
    Guid TestQuestionId,
    string? Answer,
    List<string>? Answers) : ICommand;
