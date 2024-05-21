using Application.Common.Messaging;
using Domain.Enums;

namespace Application.Courses.CreateCourse;

public record CreateCourseCommand(
    string Title,
    string? Description,
    CourseTabType? TabType) : ICommand;
