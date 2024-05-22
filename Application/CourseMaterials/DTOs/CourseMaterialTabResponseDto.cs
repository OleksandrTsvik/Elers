using Domain.Enums;

namespace Application.CourseMaterials.DTOs;

public class CourseMaterialTabResponseDto
{
    public required Guid CourseId { get; init; }
    public required Guid TabId { get; init; }
    public required string CourseTitle { get; init; }
    public required CourseTabType CourseTabType { get; init; }
}
