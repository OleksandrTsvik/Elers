namespace Application.Courses.GetListCourses;

public class GetListCourseItemResponse
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string? Description { get; init; }
    public required string? ImageUrl { get; init; }

    public int CountMembers { get; init; }
    public int CountMaterials { get; init; }
    public int CountAssignments { get; init; }
    public int CountTests { get; init; }
}

public class CourseListItemDto
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string? Description { get; init; }
    public required string? ImageUrl { get; init; }
    public required int CountMembers { get; init; }
    public required IEnumerable<Guid> TabIds { get; init; }
}

public class CountMaterialsDto
{
    public Guid TabId { get; init; }
    public int CountMaterials { get; init; }
    public int CountAssignments { get; init; }
    public int CountTests { get; init; }
}
