using Application.Common.Messaging;

namespace Application.Courses.CreateCourse;

public record CreateCourseCommand(string Title, string? Description) : ICommand;
