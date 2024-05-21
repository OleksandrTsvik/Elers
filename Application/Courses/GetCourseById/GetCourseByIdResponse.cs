using Domain.Entities;

namespace Application.Courses.GetCourseById;

public class GetCourseByIdResponse<TCourseTab>
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string? Description { get; init; }
    public required string? PhotoUrl { get; init; }
    public required string? TabType { get; init; }
    public required TCourseTab[] CourseTabs { get; init; }
}

public class CourseTabResponse : CourseTabResponseDto
{
    public CourseMaterial[] CourseMaterials { get; set; } = [];
}

public class GetCourseByIdResponseDto : GetCourseByIdResponse<CourseTabResponseDto>
{
}

public class CourseTabResponseDto
{
    public required Guid Id { get; init; }
    public required Guid CourseId { get; init; }
    public required string Name { get; init; }
    public required bool IsActive { get; init; }
    public required int Order { get; init; }
    public required string? Color { get; init; }
    public required bool ShowMaterialsCount { get; init; }
}
