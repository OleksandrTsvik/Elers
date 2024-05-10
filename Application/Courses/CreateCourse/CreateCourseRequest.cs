namespace Application.Courses.CreateCourse;

public record CreateCourseRequest(
    string Title,
    string? Description,
    string? TabType);
