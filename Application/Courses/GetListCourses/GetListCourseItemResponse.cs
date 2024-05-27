namespace Application.Courses.GetListCourses;

public class GetListCourseItemResponse
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string? Description { get; init; }
    public required string? ImageUrl { get; init; }
}
