namespace Application.Courses.GetCourseById;

public class GetCourseByIdResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? PhotoUrl { get; init; }
}
