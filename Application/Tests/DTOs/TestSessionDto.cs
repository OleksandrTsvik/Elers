namespace Application.Tests.DTOs;

public class TestSessionDto
{
    public required Guid Id { get; init; }
    public required Guid TestId { get; init; }
    public required Guid UserId { get; init; }
    public required DateTime StartedAt { get; init; }
    public required DateTime? FinishedAt { get; init; }
}
