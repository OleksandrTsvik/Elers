namespace Application.Courses.GetCourseByTabId;

public class GetCourseByTabIdResponse
{
    public required Guid TabId { get; init; }
    public required Guid? CourseId { get; init; }
    public required string? Title { get; init; }
}
