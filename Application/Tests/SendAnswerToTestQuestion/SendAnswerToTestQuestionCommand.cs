using Application.Common.Messaging;
using Domain.Entities;

namespace Application.Tests.SendAnswerToTestQuestion;

public record SendAnswerToTestQuestionCommand(
    Guid TestSessionId,
    Guid TestQuestionId,
    string? Answer,
    List<string>? Answers,
    List<AnswerMatchOption>? MatchOptions) : ICommand;
