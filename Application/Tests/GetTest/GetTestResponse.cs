namespace Application.Tests.GetTest;

public class GetTestResponse
{
    public required Guid TestId { get; init; }
    public required Guid CourseTabId { get; init; }
    public required string Title { get; init; }
    public required string? Description { get; init; }
    public required int NumberAttempts { get; init; }
    public required int? TimeLimitInMinutes { get; init; }
    public required DateTime? Deadline { get; init; }
    public required List<TestAttemptItem> Attempts { get; init; }
}

public class TestAttemptItem
{
    public required Guid TestSessionId { get; init; }
    public required DateTime StartedAt { get; init; }
    public required DateTime? FinishedAt { get; init; }
    public required double? Grade { get; init; }
    public required bool IsCompleted { get; init; }
}
