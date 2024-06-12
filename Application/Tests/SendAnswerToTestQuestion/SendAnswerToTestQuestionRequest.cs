using Domain.Entities;

namespace Application.Tests.SendAnswerToTestQuestion;

public record SendAnswerToTestQuestionRequest(
    string? Answer,
    List<string>? Answers,
    List<AnswerMatchOption>? MatchOptions);

