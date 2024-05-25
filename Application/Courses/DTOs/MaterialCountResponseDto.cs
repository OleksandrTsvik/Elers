namespace Application.Courses.DTOs;

public class MaterialCountResponseDto
{
    public required Guid TabId { get; init; }
    public required int MaterialCount { get; init; }
}
