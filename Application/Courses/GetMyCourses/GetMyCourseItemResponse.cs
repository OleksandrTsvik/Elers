namespace Application.Courses.GetMyCourses;

public class GetMyCourseItemResponse
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string? ImageUrl { get; init; }
    public required bool IsCreator { get; init; }
}
