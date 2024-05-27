using Domain.Entities;
using Domain.Enums;

namespace Application.Courses.GetCourseByIdToEdit;

public class GetCourseByIdToEditResponse<TCourseTab>
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string? Description { get; init; }
    public required string? ImageUrl { get; init; }
    public required CourseTabType TabType { get; init; }
    public required TCourseTab[] CourseTabs { get; init; }
}

public class CourseTabToEditResponse : CourseTabToEditResponseDto
{
    public int MaterialCount { get; set; }
    public CourseMaterial[] CourseMaterials { get; set; } = [];
}

public class GetCourseByIdToEditResponseDto : GetCourseByIdToEditResponse<CourseTabToEditResponseDto>
{
}

public class CourseTabToEditResponseDto
{
    public required Guid Id { get; init; }
    public required Guid CourseId { get; init; }
    public required string Name { get; init; }
    public required bool IsActive { get; init; }
    public required int Order { get; init; }
    public required string? Color { get; init; }
    public required bool ShowMaterialsCount { get; init; }
}
