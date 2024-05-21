using Domain.Enums;

namespace Application.Courses.CreateCourse;

public record CreateCourseRequest(
    string Title,
    string? Description,
    CourseTabType? TabType);
