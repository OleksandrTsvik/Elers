using Domain.Enums;

namespace Application.CourseMaterials.DTOs;

public class GetCourseMaterialResponseDto
{
    public required Guid Id { get; init; }
    public required Guid CourseId { get; init; }
    public required Guid TabId { get; init; }
    public required string CourseTitle { get; init; }
    public required CourseTabType CourseTabType { get; init; }
}

public class GetCourseMaterialContentResponseDto : GetCourseMaterialResponseDto
{
    public required string Content { get; init; }
}
