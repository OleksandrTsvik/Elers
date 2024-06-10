namespace Application.Tests.GetTestSession;

public class GetTestSessionResponse
{
    public required Guid TestSessionId { get; init; }
    public required Guid TestId { get; init; }
    public required DateTime StartedAt { get; init; }
    public required int? TimeLimitInMinutes { get; init; }
    public required List<TestSessionQuestionItem> Questions { get; init; } = [];
}

public class TestSessionQuestionItem
{
    public required Guid QuestionId { get; init; }
    public required bool IsAnswered { get; init; }
}
