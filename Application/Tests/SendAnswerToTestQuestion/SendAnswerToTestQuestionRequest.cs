namespace Application.Tests.SendAnswerToTestQuestion;

public record SendAnswerToTestQuestionRequest(
    string? Answer,
    List<string>? Answers);
